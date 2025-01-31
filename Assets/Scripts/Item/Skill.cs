using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public event System.Action skill;

    public void UseSkill()
    {
        skill?.Invoke();
    }
}
