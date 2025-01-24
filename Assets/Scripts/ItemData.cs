using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject, IStatus
{
    public string Id { get; set; }
    public string itemName;
    public string ItemName { get; set; }
    public float str;
    public float Strength { get; set; }

    public float def;
    public float Defense { get; set; }

    public float agil;
    public float Agility { get; set; }

    public float health;
    public float Health { get; set; }

    public float intelligence;
    public float Intelligence { get; set; }

    public float luck;
    public float Luck { get; set; }

    public float critical;
    public float Critical { get; set; }

    public float range;
    public float Range { get; set; }

    public float distance;
    public float Distance { get; set; }

    public float coolTime;
    public float CoolTime { get; set; }

    public Sprite sprite;

    public virtual void SetStatus(string key)
    {

    }
}
