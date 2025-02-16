using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : IStatus
{
    public string Id { get; set; }
    public string Name { get; set; }
    public float Strength { get; set; }


    public float Defense { get; set; }

    //민첩 관련
    public float Agility { get; set; }
    public float MovementSpeed;
    public float EvasionRate;


    public float Health { get; set; }


    public float Intelligence { get; set; }

    //행운 관련
    public float Luck { get; set; }
    public float Accuracy;
    public float CriticalChance;
    public float EquipmentDropRate;

    //크리티컬 관련
    public float Critical { get; set; }
    public float CriticalDamage;

    //공격 범위
    public float Range { get; set; }


    //공속
    public float MotionSpeed { get; set; }

    //투사체 속도
    public float BulletSpeed { get; set; }

    //투사체 시간
    public float BulletLivingTime { get; set; }

    //공격간 딜레이
    public float CoolTime { get; set; }

    public void SetStatus(string key)
    {
        var data = DataTableManager.CharacterTable.Get(key);
        Id = data.Id;
        Strength = data.Strength;
        Defense = data.Defense;
        Agility = data.Agility;
        MovementSpeed = Agility * 0.02f;
        
        

        Health = data.Health;
        Intelligence = data.Intelligence;
        Luck = data.Luck;


        Critical = data.Critical;
        Range = data.Range;
        MotionSpeed = data.MotionSpeed;
        BulletSpeed = data.BulletSpeed;
        BulletLivingTime = data.BulletLivingTime;
        CoolTime = data.CoolTime;
    }

}
