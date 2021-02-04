using System;
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

        //create the grid for all objects to move on
        m_Grid = new Vector2[(int)m_GridSize.x, (int)m_GridSize.y];
        CreateGrid(m_GridSize.x, m_GridSize.y);
    }

    private void Start()
    {
        StartCoroutine(SpawnHumans());

        //spawn the first 3 zombies on the player
        for (int i = 0; i < PlayerController.instance.m_PlayerZombies.Count; i++)
        {
            m_PlayerGridLocations.Add(new GridObject(PlayerController.instance.m_PlayerZombies[i], new Vector2(m_GridSize.x / 2, m_GridSize.y / 2 + i)));
        }
    }

    [Header("Grid")]
    public Vector2 m_GridSize; //the size of the grid
    public Vector2 m_GridOffset; //the offset the grid will be created at
    public Vector2[,] m_Grid; //a 2D array of the grid

    [Header("GridLocations")]
    public List<GridObject> m_PlayerGridLocations = new List<GridObject>(); //the player locations on the grid
    public List<GridObject> m_HumanGridLocations = new List<GridObject>(); //the human locations on the grid

    [Header("Human")]
    public GameObject m_HumanPrefab; //the human prefab
    public float m_HumanSpawnDelay = 10f; //the time in seconds it can take for humans to spawn
    public Vector2 m_HumansSpawnedPerCycle = new Vector2(1, 6); //the min and max amount of humans that can spawn per cycle

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
    public Vector2 GetPlayerGridPosition(int xVariable, int yVariable)
    {
        IDamagable IDamageble = PlayerController.instance.GetComponent<IDamagable>();

        //check if object is traying to go out of bounds
        if (xVariable < 0 || xVariable >= m_GridSize.x || yVariable < 0 || yVariable >= m_GridSize.x)
            if (IDamageble != null)
            {
                //deal damage if the object is damageble, and return the current position
                IDamageble.ChangeHealth(-IDamageble.healthpoints);
                return m_Grid[(int)m_PlayerGridLocations[0].gridLocation.x, (int)m_PlayerGridLocations[0].gridLocation.y];
            }

        //check if the player hit itself, if it did kill the player
        for (int i = 0; i < m_PlayerGridLocations.Count; i++)
        {
            if (i != 0)
                if (m_PlayerGridLocations[0].gridLocation == m_PlayerGridLocations[i].gridLocation)
                    IDamageble.ChangeHealth(-IDamageble.healthpoints);
        }

        //check if you hit a human
        for (int i = 0; i < m_HumanGridLocations.Count; i++)
        {
            if (m_PlayerGridLocations[0].gridLocation == m_HumanGridLocations[i].gridLocation)
            {
                Destroy(m_HumanGridLocations[i].gridObject);
                m_HumanGridLocations.RemoveAt(i);

                //TODO: maby add score

                GameObject zombie = Instantiate(PlayerController.instance.m_ZombiePrefab, PlayerController.instance.gameObject.transform);
                PlayerController.instance.m_PlayerZombies.Add(zombie);
                m_PlayerGridLocations.Add(new GridObject(zombie, m_PlayerGridLocations[m_PlayerGridLocations.Count - 1].gridLocation));
            }
        }

        //return and change the new position
        return m_Grid[xVariable, yVariable];
    }

    /// <summary>
    /// Spawn a random amount of humans every {m_HumanSpawnDelay} amount of seconds
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnHumans()
    {
        IDamagable playerIDamageble = PlayerController.instance.GetComponent<IDamagable>();

        while (playerIDamageble.isAlive)
        {
            //wait {m_HumanSpawnDelay} second until you spawn humans again
            yield return new WaitForSeconds(m_HumanSpawnDelay);

            //random amount of humans spawned
            int random = UnityEngine.Random.Range((int)m_HumansSpawnedPerCycle.x, (int)m_HumansSpawnedPerCycle.y);

            //spawn the humans
            for (int i = 0; i < random; i++)
            {
                GameObject human = Instantiate(m_HumanPrefab);

                int x = UnityEngine.Random.Range(0, (int)m_GridSize.x);
                int y = UnityEngine.Random.Range(0, (int)m_GridSize.y);

                m_HumanGridLocations.Add(new GridObject(human, new Vector2(x, y)));

                human.transform.position = m_Grid[(int)m_HumanGridLocations[m_HumanGridLocations.Count - 1].gridLocation.x, (int)m_HumanGridLocations[m_HumanGridLocations.Count - 1].gridLocation.y];
            }
        }
    }


    [Serializable]
    public struct GridObject
    {
        public GameObject gridObject;
        public Vector2 gridLocation;

        public GridObject(GameObject _gridObject, Vector2 _gridLocation)
        {
            gridObject = _gridObject;
            gridLocation = _gridLocation;
        }
    }
}
