using System;
using UnityEngine;
public enum ClassName
{
    Warrior,
    Archer,
    Sorcerer
}
public enum StatusOrder
{
    Strength,
    Intelligence,
    Agility,
    Luck,
    Health,
    Critical,
}
public class PlayerStatus : IStatus
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ClassName className;
    public float Strength { get; set; }


    public float Defense { get; set; }

    //민첩 관련
    public float Agility { get; set; }
    public float MovementSpeed;


    public float Health { get; set; }
    public float hp;

    public float Intelligence { get; set; }


    //행운 관련
    public float Luck { get; set; }
    public float Accuracy;
    public float CriticalChance;


    //크리티컬 관련
    public float Critical { get; set; }

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

    public void SetBasedStatus()
    {
        MovementSpeed = Mathf.Clamp(Agility * 0.02f, 5f, 15f);
        hp = Health;
        Accuracy = Luck * 0.3f;
        CriticalChance = (Critical + (Luck * 0.2f)) * 0.2f * 0.001f;
    }

    public void SetStatus(string key)
    {
        var data = DataTableManager.CharacterTable.Get(key);
        Id = data.Id;
        Name = data.Name;
        className = Enum.Parse<ClassName>(Id);

        Strength = data.Strength;
        Defense = data.Defense;
        Agility = data.Agility;



        Health = data.Health;
        Intelligence = data.Intelligence;
        Luck = data.Luck;


        Critical = data.Critical;
        Range = data.Range;
        MotionSpeed = data.MotionSpeed;
        BulletSpeed = data.BulletSpeed;
        BulletLivingTime = data.BulletLivingTime;
        CoolTime = data.CoolTime;
        SetBasedStatus();
    }
    public void SetStatus(PlayerStatus data)
    {
        Id = data.Id;
        Name = data.Name;
        className = Enum.Parse<ClassName>(Id);

        Strength = data.Strength;
        Defense = data.Defense;
        Agility = data.Agility;



        Health = data.Health;
        Intelligence = data.Intelligence;
        Luck = data.Luck;


        Critical = data.Critical;
        Range = data.Range;
        MotionSpeed = data.MotionSpeed;
        BulletSpeed = data.BulletSpeed;
        BulletLivingTime = data.BulletLivingTime;
        CoolTime = data.CoolTime;
        SetBasedStatus();
    }

    public void SetStatus(ItemData itemdata)
    {
        Strength += itemdata.Strength;
        Defense += itemdata.Defense;
        Agility += itemdata.Agility;
        Health += itemdata.Health;
        Intelligence += itemdata.Intelligence;
        Luck += itemdata.Luck;
        Critical += itemdata.Critical;
        MotionSpeed += itemdata.MotionSpeed;
        BulletSpeed += itemdata.BulletSpeed;
        BulletLivingTime += itemdata.BulletLivingTime;
        CoolTime += itemdata.CoolTime;
    }

    public void SetStatus(ref EquipmentSlot[] equipmentDatas)
    {
        var prevHp = hp;
        SetStatus(Id);
        for (int i = 0; i < equipmentDatas.Length - 1; i++)
        {
            if (equipmentDatas[i].itemData.IsEmpty)
                continue;
            SetStatus(equipmentDatas[i].itemData);
        }
        if (!equipmentDatas[5].itemData.IsEmpty)
            SetStatus(equipmentDatas[5].itemData);

        SetBasedStatus();
        if(prevHp <= Health)
            hp = prevHp;
    }
}
