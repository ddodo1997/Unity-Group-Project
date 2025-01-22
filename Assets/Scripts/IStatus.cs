using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Attack power	Defense	Movement speed	Evasion rate	Health	Natural recovery amount (second)	Accuracy(%)	Critical Chance	Equipment drop rate	Critical Chance	Cri Damage	Intersection(m)	Range(m)	Cooltime(second)

public interface IStatus
{
    //UI���� ���̴� ���ȵ�
    public abstract float Strength { get; set; }
    public abstract float Defense { get; set; }
    public abstract float Agility { get; set; }
    public abstract float Health { get; set; }
    public abstract float Intelligence { get; set; }
    public abstract float Luck { get; set; }
    public abstract float Critical { get; set; }
    public abstract float Range { get; set; }
    public abstract float Distance { get; set; }
    public abstract float CoolTime { get; set; }

    //���ο��� �۵��ϴ� ���ȵ�

    //�Լ���
    public abstract void SetStatus(string key);
}
