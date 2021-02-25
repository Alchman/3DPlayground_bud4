using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int damage = 40;

    private Collider col;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        col.enabled = false;
    }

    public void AttackStart()
    {
        col.enabled = true;
    }

    public void AttackEnd()
    {
        col.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if(damageable != null)
        {
            damageable.DoDamage(damage);
        }
    }
}
