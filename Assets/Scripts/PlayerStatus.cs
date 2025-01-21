using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : IStatus
{

    public float Strength { get; set; }
    public float Defense { get; set; }
    public float Agility { get; set; }
    public float Health { get; set; }
    public float Intelligence { get; set; }
    public float Luck { get; set; }
    public float Critical { get; set; }
    public float Range { get; set; }
    public float Distance { get; set; }
    public float CoolTime { get; set; }

    public void SetStatus()
    {
#if UNITY_EDITOR
        Agility = 5f;
#endif
    }
}
