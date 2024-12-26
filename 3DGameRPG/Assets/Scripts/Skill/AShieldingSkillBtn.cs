using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AShieldingSkillBtn : MonoBehaviour, ICanUseSkill
{
    [Header("Skill Setup")]
    [SerializeField] SkillConfig skill;
    [SerializeField] Image actBtnClr, actChk;
    [SerializeField] TMP_Text actName;

    public int CostOfSP() { return skill.spUsed; }
    public TypeOfSkill Type() { return skill.type; }

    void Awake()
    {
        actName.text = skill.skillName;
        actBtnClr.sprite = skill.listBtnClr[0]; //normal color
        actChk.sprite = skill.listBtnClr[1]; //darker color
    }

    void ICanUseSkill.SkillUsed(IHaveSameStat user, IHaveSameStat opp)
    {
        //buff defense tam thoi len
        user.DEFTemp += skill.power;
    }

    string ICanUseSkill.MessageUsedSkill(IHaveSameStat user, IHaveSameStat opp)
    {
        return $"{user.NameStat()} used {skill.skillName}! " +
            $"\n{user.NameStat()} is increasing defense slightly.";
    }

    string ICanUseSkill.MessageActionOnly(IHaveSameStat user)
    {
        return $"{user.NameStat()} used {skill.skillName}! ";
    }
}
