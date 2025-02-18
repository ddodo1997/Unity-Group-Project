using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    private Player player;
    private ParticleSystem skillEffect;
    private SkillData data;
    private void Awake()
    {
        skillEffect = GetComponent<ParticleSystem>();
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>();
    }
    private void Start()
    {
        player.PlayAttackAnim();
        if (player.transform.rotation.y == 0)
        {
            if (data.SkillEffectId != "Ef_1_blue")
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            if (data.SkillEffectId != "Ef_1_blue")
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        switch (data.SkillEffectId)
        {
            case "Ef_1_blue":
            case "Plazma":
            case "Ef_14_green_p":
                if (player.transform.rotation.y == 0)
                {
                    transform.position = new Vector3(player.transform.position.x - data.xRange * 0.5f, transform.position.y + 0.5f, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(player.transform.position.x + data.xRange * 0.5f, transform.position.y + 0.5f, transform.position.z);
                }
                break;
            case "Explosion_1":
                var nextPos = Vector3.one;
                if (player.transform.rotation.y == 0)
                {
                    nextPos = new Vector3(transform.position.x - data.xRange * 0.5f, player.transform.position.y, player.transform.position.z);
                }
                else
                {
                    nextPos = new Vector3(transform.position.x + data.xRange * 0.5f, player.transform.position.y, player.transform.position.z);
                }
                var cols = Physics2D.OverlapPointAll(nextPos);
                foreach (var col in cols)
                {
                    if (col.CompareTag("NotMoveableArea"))
                    {
                        return;
                    }
                }
                player.transform.position = nextPos;

                break;
            case "Ef_19_normal_p":
                transform.position = new Vector3(player.transform.position.x, transform.position.y + 0.5f, transform.position.z);
                break;
        }
    }
    private void Update()
    {
        if (!skillEffect.isPlaying)
            Destroy(gameObject);
    }

    public void SetData(SkillData data)
    {
        this.data = data;
        transform.localScale = new Vector3(this.data.xRange, this.data.yRange, 1);
    }
    public void SetData()
    {
        data = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Monster) && !collision.isTrigger)
        {
            var monster = collision.GetComponent<Monster>();

            if (monster != null && !monster.isDie)
            {
                monster.OnDamage(data.Damage);
                switch (data.SkillEffectId)
                {
                    case "Ef_14_green_p":
                        if (monster.currentEffect != Monster.StatusEffect.Poison)
                            monster.StartCoroutine(monster.Poison(data.Damage, data.NumberOfTriggers));
                        break;
                    case "Electricity_ef_13":
                        if (monster.currentEffect != Monster.StatusEffect.UnAttackable)
                            monster.StartCoroutine(monster.UnAttackable());
                        player.OnHeal(player.status.Health * 0.15f, true);
                        break;
                    case "Ef_19_normal_p":
                        if (monster.currentEffect != Monster.StatusEffect.Stunning)
                            monster.StartCoroutine(monster.Stunning());
                        break;
                    case "Plazma":
                        if(player.transform.rotation.y == 0)
                            monster.transform.position = new Vector3( monster.transform.position.x - (data.xRange * 1.5f), monster.transform.position.y, monster.transform.position.z );
                        else
                            monster.transform.position = new Vector3(monster.transform.position.x + (data.xRange * 1.5f), monster.transform.position.y, monster.transform.position.z);


                        break;
                }
            }
        }

        if (collision.CompareTag(Tags.Elite) && !collision.isTrigger)
        {
            var monster = collision.GetComponent<EliteMonster>();

            if (monster != null && !monster.isDie)
            {
                monster.OnDamage(data.Damage);

                switch (data.SkillEffectId)
                {
                    case "Ef_14_green_p":
                        if (monster.currentEffect != EliteMonster.StatusEffect.Poison)
                            monster.StartCoroutine(monster.Poison(data.Damage, data.NumberOfTriggers));
                        break;
                    case "Electricity_ef_13":
                        if (monster.currentEffect != EliteMonster.StatusEffect.UnAttackable)
                            monster.StartCoroutine(monster.UnAttackable());
                        player.OnHeal(player.status.Health * 0.15f, true);
                        break;
                    case "Ef_19_normal_p":
                        if (monster.currentEffect != EliteMonster.StatusEffect.Stunning)
                            monster.StartCoroutine(monster.Stunning());
                        break;
                }
            }
        }
    }
}
