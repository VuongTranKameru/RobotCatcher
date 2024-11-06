using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface ICanUseSkill
{
    public void SkillUsed(RobotStat user, RobotStat opp);
    public string MessageUsedSkill(RobotStat user, RobotStat opp = null);
}
