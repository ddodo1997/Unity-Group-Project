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
    public float Range { get; set; } //���� ����
    public float Distance { get; set; } //�þ� ������
    public float CoolTime { get; set; } //���� �ֱ�

    public void SetStatus(string key)
    {
#if UNITY_EDITOR
        Agility = 3f;
        Distance = 5f;
        CoolTime = 1.8f;
#endif
    }
}
