using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public InventoryManager inventoryManager;
    [SerializeField] public EquipmentData[] armorDatas;
    [SerializeField] public WeaponData weaponData;
    private Player player;
    private void Start()
    {
        player = GetComponent<Player>();
        armorDatas = new EquipmentData[5];
        weaponData = new WeaponData();
        inventoryManager = GameObject.FindGameObjectWithTag(Tags.InventoryManager).GetComponent<InventoryManager>();
    }

    public void OnEquipItem(ItemData item)
    {
        if (item is EquipmentData)
        {
            var equip = (item as EquipmentData);
            if (armorDatas[(int)equip.Type] != null)
            {
                var tempEquip = armorDatas[(int)equip.Type];
                inventoryManager.items.Add(tempEquip);
            }
            armorDatas[(int)equip.Type] = equip;
            inventoryManager.items.Remove(equip);
        }
        else if (item is WeaponData)
        {
            var currentWeapon = item as WeaponData; 
            if (weaponData != null)
            {
                var tempWeapon = weaponData;
                inventoryManager.items.Add(tempWeapon);
            }
            switch (player.status.className)
            {
                case ClassName.Warrior:
                    if (currentWeapon.Type == WeaponType.Sword)
                        weaponData = currentWeapon;
                    inventoryManager.items.Remove(currentWeapon);
                    break;
                case ClassName.Archer:
                    if (currentWeapon.Type == WeaponType.Staff)
                        weaponData = currentWeapon;
                    inventoryManager.items.Remove(currentWeapon);
                    break;
                case ClassName.Sorcerer:
                    if (currentWeapon.Type == WeaponType.Bow)
                        weaponData = currentWeapon;
                    inventoryManager.items.Remove(currentWeapon);
                    break;
            }

        }
        inventoryManager.UpdateSlots();
        inventoryManager.SetCurrentItem();
        player.status.SetStatus(ref armorDatas, weaponData);
    }


}
