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
    public Vector2[,] m_Grid;

    private void Start()
    {
        m_Grid = new Vector2[(int)m_GridSize.x, (int)m_GridSize.y];
    }
}
