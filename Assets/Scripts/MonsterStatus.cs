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
    public float MotionSpeed { get; set; } //�ִϸ��̼� ��� �ӵ�
    public float Range { get; set; } //���� ����
    public float Distance { get; set; } //�þ� ������
    public float CoolTime { get; set; } //���� �ֱ�

    //�ٰŸ� ������ �� 0
    public float BulletSpeed { get; set; }
    public float BulletLivingTime { get; set; }

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
        //var data = DataTableManager.MonsterTable.Get(key);


    }
}
