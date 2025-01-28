using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Language
{
    Korea,
}
public enum EntityStatus
{
    Player,
    Monsters,
}
public enum EquipType
{
    Armor,
    Weapon
};

public static class DataTableIds
{
    public static readonly string[] EntityStatus =
    {
        "PlayerStatusTable",
        "MonsterStatusTable"
    };
    public static readonly string[] ItemStatus =
    {
        "ArmorTable",
        "WeaponTable"
    };
}
public static class PathFormats
{
    public static readonly string prefabs = "prefabs/{0}";
    public static readonly string tables = "tables/{0}";
}

public static class Variables
{
    public static Language currentLang = Language.Korea;
    public static readonly int MaxDropCnt = 3;
}

public static class Direction
{
    public static readonly Quaternion Left = Quaternion.Euler(0, Mathf.Atan2(0, 1) * Mathf.Rad2Deg, 0);
    public static readonly Quaternion Right = Quaternion.Euler(0, Mathf.Atan2(0, -1) * Mathf.Rad2Deg, 0);
}

public static class Tags
{
    public static readonly string Player = "Player";
    public static readonly string Monster = "Monster";
    public static readonly string InventoryManager = "InventoryManager";
}

public static class SortingLayers
{
    public static readonly string Default = "Default";
}
public static class Layers
{
    public static readonly string Default = "Default";
    public static readonly string UI = "UI";
}