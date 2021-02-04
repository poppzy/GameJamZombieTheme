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
        m_PlayerGridLocations.Add(new Vector2(m_GridSize.x / 2, m_GridSize.y / 2));
        m_PlayerGridLocations.Add(new Vector2(m_GridSize.x / 2, m_GridSize.y / 2 + 1));
        m_PlayerGridLocations.Add(new Vector2(m_GridSize.x / 2, m_GridSize.y / 2 + 2));

    }

    private void Start()
    {
        StartCoroutine(SpawnHumans());
    }

    [Header("Grid")]
    public Vector2 m_GridSize; //the size of the grid
    public Vector2 m_GridOffset; //the offset the grid will be created at
    public Vector2[,] m_Grid; //a 2D array of the grid

    [Header("GridLocations")]
    public List<Vector2> m_PlayerGridLocations = new List<Vector2>(); //the player locations on the grid
    public List<Vector2> m_HumanGridLocations = new List<Vector2>(); //the human locations on the grid

    [Header("Human")]
    public GameObject m_HumanPrefab;
    public float m_HumanSpawnDelay = 10f;
    public Vector2 m_HumansSpawnedPerCycle = new Vector2(1, 6);

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
        IDamagable IDamageble = _object.GetComponent<IDamagable>();

        //check if object is traying to go out of bounds
        if (xVariable < 0 || xVariable >= m_GridSize.x || yVariable < 0 || yVariable >= m_GridSize.x)
            if (IDamageble != null)
            {
                //deal damage if the object is damageble, and return the current position
                IDamageble.ChangeHealth(-IDamageble.healthpoints);
                return m_Grid[(int)m_PlayerGridLocations[0].x, (int)m_PlayerGridLocations[0].y];
            }

        //check if the player hit itself, if it did kill the player
        for (int i = 0; i < m_PlayerGridLocations.Count; i++)
        {
            if (i != 0)
                if (m_PlayerGridLocations[0] == m_PlayerGridLocations[i])
                    IDamageble.ChangeHealth(-IDamageble.healthpoints);
        }

        //return and change the new position
        return m_Grid[xVariable, yVariable];
    }

    private IEnumerator SpawnHumans()
    {
        IDamagable playerIDamageble = Player.instance.GetComponent<IDamagable>();

        while (playerIDamageble.isAlive)
        {
            //wait {m_HumanSpawnDelay} second until you spawn humans again
            yield return new WaitForSeconds(m_HumanSpawnDelay);

            //random amount of humans spawned
            int random = Random.Range((int)m_HumansSpawnedPerCycle.x, (int)m_HumansSpawnedPerCycle.y);

            //spawn the humans
            for (int i = 0; i < random; i++)
            {
                GameObject human = Instantiate(m_HumanPrefab);

                int x = Random.Range(0, (int)m_GridSize.x);
                int y = Random.Range(0, (int)m_GridSize.y);

                m_HumanGridLocations.Add(new Vector2(x, y));

                human.transform.position = m_Grid[(int)m_HumanGridLocations[i].x, (int)m_HumanGridLocations[i].y];
            }
        }
    }
}
