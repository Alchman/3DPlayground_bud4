using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public Action<int> OnRecieveDamage = delegate { };

    public void DoDamage(int damage)
    {
        OnRecieveDamage(damage);
    }
}
