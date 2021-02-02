using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public int healthpoints { get; set; }

    public void ChangeHealth(int _value);
}
