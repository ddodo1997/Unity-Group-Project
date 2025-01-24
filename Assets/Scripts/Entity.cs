using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IAction
{
    protected readonly int attackTrigger = Animator.StringToHash("2_Attack");
    protected readonly int damageTrigger = Animator.StringToHash("3_Damaged");
    protected readonly int deathTrigger = Animator.StringToHash("4_Death");
    protected readonly string moveBool = "1_Move";
    protected readonly string dieBool = "isDeath";

    public abstract void Attack();
    public abstract void Move();
    public abstract void OnDamage(float damage);
    public abstract void OnDie();
}
