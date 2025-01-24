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
public enum ItemStatus
{
    Weapon,
    Helmet,
    Armor,
    Acceaccessorie,
    Cloak,
    Shoose,
}

public static class DataTableIds
{
    public static readonly string[] EntityStatus =
    {
        "PlayerStatusTable",
        "MonsterStatusTable"
    };
    public static readonly string[] ItemStatus =
    {
        "ItemTable"
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
}

public static class Tags
{
    public static readonly string Player = "Player";
    public static readonly string Monster = "Monster";
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