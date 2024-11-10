using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour, IHaveSameStat
{
    static PlayerStat instance;
    [SerializeField] StatConfig stat;
    int atk, def, sed;

    [Header("Skill Set")]
    [SerializeField] List<SkillConfig> hasSkills;

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

    public int SPETemp
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

    private void Start()
    {
        //dung static de xac dinh duy nhat 1 player ton tai, ko bi nhan len
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //ko huy player khi chuyen scene
        DontDestroyOnLoad(gameObject);
    }

    void ScanExistSkill()
    {
        hasSkills = new List<SkillConfig>();
        for (int i = 0; i < stat.learnableSkills.Length; i++)
        {
            if (stat.learnableSkills[i].atLevel <= stat.lv)
                hasSkills.Add(stat.learnableSkills[i].learnableSkill);
        }
    }
}
