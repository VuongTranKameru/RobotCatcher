using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour, IHaveSameStat
{
    [SerializeField] PlayerManager Manager;

    [SerializeField] StatConfig stat;
    int atk, def, sed;
    AffectSkill affect;
    internal int seCooldown, cooldownStack; //status effect has max 5 turn cooldown
    int isBattle; //check who in battle

    [Header("Skill Set")]
    List<SkillConfig> hasSkills;

    [Header("Robotcatched")]
    [SerializeField] StatConfig[] robotList = new StatConfig[5]; //DO NOT USE PLAYER STAT

    [Header("Itemlooted")]
    [SerializeField] List<ItemConfig> itemList = new();

    #region Callout Stat
    //read only
    public StatConfig PlayerStats() { return stat; }
    public string NameStat() { return stat.nameChar; }
    public List<SkillConfig> ListOfAction() { return hasSkills; }
    public float ChanceToCatch() { return -1; }

    //read, only write when meet condition
    public int AttackStat() { return stat.attack; }
    public int DefenseStat() { return stat.defense; }
    public int SpeedStat() { return stat.speed; }
    public int MaxHPStat() { return stat.maxHP; }
    public int LvStat() { return stat.lv; }
    public StatusEffect StatusEffectState() { return stat.status; }
    public void StatusCooldown() { seCooldown -= 1; }

    //read and write
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
        get { return 0; }
        set { stat.specialPoint = 0; }
    }

    public AffectSkill AFF { get => affect; set => affect = value; }

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

    public void IsInBattle() { isBattle = -1; }
    #endregion

    private void OnEnable()
    {
        atk = stat.attack;
        def = stat.defense;
        sed = stat.speed;
        affect = AffectSkill.Normal;
        ScanExistSkill();

        isBattle = -1;
    }

    private void Update()
    {
        if (seCooldown == 0)
            stat.status = StatusEffect.None;
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

    #region Robot Management
    //read only
    public int AmountOfRobots()
    {
        int count = 0;
        for (int i = 0; i < robotList.Length; i++)
            if (robotList[i] != null)
                count++;
        //result is how many robotcatcher, in human count
        return count;
    }

    public bool CheckAvailableRobot()
    {
        for (int i = 0; i < AmountOfRobots(); i++)
            if (robotList[i] != null && robotList[i].health > 0)
                return true;

        return false;
    }

    public StatConfig ChooseRobot(int num) { return robotList[num]; }
    public StatConfig ChooseRobot(int num, bool inBattle) //use for change into battle
    {
        if (inBattle)
            isBattle = num;
        return robotList[num];
    }

    public StatConfig RemainOnBattle() //check who in battle, use for ownermenu
    {
        if (AmountOfRobots() == 0 || isBattle == -1)
            return stat;
        else return robotList[isBattle];
    }

    //read, only write when meet condition
    public StatConfig FirstPickRobot
    {
        get 
        {
            isBattle = 0;
            return robotList[0]; 
        }
        set 
        {
            if (value != robotList[0])
            {
                StatConfig temp = robotList[0];
                robotList[0] = value;
                for (int i = 0; i < AmountOfRobots(); i++)
                    if (robotList[i] != null && robotList[i].uniqueID == value.uniqueID)
                    {
                        robotList[i] = temp;
                        break;
                    }
            }
        }
    }

    //write only
    public void HackRobotSuccess(StatConfig bot)
    {
        Debug.Log("into here " + bot.nameChar);
        for (int i = 0; i < robotList.Length; i++)
            if (robotList[i] == null)
            {
                robotList[i] = bot;
                break;
            }
        string json = JsonUtility.ToJson(bot, true);
        Manager.AddNewRobot(json);
        //Debug.Log(json);
    }
    #endregion

    #region Item Management
    public ItemConfig ClickOnItem(int num) { return itemList[num]; }
    public void DeleteItem(ItemConfig item) { itemList.Remove(item); }
    public int AmountOfItems()
    {
        int calc = 0;
        for (int i = 0; i < itemList.Count; i++)
            if (itemList[i] != null)
                calc++;

        return calc;
    }

    public void AddItem(ItemConfig item) //has to put in original, not the clone one
    {
        itemList.Add(item);
        string ison = JsonUtility.ToJson(item, true);
        Manager.AddNewItem(ison);
    }
    #endregion

    #region Unused
    public StatConfig CreatingNewRobotcatcher() //DO NOT USE THIS
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
