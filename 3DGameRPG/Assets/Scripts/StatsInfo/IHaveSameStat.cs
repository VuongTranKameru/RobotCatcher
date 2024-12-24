using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface IHaveSameStat
{
    #region Callout Stat
    //read only
    public string NameStat();
    public List<SkillConfig> ListOfAction();
    public float ChanceToCatch();
    public StatConfig CreatingNewRobotcatcher();

    //read, only write when meet condition
    public int AttackStat();
    public int DefenseStat();
    public int SpeedStat();
    public int MaxHPStat();
    public int LvStat();
    public StatusEffect StatusEffectState();
    public void StatusCooldown();

    //read and write
    public int HPRemain { get; set; }
    public int ATKTemp { get; set; }
    public int DEFTemp { get; set; }
    public int SPETemp { get; set; }
    public int SPRemain { get; set; }
    public AffectSkill AFF { get; set; }
    public void ReceiveStatusE(StatusEffect status);
    #endregion
}
