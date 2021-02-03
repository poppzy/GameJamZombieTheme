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
    public List<GameObject> m_PlayerZombies; //the list of zombies behind you
    public float m_Speed = 5f; //the amount of meter moved per movementupdate 
    public Direction m_Faceing; //the direction you are facing
    public Vector2 position; //the position of the object
    Vector2 direction;

    //private
    private GridManager gridInst;

    void Start()
    {
        m_PlayerZombies = new List<GameObject>();
        gridInst = GridManager.instance;
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        position = transform.position;

        if (x != 0)
        {
            if (x > 0 || m_Faceing == Direction.Right)
            {
                m_Faceing = Direction.Right;
                gridInst.m_PlayerGridPositions[0] += new Vector2(1, 0);
            }
            else if (x < 0 || m_Faceing == Direction.Left)
            {
                m_Faceing = Direction.Left;
                gridInst.m_PlayerGridPositions[0] += new Vector2(-1, 0);
            }
        }

        if (y != 0)
        {
            if (y > 0)
            {
                m_Faceing = Direction.Up;
                gridInst.m_PlayerGridPositions[0] += new Vector2(0, -1);
            }
            else if (y < 0)
            {
                m_Faceing = Direction.Down;
                gridInst.m_PlayerGridPositions[0] += new Vector2(0, 1);
            }
        }

        transform.position = gridInst.GetGridPosition((int)gridInst.m_PlayerGridPositions[0].x, (int)gridInst.m_PlayerGridPositions[0].y);

        //transform.Translate(direction * m_Speed * Time.deltaTime);

        /*SnapToGrid();*/
    }

    public void SnapToGrid()
    {
        if (m_Faceing == Direction.Up && Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.position = (new Vector2(transform.position.x, Mathf.Ceil(transform.position.y)));
        }
        else if (m_Faceing == Direction.Down && Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.position = (new Vector2(transform.position.x, Mathf.Floor(transform.position.y)));
        }
        else if (m_Faceing == Direction.Left && Input.GetAxisRaw("Vertical") != 0)
        {
            transform.position = new Vector2(Mathf.Floor(transform.position.x), transform.position.y);
        }
        else if (m_Faceing == Direction.Right && Input.GetAxisRaw("Vertical") != 0)
        {
            transform.position = (new Vector2(Mathf.Ceil(transform.position.x), transform.position.y));
        }
    }
}
