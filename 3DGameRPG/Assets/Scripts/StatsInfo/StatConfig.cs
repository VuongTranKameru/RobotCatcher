using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LearnableSkills
{
    [SerializeField] internal SkillConfig learnableSkill;
    [SerializeField] internal int atLevel;
}

public enum StatusEffect
{
    None,
    Overheat,
    Shock
}

[CreateAssetMenu(fileName = "StatScriptData", menuName = "ScriptableObjects/StatData", order = 1)]
public class StatConfig : ScriptableObject
{
    [Header("Prefab")]
    [SerializeField] GameObject itself;
    [SerializeField] Sprite portrait;
    public GameObject Itself() { return itself; }
    public Sprite Avatar() { return portrait; }

    [Header("Information")]
    [SerializeField] internal string charID;
    [SerializeField] internal string uniqueID, nameChar, description;

    [Header("Stats Number")]
    [SerializeField] internal int maxHP;
    [SerializeField] internal int attack, defense, speed, lv, maxSP;
    [SerializeField] internal float expStandard;
    internal int health, specialPoint, expProgress;

    //bool statusEffect;
    /*Overheat: lost hp each turn, defense low
      Shock: lost 1-2 turns
      Waterlogged: lost hp, got double dmg when eletric
      --decide to fuse shock with waterlogged, receive double char atk when eletric or water. overheat only lost hp*/
    [Header("Status Effect")]
    [SerializeField] internal StatusEffect status;

    [Header("Can Used Skill")]
    [SerializeField] internal LearnableSkills[] learnableSkills;

    public void SaveRobotcatcher(GameObject prefab, Sprite image, string id, string name, string des, int mHP, int mSP,
        int atk, int def, int spe, int level, LearnableSkills[] learnSkils, int learnSkiLimit)
    {
        itself = prefab;
        portrait = image;
        charID = id;
        nameChar = name;
        description = des;
        maxHP = mHP; health = maxHP;
        maxSP = mSP;
        attack = atk;
        defense = def;
        speed = spe;
        lv = level;

        DateTime catchTime = DateTime.Now;
        uniqueID = id + "-" + catchTime.Year.ToString() + catchTime.Month.ToString() + catchTime.Day.ToString() + "-" +
            catchTime.Hour.ToString() + catchTime.Minute.ToString() + catchTime.Second.ToString();

        expStandard = 50;

        learnableSkills = new LearnableSkills[learnSkiLimit];
        for (int i = 0; i < learnSkiLimit; i++)
            learnableSkills[i] = learnSkils[i];
    }

    public void AddingExp(int expPoint)
    {
        expProgress += expPoint;
        if (expProgress >= (expStandard * lv))
            LevelUp();
    }

    void LevelUp()
    {
        lv += 1;
        //random stat bonus when leveling
        maxHP += UnityEngine.Random.Range(0, 5);
        maxSP += (UnityEngine.Random.Range(0, 5) * 5);
        attack += UnityEngine.Random.Range(0, 5);
        defense += UnityEngine.Random.Range(0, 5);
        speed += UnityEngine.Random.Range(0, 5);
        //heal when leveling
        health = maxHP;
        specialPoint = maxSP;
    }
}
