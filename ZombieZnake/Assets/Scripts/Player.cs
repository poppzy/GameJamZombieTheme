using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    public static Player instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public enum Direction : int
    {
        Up = 0,
        Down,
        Left,
        Right
    }

    [Header("Player")]
    public List<GameObject> m_PlayerZombies; //the list of zombies behind you
    public float m_MovementUpdate = 0.5f; //the amount of meter moved per movementupdate 
    public float m_StepSize = 1f; //the amount of steps taken per movementupdate
    public Direction m_Faceing; //the direction you are facing

    //private
    private Health healthScript;
    private GridManager grid;

    void Start()
    {
        m_PlayerZombies = new List<GameObject>();
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                m_PlayerZombies.Add(child.gameObject);
            }
        }
        healthScript = GetComponent<Health>();
        grid = GridManager.instance;
        //inputQueue = new List<int>();

        StartCoroutine(Movement());
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (y != 0)
        {
            if (y > 0 && m_Faceing != Direction.Down && m_Faceing != Direction.Up)
                m_Faceing = Direction.Up;
            else if (y < 0 && m_Faceing != Direction.Up && m_Faceing != Direction.Down)
                m_Faceing = Direction.Down;
        }

        if (x != 0)
        {
            if (x < 0 && m_Faceing != Direction.Right && m_Faceing != Direction.Left)
                m_Faceing = Direction.Left;
            else if (x > 0 && m_Faceing != Direction.Left && m_Faceing != Direction.Right)
                m_Faceing = Direction.Right;
        }
    }

    private IEnumerator Movement()
    {
        //check if object is alive
        while (healthScript.isAlive)
        {
            //wait for {m_MovementUpdate} amount of seconds
            yield return new WaitForSeconds(m_MovementUpdate);

            //the desired position on the grid
            Vector2 desiredPosition = grid.m_PlayerGridLocations[0];
            Vector2 previousPosition = Vector2.zero;

            switch (m_Faceing)
            {
                case Direction.Up:
                    desiredPosition += new Vector2(0, -1);
                    break;
                case Direction.Down:
                    desiredPosition += new Vector2(0, 1);
                    break;
                case Direction.Left:
                    desiredPosition += new Vector2(-1, 0);
                    break;
                case Direction.Right:
                    desiredPosition += new Vector2(1, 0);
                    break;
                default:
                    break;
            }

            for (int i = 0; i < m_PlayerZombies.Count; i++)
            {
                if (i != 0)
                    desiredPosition = previousPosition;

                //update the position using the grid
                m_PlayerZombies[i].transform.position = grid.GetPlayerGridPosition((int)desiredPosition.x, (int)desiredPosition.y) * m_StepSize;
                previousPosition = grid.m_PlayerGridLocations[i];
                m_PlayerZombies[i].GetComponent<Animator>().SetFloat("X", desiredPosition.x - previousPosition.x);
                m_PlayerZombies[i].GetComponent<Animator>().SetFloat("Y", desiredPosition.y - previousPosition.y);
                grid.m_PlayerGridLocations[i] = new Vector2(desiredPosition.x, desiredPosition.y);
            }
        }
    }
}
