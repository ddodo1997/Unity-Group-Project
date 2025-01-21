using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    public abstract void Move();
    public abstract void Attack();
    public abstract void OnDamage(float damage);
    public abstract void OnDie();
}
