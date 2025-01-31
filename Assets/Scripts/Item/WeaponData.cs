using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
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
}
