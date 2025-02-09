using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData
{
    public string SkillId { get; set; }
    public string SkillName { get; set; }
    public string SkillEffectId { get; set; }
    public ParticleSystem skillEffect;
    public string SkillIconId { get; set; }
    public Sprite skillIcon;
    public int Damage { get; set; }
    public float NumberOfTriggers { get; set; }
    public float HoldingTime { get; set; }
    public float xRange { get; set; }
    public float yRange { get; set; }
    public float CoolTime { get; set; }

    public delegate void Skill(Player player);
    public Skill skill;
    public SkillData GetNewData()
    {
        SkillData skillData = new SkillData();
        skillData.SkillId = SkillId;
        skillData.SkillName = SkillName;
        skillData.SkillEffectId = SkillEffectId;
        //skillEffect
        var path = string.Format(PathFormats.particles, SkillEffectId);
        skillEffect = (ParticleSystem)Resources.Load(path);
        skillData.SkillIconId = SkillIconId;
        //skillIcon
        path = string.Format(PathFormats.particles, SkillIconId);
        skillIcon = (Sprite)Resources.Load(path);
        skillData.Damage = Damage;
        skillData.NumberOfTriggers = NumberOfTriggers;
        skillData.HoldingTime = HoldingTime;
        skillData.xRange = xRange;
        skillData.yRange = yRange;
        skillData.CoolTime = CoolTime;

        return skillData;
    }

    public void InitSkill(Player player)
    {
        //���� ���
        {
            skill = (player) =>
            {
                PlayEffect(player);
            };
        }
        //ī����
        {
            skill = (player) =>
            {
                PlayEffect(player);
            };
        }
        //����
        {
            skill = (player) =>
            {
                PlayEffect(player);
            };
        }
        //���� ����
        {
            skill = (player) =>
            {
                PlayEffect(player);
            };
        }
        //���յ�
        {
            skill = (player) =>
            {
                PlayEffect(player);
            };
        }
        //����
        {
            skill = (player) =>
            {
                PlayEffect(player);
            };
        }
    }

    public void PlayEffect(Player player)
    {
        skillEffect.gameObject.transform.position = player.transform.position;
        skillEffect.Play();
    }
}
