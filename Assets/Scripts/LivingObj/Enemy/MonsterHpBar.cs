using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : MonoBehaviour
{
    public Monster monster;
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
        if (hpBar != null)
            hpBar.maxValue = status.Health;
    }
    public void SetCurrentHp(MonsterStatus status)
    {
        if (hpBar != null)
            hpBar.value = status.hp;
    }

    public void SetPosition(Vector3 position)
    {
        if (gameObject != null && gameObject.activeSelf)
            transform.position = position;
    }
}
