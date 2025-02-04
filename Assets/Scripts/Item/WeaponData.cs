using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None,
    Sword,
    Staff,
    Bow
}

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Object/Weapon Data", order = int.MaxValue)]
public class WeaponData : ItemData
{
    public WeaponType Type {  get; set; }

    Skill weaponSkill = new Skill();
    public override void SetStatus(string key)
    {
        base.SetStatus(key);
        //스킬 초기화
        weaponSkill.skill += () => Debug.Log("스킬 사용!");
    }
    public void UseSkill()
    {
        weaponSkill.UseSkill();
    }

    public WeaponData GetNewData()
    {
        WeaponData data = new WeaponData();
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
