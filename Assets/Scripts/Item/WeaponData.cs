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

public class WeaponData : ItemData
{
    public WeaponType Type {  get; set; }
    public string SkillId { get; set; }

    public SkillData skill;

    public override ItemData GetNewData()
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
        data.SkillId = SkillId;
        data.skill = DataTableManager.SkillTable.Get(SkillId)?.GetNewData() ?? null;
        return data;
    }

    public override void SetStatusForLevel()
    {
        base.SetStatusForLevel();
        if (skill == null)
        {
            return;
        }
        SkillData skillData = DataTableManager.SkillTable.Get(skill?.SkillId) ?? null;
        skill.Damage = skillData.Damage + (int)(skillData.Damage * 0.2 * Level);
        Debug.Log("WeaponLevelUp");
        Debug.Log(skill.Damage);
    }
}
