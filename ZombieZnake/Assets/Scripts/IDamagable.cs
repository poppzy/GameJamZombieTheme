using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    int healthpoints { get; }

    public void ChangeHealth(int _value);
}
