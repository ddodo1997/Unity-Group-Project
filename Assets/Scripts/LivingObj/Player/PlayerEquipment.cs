using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

enum EquipSlots
{
    Helmet,
    Armor,
    Shoes,
    Cloak,
    Ring,
    Weapon
}
public class PlayerEquipment : MonoBehaviour
{
    public GameObject skillButton;
    public InventoryManager inventoryManager;
    public PlayerHpBar hpBar;
    public EquipmentSlot[] equipSlots;
    public Player player;
    public TextMeshProUGUI[] statTexts;
    private bool skillCoolTime = true;
    private void Start()
    {
        player = GetComponent<Player>();
        inventoryManager = GameObject.FindGameObjectWithTag(Tags.InventoryManager).GetComponent<InventoryManager>();
    }

    public void OnEquipItem(ItemData item)
    {
        if (item is EquipmentData)
        {
            var equip = (item as EquipmentData);
            if (!equipSlots[(int)equip.Type].itemData.IsEmpty)
            {
                var tempEquip = equipSlots[(int)equip.Type];
                inventoryManager.items.Add(tempEquip.itemData);
            }
            equipSlots[(int)equip.Type].SetData(ref item);
            inventoryManager.items.Remove(item);
        }
        else if (item is WeaponData)
        {
            var currentWeapon = item as WeaponData;
            switch (player.status.className)
            {
                case ClassName.Warrior:
                    if (currentWeapon.Type != WeaponType.Sword)
                    {
                        return;
                    }
                    break;
                case ClassName.Archer:
                    if (currentWeapon.Type != WeaponType.Staff)
                    {
                        return;
                    }
                    break;
                case ClassName.Sorcerer:
                    if (currentWeapon.Type != WeaponType.Bow)
                    {
                        return;
                    }
                    break;
            }
            if (!equipSlots[(int)EquipSlots.Weapon].itemData.IsEmpty)
            {
                var tempWeapon = equipSlots[(int)EquipSlots.Weapon].itemData; ;
                inventoryManager.items.Add(tempWeapon);
            }
            if (currentWeapon.Rate < EquipRate.Hero)
                skillButton.GetComponent<Image>().sprite = null;
            else
                skillButton.GetComponent<Image>().sprite = currentWeapon.skill?.skillIcon;
            equipSlots[(int)EquipSlots.Weapon].SetData(ref item);
            inventoryManager.items.Remove(item);
            player.weaponRenderer.sprite = equipSlots[(int)EquipSlots.Weapon].image.sprite;
        }
        inventoryManager.SetCurrentItem();

        player.status.SetStatus(ref equipSlots);
        player.StatusBasedSetting();
        UpdateStatusText();
        hpBar.UpdateHpBar(player.status);
    }

    public void UpdateStatusText()
    {
        statTexts[0].text = $"Strength : {player.status.Strength}";
        statTexts[1].text = $"Intelligence : {player.status.Intelligence}";
        statTexts[2].text = $"Agillity : {player.status.Agility}";
        statTexts[3].text = $"Luck : {player.status.Luck}";
        statTexts[4].text = $"Health : {player.status.Health}";
        statTexts[5].text = $"Critical : {player.status.Critical}";
    }

    public void TouchSkillButton()
    {
        var currentWeapon = equipSlots[(int)EquipSlots.Weapon].itemData as WeaponData;
        if (currentWeapon == null && currentWeapon.IsEmpty)
        {
            return;
        }
        if (!skillCoolTime)
            return;
        StartCoroutine(SkillCoolTime(currentWeapon));
        var effect = Instantiate(currentWeapon.skill.skillEffect, player.transform.position, Quaternion.identity);
        effect.GetComponent<Skill>().SetData(currentWeapon.skill);
    }

    public IEnumerator SkillCoolTime(WeaponData weapon)
    {
        skillCoolTime = false;
        skillButton.GetComponent<Image>().color = Color.gray;
        yield return new WaitForSeconds(weapon.skill.CoolTime);
        skillButton.GetComponent<Image>().color = Color.white;
        skillCoolTime = true;
    }
}