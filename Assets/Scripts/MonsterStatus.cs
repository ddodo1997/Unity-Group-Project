using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : IStatus
{
    public string Id { get; set; }
    public int Stage { get; set; }
    public string Name { get; set; }
    public string Class {  get; set; }
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
        Strength = 100;
        Health = 300;
        Agility = 3f;
        Range = 2f;
        Distance = 5f;
        CoolTime = 1.8f;
#endif
    }
}
