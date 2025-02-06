using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMonsterHpBar : MonoBehaviour
{
    public BossMonster monster;
    private Slider hpBar;
    private void Start()
    {
        hpBar = GetComponent<Slider>();
        UpdateHpBar(monster.status);
    }
    public void UpdateHpBar(MonsterStatus status)
    {
        SetMaxHp(status);
        SetCurrentHp(status);
    }
    public void SetMaxHp(MonsterStatus status)
    {
        hpBar.maxValue = status.Health;
    }
    public void SetCurrentHp(MonsterStatus status)
    {
        hpBar.value = status.hp;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
