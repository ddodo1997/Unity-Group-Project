using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
public enum ClassName
{
    Warrior,
    Archer,
    Sorcerer
}
public class PlayerStatus : IStatus
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ClassName className;
    public float Strength { get; set; }


    public float Defense { get; set; }

    //��ø ����
    public float Agility { get; set; }
    public float MovementSpeed;
    public float EvasionRate;


    public float Health { get; set; }


    public float Intelligence { get; set; }

    //��� ����
    public float Luck { get; set; }
    public float Accuracy;
    public float CriticalChance;
    public float EquipmentDropRate;

    //ũ��Ƽ�� ����
    public float Critical { get; set; }
    public float CriticalDamage;

    //���� ����
    public float Range { get; set; }


    //����
    public float MotionSpeed { get; set; }

    //����ü �ӵ�
    public float BulletSpeed { get; set; }

    //����ü �ð�
    public float BulletLivingTime { get; set; }

    //���ݰ� ������
    public float CoolTime { get; set; }

    public void SetStatus(PlayerStatus status)
    {
        Strength = status.Strength;
        Defense = status.Defense;
        Agility = status.Agility;
        Health = status.Health;
        Intelligence = status.Intelligence;
        Luck = status.Luck;
        Critical= status.Critical;
        MotionSpeed = status.MotionSpeed;
        BulletSpeed = status.BulletSpeed;
        BulletLivingTime = status.BulletLivingTime;
        CoolTime = status.CoolTime;
    }
    public void SetStatus(string key)
    {
        var data = DataTableManager.CharacterTable.Get(key);
        Id = data.Id;
        Name = data.Name;
        className = Enum.Parse<ClassName>(Name);

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
     
    public void SetStatus(ref EquipmentData[] equipmentDatas, WeaponData weaponData)
    {
        //SetStatus(Name);
        //float[] stats = new float[12];
        //for (int i = 0; i < stats.Length; i++)
        //{
        //    stats[i] 
        //}
    }
}
