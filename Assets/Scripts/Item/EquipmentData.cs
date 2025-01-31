using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmorType
{
    Helmet,
    Armor,
    Shoes,
    Cloak,
    Ring,
};

public class EquipmentData : ItemData
{
    public ArmorType Type { get; set; }
    public override void SetStatus(ItemData data)
    {
        base.SetStatus(data);
        Type = (data as EquipmentData).Type;
    }

    public override void SetStatus(string key)
    {
        base.SetStatus(key);


    }
}
