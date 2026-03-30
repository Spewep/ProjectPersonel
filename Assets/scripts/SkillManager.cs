using UnityEngine;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour
{
    public List<Skill> skills = new List<Skill>();

    public void BuySkill(int index)
    {
        if (index < 0 || index >= skills.Count) return;

        skills[index].Buy();
    }

    public bool HasSkill(string skillName)
    {
        foreach (var skill in skills)
        {
            if (skill.skillName == skillName && skill.isUnlocked)
                return true;
        }

        return false;
    }
}