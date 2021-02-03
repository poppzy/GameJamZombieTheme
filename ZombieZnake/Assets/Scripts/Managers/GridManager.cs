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

        m_Grid = new Vector2[(int)m_GridSize.x, (int)m_GridSize.y];
        CreateGrid(m_GridSize.x, m_GridSize.y);
        m_PlayerGridPositions.Add(new Vector2(m_GridSize.x / 2, m_GridSize.y / 2));
    }

    [Header("Grid")]
    public Vector2 m_GridSize;
    public Vector2 m_GridOffset;
    public Vector2[,] m_Grid;

    [Header("Objects")]
    public List<Vector2> m_PlayerGridPositions = new List<Vector2>(); //the player location on the grid

    /// <summary>
    /// Create a grid using the a 2D array.
    /// </summary>
    /// <param name="_width"></param>
    /// <param name="_length"></param>
    private void CreateGrid(float _width, float _length)
    {
        for (int y = 0; y < _length; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                m_Grid[x, y] = new Vector2(m_GridOffset.x + x, m_GridOffset.y - y);
                //Debug.Log($"{x} = {m_GridOffset.x + x}, {y} = {m_GridOffset.y - y}.");
            }
        }
    }

    /// <summary>
    /// Get the position of the x and y value in the Grid Array.
    /// </summary>
    /// <param name="xVariable">The width of the grid</param>
    /// <param name="yVariable">The length of the grid</param>
    /// <returns></returns>
    public Vector2 GetGridPosition(int xVariable, int yVariable, GameObject _object)
    {
        //check if object is traying to go out of bounds
        if (xVariable < 0 || xVariable >= m_GridSize.x || yVariable < 0 || yVariable >= m_GridSize.x)
            if(_object.GetComponent<IDamagable>() != null)
            {
                //deal damage if the object is damageble, and return the current position
                _object.GetComponent<IDamagable>().ChangeHealth(-1);
                return m_Grid[(int)m_PlayerGridPositions[0].x, (int)m_PlayerGridPositions[0].y];
            }

        //return and change the new position
        m_PlayerGridPositions[0] = new Vector2(xVariable, yVariable);
        return m_Grid[xVariable, yVariable];
    }
}
