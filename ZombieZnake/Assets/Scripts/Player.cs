using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private List<GameObject> m_PlayerZombies; //the list of zombies behind you
    public float m_Speed = 0.5f; //the amount of meter moved per movementupdate 
    public Direction m_Faceing; //the direction you are facing


    //Private
    private bool isAlive = true; //booleon for if the object is alive

    void Start()
    {
        m_PlayerZombies = new List<GameObject>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y);

        if (direction.x > 0)
            m_Faceing = Direction.Right;
        else if (direction.x < 0)
            m_Faceing = Direction.Left;

        if (direction.y > 0)
            m_Faceing = Direction.Up;
        else if (direction.y < 0)
            m_Faceing = Direction.Down;
    }

    private void FixedUpdate()
    {
        while (isAlive)
        {
            switch (m_Faceing)
            {
                case Direction.Up:
                    gameObject.transform.Translate(Vector2.up * m_Speed * Time.fixedDeltaTime, Space.World);
                    break;
                case Direction.Down:
                    gameObject.transform.Translate(Vector2.down * m_Speed * Time.fixedDeltaTime, Space.World);
                    break;
                case Direction.Left:
                    gameObject.transform.Translate(Vector2.left * m_Speed * Time.fixedDeltaTime, Space.World);
                    break;
                case Direction.Right:
                    gameObject.transform.Translate(Vector2.right * m_Speed * Time.fixedDeltaTime, Space.World);
                    break;
                default:
                    break;
            }
        }
    }
}
