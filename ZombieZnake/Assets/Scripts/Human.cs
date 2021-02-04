using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Human : MonoBehaviour
{
    public enum Direction : int
    {
        Up = 0,
        Down,
        Left,
        Right
    }

    [Header("Human")]
    public Direction m_Faceing; //the direction you are facing

    //private
    private Health healthScript;
    private GridManager grid;

    void Start()
    {
        healthScript = GetComponent<Health>();
        grid = GridManager.instance;
        StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {
        while (healthScript.isAlive)
        {
            yield return new WaitForSeconds(GridManager.instance.m_MovementUpdate);

            int x = Random.Range(-1, 2);
            int y = Random.Range(-1, 2);
        }
    }
}
