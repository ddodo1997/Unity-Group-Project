using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmorType
{
    Helmet,
    Armor,
    Shoes,
    Cloak,
    Ring,
};

public class EquipmentData : ItemData
{
    public ArmorType Type { get; set; }
    public override void SetStatus(ItemData data)
    {
        base.SetStatus(data);
        Type = (data as EquipmentData).Type;
    }

    public override void SetStatus(string key)
    {
        base.SetStatus(key);
    }
    public override ItemData GetNewData()
    {
        EquipmentData data = new EquipmentData();
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
        data.Type = Type;
        return data;
    }
}
