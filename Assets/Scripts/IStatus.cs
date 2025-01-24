using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Attack power	Defense	Movement speed	Evasion rate	Health	Natural recovery amount (second)	Accuracy(%)	Critical Chance	Equipment drop rate	Critical Chance	Cri Damage	Intersection(m)	Range(m)	Cooltime(second)

public interface IStatus
{
    //데이터 테이블 파싱용 id
    public abstract string Id { get; set; }
    //UI에서 보이는 스탯들
    public abstract string Name { get; set; }
    public abstract float Strength { get; set; }
    public abstract float Defense { get; set; }
    public abstract float Agility { get; set; }
    public abstract float Health { get; set; }
    public abstract float Intelligence { get; set; }
    public abstract float Critical { get; set; }
    public abstract float Range { get; set; }
    public abstract float MotionSpeed { get; set; }
    public abstract float CoolTime { get; set; }
    public abstract float BulletSpeed { get; set; }
    public abstract float BulletLivingTime { get; set; }

    //함수들
    public abstract void SetStatus(string key);
}
