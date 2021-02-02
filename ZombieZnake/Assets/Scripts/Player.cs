using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Direction: int
    {
        Up = 0,
        Down,
        Left,
        Right
    }

    [Header("Player")]
    [SerializeField] private List<GameObject> m_PlayerZombies; //the list of zombies behind you
    public float m_Speed = 1.0f; //the amount of second it takes to move
    public Direction m_Faceing; //the direction you are facing


    //Private
    private Coroutine movementIEnumerator; //the movementCoroutine
    private bool isAlive = true; //booleon for if the object is alive

    void Start()
    {
        m_PlayerZombies = new List<GameObject>();
        movementIEnumerator = StartCoroutine(Movement());
    }

    public IEnumerator Movement()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(m_Speed);

            switch (m_Faceing)
            {
                case Direction.Up:
                    gameObject.transform.Translate(Vector2.up);
                    break;
                case Direction.Down:
                    gameObject.transform.Translate(Vector2.down);
                    break;
                case Direction.Left:
                    gameObject.transform.Translate(Vector2.left);
                    break;
                case Direction.Right:
                    gameObject.transform.Translate(Vector2.right);
                    break;
                default:
                    break;
            }
        }

        StopCoroutine(movementIEnumerator);
        yield return null;
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
}
