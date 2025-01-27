using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : IStatus
{
    public enum Rating
    {
        Normal,
        Elite,
        Boss
    }

    public string Id { get; set; }
    public int Stage { get; set; }
    public string StringId { get; set; }
    public Rating Rate {  get; set; }
    public string Name { get; set; }
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

    //근거리 몬스터일 시 0
    public float BulletSpeed { get; set; }
    public float BulletLivingTime { get; set; }

    public void SetStatus(string key)
    {
        var data = DataTableManager.MonsterTable.Get(key);
        Id = data.Id;
        Stage = data.Stage;
        StringId = data.StringId;
        Rate = data.Rate;
        Name = data.Name;
        Level = data.Level;
        Strength = data.Strength;
        Defense = data.Defense;
        Agility = data.Agility;
        Health = data.Health;
        Intelligence = data.Intelligence;
        Luck = data.Luck;
        Critical = data.Critical;
        Range = data.Range;
        Distance = data.Distance;
        CoolTime = data.CoolTime;
        BulletSpeed = data.BulletSpeed;
        BulletLivingTime = data.BulletLivingTime;

    }
}
