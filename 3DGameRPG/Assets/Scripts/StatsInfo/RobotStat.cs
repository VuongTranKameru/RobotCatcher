using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotStat : MonoBehaviour
{
    [SerializeField] StatConfig stat;
    int atk, def, sed;

    [Header("Skill Set")]
    [SerializeField] internal Toggle[] actionToggle;

    #region Callout Stat
    //public StatConfig RobotStats() { return stat; }

    //read only
    public string NameStat() { return stat.nameChar; }
    public string DescriptionStat() { return stat.description; }

    //read, only write when meet condition
    public int AttackStat() { return stat.attack; }
    public int DefenseStat() { return stat.defense; }
    public int SpeedStat() { return stat.speed; }
    public int MaxHPStat() { return stat.maxHP; }
    public int MaxSPStat() { return stat.maxSP; }
    public int LvStat() { return stat.lv; }
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
                atk = atk + value;
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
    }
}
