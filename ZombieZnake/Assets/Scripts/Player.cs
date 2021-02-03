using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    public enum Direction : int
    {
        Up = 0,
        Down,
        Left,
        Right
    }

    [Header("Player")]
    public List<GameObject> m_PlayerZombies; //the list of zombies behind you
    public float m_MovementUpdate = 0.2f; //the amount of meter moved per movementupdate 
    public float m_StepSize = 1f;
    public Direction m_Faceing; //the direction you are facing
    public Vector2 position; //the position of the object

    //private
    private Health healthScript;
    private GridManager gridInst;

    void Start()
    {
        m_PlayerZombies = new List<GameObject>();
        healthScript = GetComponent<Health>();
        gridInst = GridManager.instance;

        StartCoroutine(Movement());
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        position = transform.position;

        if (y != 0)
        {
            if (y > 0)
                m_Faceing = Direction.Up;
            else if (y < 0)
                m_Faceing = Direction.Down;
        }

        if (x != 0)
        {
            if (x < 0)
                m_Faceing = Direction.Left;
            else if (x > 0)
                m_Faceing = Direction.Right;
        }
    }

    private IEnumerator Movement()
    {
        Debug.Log(healthScript.isAlive);

        while (healthScript.isAlive)
        {
            yield return new WaitForSeconds(m_MovementUpdate);

            switch (m_Faceing)
            {
                case Direction.Up:
                    gridInst.m_PlayerGridPositions[0] += new Vector2(0, -1);
                    break;
                case Direction.Down:
                    gridInst.m_PlayerGridPositions[0] += new Vector2(0, 1);
                    break;
                case Direction.Left:
                    gridInst.m_PlayerGridPositions[0] += new Vector2(-1, 0);
                    break;
                case Direction.Right:
                    gridInst.m_PlayerGridPositions[0] += new Vector2(1, 0);
                    break;
                default:
                    break;
            }

            transform.position = gridInst.GetGridPosition((int)gridInst.m_PlayerGridPositions[0].x, (int)gridInst.m_PlayerGridPositions[0].y) * m_StepSize;
        }
    }

    //public void SnapToGrid()
    //{
    //    if (m_Faceing == Direction.Up && Input.GetAxisRaw("Horizontal") != 0)
    //    {
    //        transform.position = (new Vector2(transform.position.x, Mathf.Ceil(transform.position.y)));
    //    }
    //    else if (m_Faceing == Direction.Down && Input.GetAxisRaw("Horizontal") != 0)
    //    {
    //        transform.position = (new Vector2(transform.position.x, Mathf.Floor(transform.position.y)));
    //    }
    //    else if (m_Faceing == Direction.Left && Input.GetAxisRaw("Vertical") != 0)
    //    {
    //        transform.position = new Vector2(Mathf.Floor(transform.position.x), transform.position.y);
    //    }
    //    else if (m_Faceing == Direction.Right && Input.GetAxisRaw("Vertical") != 0)
    //    {
    //        transform.position = (new Vector2(Mathf.Ceil(transform.position.x), transform.position.y));
    //    }
    //}
}
