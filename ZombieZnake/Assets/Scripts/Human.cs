using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float m_MovementUpdate = 0.5f; //the amount of meter moved per movementupdate 
    public float m_StepSize = 1f; //the amount of steps taken per movementupdate
    public Direction m_Faceing; //the direction you are facing

    private void Start()
    {
        StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {
        yield return new WaitForSeconds(m_MovementUpdate);
    }
}
