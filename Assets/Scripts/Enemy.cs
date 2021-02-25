using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable), typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 100;

    Animator anim;
    Damageable damageable;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();

        damageable.OnRecieveDamage += RecieveDamage;
    }

    private void RecieveDamage(int damage)
    {
        anim.SetTrigger("Hit");
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
