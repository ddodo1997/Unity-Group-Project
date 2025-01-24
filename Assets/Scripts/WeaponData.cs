using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Object/Weapon Data", order = int.MaxValue)]
public class WeaponData : ItemData
{
    Skill weaponSkill = new Skill();
    public override void SetStatus(string key)
    {
        base.SetStatus(key);
        //��ų �ʱ�ȭ
        weaponSkill.skill += () => Debug.Log("��ų ���!");
    }
    public void UseSkill()
    {
        weaponSkill.UseSkill();
    }
}
