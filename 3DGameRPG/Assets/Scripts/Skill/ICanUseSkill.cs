using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface ICanUseSkill
{
    public void SkillUsed(IHaveSameStat user, IHaveSameStat opp);
    public int CostOfSP();
    public TypeOfSkill Type();
    public string MessageUsedSkill(IHaveSameStat user, IHaveSameStat opp = null);
    public string MessageActionOnly(IHaveSameStat user);
}
