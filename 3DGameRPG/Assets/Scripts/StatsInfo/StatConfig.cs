using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LearnableSkills
{
    [SerializeField] internal SkillConfig learnableSkill;
    [SerializeField] internal int atLevel;
}

[CreateAssetMenu(fileName = "StatScriptData", menuName = "ScriptableObjects/StatData", order = 1)]
public class StatConfig : ScriptableObject
{
    [Header("Prefab")]
    [SerializeField] GameObject itself;
    public GameObject Itself() { return itself; }

    [Header("Information")]
    [SerializeField] internal string charID;
    [SerializeField] internal string nameChar, description;

    [Header("Stats Number")]
    [SerializeField] internal int maxHP;
    [SerializeField] internal int attack, defense, speed, lv, maxSP;
    internal int health, specialPoint;
    internal float exp;

    [Header("Can Used Skill")]
    [SerializeField] internal LearnableSkills[] learnableSkills;

    //bool statusEffect;
    /*Burn: lost hp each turn, defense low
      Shock: lost 1-2 turns
      Waterlogged: lost hp, got double dmg when eletric*/
}
