using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] StatConfig stat;
    int atk, def, sed;

    [Header("Skill Set")]
    List<SkillConfig> hasSkills;

    #region Callout Stat
    //read only
    public string NameStat() { return stat.nameChar; }
    public List<SkillConfig> ListOfAction() { return hasSkills; }

    //read, only write when meet condition
    public int AttackStat() { return stat.attack; }
    public int DefenseStat() { return stat.defense; }
    public int SpeedStat() { return stat.speed; }
    public int MaxHPStat() { return stat.maxHP; }
    public int MaxSPStat() { return stat.maxSP; }
    public int LvStat() { return stat.lv; }

    //read and write
    public int HPRemain
    {
        get { return stat.health; }
        set
        {
            if (value < 0)
                stat.health = 0;
            else stat.health = value;
        }
    }

    public int ATKTemp
    {
        get { return atk; }
        set
        {
            if (value + atk != stat.attack)
                atk += value;
        }
    }

    public int DEFTemp
    {
        get { return def; }
        set
        {
            if (value + def != stat.defense)
                def += value;
        }
    }

    public int SEDTemp
    {
        get { return sed; }
        set
        {
            if (value + sed != stat.speed)
                sed += value;
        }
    }
    #endregion

    private void Awake()
    {
        atk = stat.attack;
        def = stat.defense;
        sed = stat.speed;
        ScanExistSkill();
    }

    void ScanExistSkill()
    {
        hasSkills = new List<SkillConfig>();
        for (int i = 0; i < stat.learnableSkills.Length; i++)
        {
            if (stat.learnableSkills[i].atLevel == stat.lv)
                hasSkills.Add(stat.learnableSkills[i].learnableSkill);
        }
    }
}
