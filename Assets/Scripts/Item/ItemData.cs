using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipRate
{
    Common,
    UnCommon,
    Rare,
    Hero,
    Legendary,
}

[System.Serializable]
public class ItemData : IStatus
{
    public EquipRate Rate {  get; set; }
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

    public virtual void SetStatus(ItemData data)
    {
        Rate = data.Rate;
        Id = data.Id;
        Name = data.Name;
        Strength = data.Strength;
        Defense = data.Defense;
        Agility = data.Agility;
        Health = data.Health;
        Intelligence = data.Intelligence;
        Luck = data.Luck;
        Critical = data.Critical;
        Range = data.Range;
        MotionSpeed = data.MotionSpeed;
        CoolTime = data.CoolTime;
        BulletSpeed = data.BulletSpeed;
        BulletLivingTime = data.BulletLivingTime;
        LevelUpExperienceRequired = data.LevelUpExperienceRequired;
        ExperienceValue = data.ExperienceValue;

        sprite = data.sprite;
    }

    public virtual void SetStatus(string key)
    {
        SetStatus(DataTableManager.ArmorTable.Get(key));
    }
}
