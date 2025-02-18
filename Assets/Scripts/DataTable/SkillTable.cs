using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.Linq;

public class SkillTable : DataTable
{
    private readonly Dictionary<string, SkillData> dictionary = new Dictionary<string, SkillData>();

    public override void Load(string fileName)
    {
        var path = string.Format(PathFormats.tables, fileName);

        var textAsset = Resources.Load<TextAsset>(path);

        var list = LoadCSV<SkillData>(textAsset.text);

        dictionary.Clear();
        foreach (var skill in list)
        {
            if (!dictionary.ContainsKey(skill.SkillId))
            {
                dictionary.Add(skill.SkillId, skill);
            }
            else
            {
                Debug.Log($"{skill.SkillId} is ม฿บน!");
            }
        }
    }

    public SkillData Get(string key)
    {
        if (!dictionary.ContainsKey(key))
        {
            return null;
        }
        return dictionary[key];
    }
}
