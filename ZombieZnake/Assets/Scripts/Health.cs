using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour , IDamagable
{
    [Header("Health")]
    public int m_StartingHealth = 10;

    //private
    public int healthpoints { get; private set; }
    public bool isAlive { get; private set; }

    void Start()
    {
        healthpoints = m_StartingHealth;
        isAlive = true;
    }

    public void ChangeHealth(int _value)
    {
        healthpoints += _value;

        if (healthpoints <= 0)
            Die();
    }

    public void Die()
    {
        isAlive = false;
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
