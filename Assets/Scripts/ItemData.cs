using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData : IStatus
{
    public string Rate {  get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public float Strength { get; set; }
    public float Defense { get; set; }
    public float Agility { get; set; }
    public float Health { get; set; }
    public float Intelligence { get; set; }
    public float Luck { get; set; }
    public float Critical { get; set; }
    public float Range { get; set; }
    public float MotionSpeed { get; set; }
    public float CoolTime { get; set; }
    public float BulletSpeed { get; set; }
    public float BulletLivingTime { get; set; }
    public float LevelUpExperienceRequired {  get; set; }
    public float ExperienceValue {  get; set; }

    public Sprite sprite;

    public virtual void SetStatus(string key)
    {

    }
}
