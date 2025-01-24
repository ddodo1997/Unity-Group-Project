using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : IStatus
{
    public string Id { get; set; }
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

    public void UpdateStatus(ItemData itemData)
    {

    }
}
