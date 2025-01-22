using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : IStatus
{
    public float Strength { get; set; }


    public float Defense { get; set; }

    //��ø ����
    public float Agility { get; set; }
    public float MovementSpeed { get; set; }
    public float EvasionRate { get; set; }


    public float Health { get; set; }


    public float Intelligence { get; set; }

    //��� ����
    public float Luck { get; set; }
    public float Accuracy { get; set; }
    public float CriticalChance { get; set; }
    public float EquipmentDropRate { get; set; }

    //ũ��Ƽ�� ����
    public float Critical { get; set; }
    public float CriticalDamage { get; set; }

    //���� ����
    public float Range { get; set; }


    //����
    public float MotionSpeed { get; set; }

    //����ü �ӵ�
    public float BulletSpeed { get; set; }

    //����ü �ð�
    public float BulletLivingTime { get; set; }

    //���X
    public float Distance { get; set; }

    //���ݰ� ������
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
