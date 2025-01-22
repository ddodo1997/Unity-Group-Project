using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : IStatus
{
    public float Strength { get; set; }


    public float Defense { get; set; }

    //민첩 관련
    public float Agility { get; set; }
    public float MovementSpeed { get; set; }
    public float EvasionRate { get; set; }


    public float Health { get; set; }


    public float Intelligence { get; set; }

    //행운 관련
    public float Luck { get; set; }
    public float Accuracy { get; set; }
    public float CriticalChance { get; set; }
    public float EquipmentDropRate { get; set; }

    //크리티컬 관련
    public float Critical { get; set; }
    public float CriticalDamage { get; set; }

    //공격 범위
    public float Range { get; set; }


    //공속
    public float MotionSpeed { get; set; }

    //투사체 속도
    public float BulletSpeed { get; set; }

    //투사체 시간
    public float BulletLivingTime { get; set; }

    //사용X
    public float Distance { get; set; }

    //공격간 딜레이
    public float CoolTime { get; set; }



    public void SetStatus(string key)
    {
#if UNITY_EDITOR
        Agility = 5f;
        Range = 0.8f;
        CoolTime = 0.8f;
#endif
    }

    public void UpdateStatus(ItemData itemData)
    {

    }
}
