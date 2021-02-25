using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHelper : MonoBehaviour
{
    [SerializeField] private Weapon weapon;

    private Animator anim;
    private bool checkCombo;
    private bool isAttacking;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAttacking)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (checkCombo)
            {
                anim.SetTrigger("Attack2");
            }
            else
            {
                anim.SetTrigger("Attack");
            }
        }
    }

    public void MeleeAttackStart()
    {
        isAttacking = true;
        weapon.AttackStart();
    }

    public void MeleeAttackEnd()
    {
        isAttacking = false;
        weapon.AttackEnd();

        anim.ResetTrigger("Attack");
    }

    public void ComboStart()
    {
        checkCombo = true;
    }

    public void ComboEnd()
    {
        checkCombo = false;
    }
}
