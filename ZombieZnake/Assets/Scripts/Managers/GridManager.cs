using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    [Header("Grid")]
    public Vector2 m_GridSize;
    public Vector2 m_GridOffset;
    public Vector2[,] m_Grid;

    private void Start()
    {
        m_Grid = new Vector2[(int)m_GridSize.x, (int)m_GridSize.y];
        CreateGrid(m_GridSize.x, m_GridSize.y);
    }

    /// <summary>
    /// Create a grid using the a 2D array.
    /// </summary>
    /// <param name="_width"></param>
    /// <param name="_length"></param>
    private void CreateGrid(float _width, float _length)
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _length; y++)
            {
                m_Grid[x, y] = new Vector2(m_GridOffset.x + x, m_GridOffset.y + y);
            }
        }
    }

    public void MoveOnGrid(float x, float y)
    {

    }
}
