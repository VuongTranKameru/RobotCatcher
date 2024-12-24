using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotStat : MonoBehaviour, IHaveSameStat
{
    [SerializeField] StatConfig stat;
    int atk, def, sed;
    AffectSkill affect;
    internal int seCooldown, cooldownStack; //status effect has max 5 turn cooldown

    [Header("Skill Set")]
    [SerializeField] List<SkillConfig> hasSkills;

    [Header("Chance To Catch")]
    [SerializeField] float chance;

    #region Callout Stat
    //public StatConfig RobotStats() { return stat; }

    //read only
    public string NameStat() { return stat.nameChar; }
    public string DescriptionStat() { return stat.description; }
    public List<SkillConfig> ListOfAction() { return hasSkills; }
    public SkillConfig UsedAction(int num) { return hasSkills[num]; }
    public float ChanceToCatch() { return chance; }

    //read, only write when meet condition
    public int AttackStat() { return stat.attack; }
    public int DefenseStat() { return stat.defense; }
    public int SpeedStat() { return stat.speed; }
    public int MaxHPStat() { return stat.maxHP; }
    public int MaxSPStat() { return stat.maxSP; }
    public int LvStat() { return stat.lv; }
    public StatusEffect StatusEffectState() { return stat.status; }
    public void StatusCooldown() 
    { 
        Debug.Log("bef cooldown" + seCooldown); 
        seCooldown -= 1; 
        Debug.Log("aft low cooldown a bit" + seCooldown); 
    }
    public void LevelUp()
    {
        stat.lv += 1;
        //random stat bonus when leveling
        stat.maxHP += Random.Range(0, 5);
        stat.maxSP += (Random.Range(0, 5) * 5);
        stat.attack += Random.Range(0, 5);
        stat.defense += Random.Range(0, 5);
        stat.speed += Random.Range(0, 5);
        //heal when leveling
        stat.health = stat.maxHP;
        stat.specialPoint = stat.maxSP;
    }

    //read and write
    public StatConfig RobotStats { get => null; set => stat = value; }
    public int HPRemain
    {
        get { return stat.health; }
        set 
        {
            if (value < 0)
                stat.health = 0;
            else if (value > stat.maxHP)
                stat.health = stat.maxHP;
            else stat.health = value;
        }
    }

    public int ATKTemp
    {
        get { return atk; }
        set
        {
            if (value < 0)
                atk = 0;
            else atk = value;
        }
    }

    public int DEFTemp
    {
        get { return def; }
        set
        {
            if (value < 0)
                def = 0;
            else def = value;
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

    public int SPRemain
    {
        get { return stat.specialPoint; }
        set
        {
            if (value >= stat.maxSP)
                stat.specialPoint = stat.maxSP;
            else stat.specialPoint = value;
        }
    }

    public AffectSkill AFF 
    {
        get => affect;
        set => affect = value;
    }

    public void ReceiveStatusE(StatusEffect status)
    {
        if (stat.status != status)
            seCooldown = 0;

        stat.status = status;
        
        cooldownStack = Random.Range(1, 4);
        seCooldown += cooldownStack;
        if (seCooldown > 5)
            seCooldown = 5;
    }
    #endregion

    private void Update()
    {
        if (seCooldown == 0)
            stat.status = StatusEffect.None;
    }

    public void CallOutTempStat() //only in battle
    {
        atk = stat.attack;
        def = stat.defense;
        sed = stat.speed;
        affect = AffectSkill.Normal;
        SPRemain = 0;
        ScanExistSkill();
    }

    void ScanExistSkill()
    {
        hasSkills = new List<SkillConfig>();
        for (int i = 0; i < stat.learnableSkills.Length; i++)
        {
            if(stat.learnableSkills[i].atLevel <= stat.lv)
                hasSkills.Add(stat.learnableSkills[i].learnableSkill);
        }
    }

    public StatConfig CreatingNewRobotcatcher()
    {
        StatConfig newBot = ScriptableObject.CreateInstance<StatConfig>();
        newBot.SaveRobotcatcher(stat.Itself(), stat.Avatar(), stat.charID, NameStat(), DescriptionStat(),
            MaxHPStat(), MaxSPStat(), AttackStat(), DefenseStat(), SpeedStat(), LvStat(), stat.learnableSkills, stat.learnableSkills.Length);

        /*string json = JsonUtility.ToJson(newBot, true);
        Debug.Log(json);

        StatConfig newone = ScriptableObject.CreateInstance<StatConfig>();
        JsonUtility.FromJsonOverwrite(json, newone);
        Debug.Log(newone.Itself() + " & skil: " + newone.learnableSkills[0].learnableSkill.skillName);*/

        return newBot;
    }
}
