using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI hpText;
    private Slider hpBar;
    private void Start()
    {
        hpBar = GetComponent<Slider>();
        UpdateHpBar(player.status);
    }
    public void UpdateHpBar(PlayerStatus status)
    {
        SetMaxHp(status);
        SetCurrentHp(status);
        SetText(status);
    }
    public void SetMaxHp(PlayerStatus status)
    {
        hpBar.maxValue = status.Health;
    }
    public void SetCurrentHp(PlayerStatus status)
    {
        hpBar.value = status.hp;
    }
    public void SetText(PlayerStatus status)
    {
        hpText.text = $"{hpBar.value} / {hpBar.maxValue}";
    }

}
