using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    public abstract float HP { get; set; }
    public abstract float Speed { get; set; }
    //public abstract float 


    public abstract void Move();
    public abstract void Attack();
    public abstract void OnDamage(float damage);
    public abstract void OnDie();
}
