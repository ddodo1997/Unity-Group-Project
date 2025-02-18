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
    public int Level;
    public bool IsEmpty = true;
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
    public float BulletSpeed { get; set; }
    public float BulletLivingTime { get; set; }
    public float CoolTime { get; set; }
    public float LevelUpExperienceRequired {  get; set; }
    public float ExperienceValue {  get; set; }

    public int currentExp;

    public Sprite sprite;
    public virtual void SetStatusForLevel()
    {
        ItemData tempItem = DataTableManager.ArmorTable.Get(Id);
        if (tempItem == null)
            tempItem = DataTableManager.WeaponTable.Get(Id);

        for (int i = 0; i < Level; i++)
        {
            Strength += (int)tempItem.Strength * 0.2f;
            Defense += (int)tempItem.Defense * 0.2f;
            Agility += (int)tempItem.Agility * 0.2f;
            Health += (int)tempItem.Health * 0.2f;
            Intelligence += (int)tempItem.Intelligence * 0.2f;
            Luck += (int)tempItem.Luck * 0.2f;
            Critical += (int)tempItem.Critical * 0.2f;
            Range += (int)tempItem.Range * 0.2f;
            MotionSpeed += (int)tempItem.MotionSpeed * 0.2f;
            CoolTime += (int)tempItem.CoolTime * 0.2f;
            BulletSpeed += (int)tempItem.BulletSpeed * 0.2f;
            BulletLivingTime += (int)tempItem.BulletLivingTime * 0.2f;
        }
    }

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
        ExperienceValue = Random.Range(0, data.ExperienceValue);

        sprite = data.sprite;
    }

    public virtual void SetStatus(string key)
    {
        SetStatus(DataTableManager.ArmorTable.Get(key));
    }
    public virtual void LevelUp(int exp)
    {
        int totalexp = (exp / 10) + currentExp;
        int prevLevel = Level;
        while (totalexp >= LevelUpExperienceRequired)
        {
            totalexp -= (int)LevelUpExperienceRequired;
            Level++;
        }
        currentExp = totalexp;
        if(Level > 50)
        {
            Level = 50;
            currentExp = 0;
        }
        if (Level != prevLevel)
        {
            SetStatusForLevel();
        }
    }

    public virtual ItemData GetNewData()
    {
        ItemData data = new ItemData();
        data.Rate = Rate;
        data.Id = Id;
        data.Name = Name;
        data.Strength = Strength;
        data.Defense = Defense;
        data.Agility = Agility;
        data.Health = Health;
        data.Intelligence = Intelligence;
        data.Luck = Luck;
        data.Critical = Critical;
        data.Range = Range;
        data.MotionSpeed = MotionSpeed;
        data.CoolTime = CoolTime;
        data.BulletSpeed = BulletSpeed;
        data.BulletLivingTime = BulletLivingTime;
        data.LevelUpExperienceRequired = LevelUpExperienceRequired;
        data.ExperienceValue = Random.Range(0, ExperienceValue);
        return data;
    }
}
