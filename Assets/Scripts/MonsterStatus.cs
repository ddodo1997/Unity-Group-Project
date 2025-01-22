using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : IStatus
{
    public int Level { get; private set; }
    public float Strength { get; set; }
    public float Defense { get; set; }
    public float Agility { get; set; }
    public float Health { get; set; }
    public float Intelligence { get; set; }
    public float Luck { get; set; }
    public float Critical { get; set; }
    public float Range { get; set; } //공격 범위
    public float Distance { get; set; } //시야 반지름
    public float CoolTime { get; set; } //공격 주기

    public void SetStatus(string key)
    {
#if UNITY_EDITOR
        Agility = 3f;
        Distance = 5f;
        CoolTime = 1.8f;
#endif
    }
}
