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
    public Stack<GameObject> m_PlayerZombies; //the list of zombies behind you
    public float m_Speed = 5f; //the amount of meter moved per movementupdate 
    public Direction m_Faceing; //the direction you are facing
    public Vector2 position; //the position of the object

    void Start()
    {
        m_PlayerZombies = new Stack<GameObject>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        position = transform.position;

        Vector2 direction = new Vector2(x, y);

        if (x != 0)
        {
            if (direction.x > 0)
                m_Faceing = Direction.Right;
            else if (direction.x < 0)
                m_Faceing = Direction.Left;
        }

        if (y != 0)
        {
            if (direction.y > 0)
                m_Faceing = Direction.Up;
            else if (direction.y < 0)
                m_Faceing = Direction.Down;
        }

    }

    private void FixedUpdate()
    {
        switch (m_Faceing)
        {
            case Direction.Up:
                transform.Translate(Vector2.up * m_Speed * Time.fixedDeltaTime);
                transform.position = (new Vector2(Mathf.Round(transform.position.x), transform.position.y));
                break;
            case Direction.Down:
                transform.Translate(Vector2.down * m_Speed * Time.fixedDeltaTime);
                transform.position = (new Vector2(Mathf.Round(transform.position.x), transform.position.y));
                break;
            case Direction.Left:
                transform.Translate(Vector2.left * m_Speed * Time.fixedDeltaTime);
                transform.position = new Vector2(transform.position.x, Mathf.Round(transform.position.y));
                break;
            case Direction.Right:
                transform.Translate(Vector2.right * m_Speed * Time.fixedDeltaTime);
                transform.position = (new Vector2(transform.position.x, Mathf.Round(transform.position.y)));
                break;
            default:
                break;
        }
    }
}
