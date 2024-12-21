using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ATemperedSlamSkillBtn : MonoBehaviour, ICanUseSkill
{
    [Header("Skill Setup")]
    [SerializeField] SkillConfig skill;
    [SerializeField] Image actBtnClr, actChk;
    [SerializeField] TMP_Text actName;

    int tempDamg;
    string changeMes;

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
        tempDamg = (user.DEFTemp) - opp.DEFTemp;
        if (tempDamg > 0)
            opp.HPRemain -= tempDamg;

        if (user.DefenseStat() < user.DEFTemp)
            user.DEFTemp -= (user.DEFTemp - user.DefenseStat()); //tro ve def nguyen goc
        else user.DEFTemp -= Mathf.RoundToInt(user.DEFTemp * (skill.power/100f)); //tru half def
    }

    string ICanUseSkill.MessageUsedSkill(IHaveSameStat user, IHaveSameStat opp)
    {
        if (tempDamg < 0)
            tempDamg = 0;

        if (user.DefenseStat() == user.DEFTemp)
            changeMes = $"\n{user.NameStat()} is lower its defense to its origin.";
        else changeMes = $"\n{user.NameStat()} is lower its defense to half.";

        return $"{user.NameStat()} used {skill.skillName}! " +
            $"\n{opp.NameStat()} take {tempDamg} damage." + changeMes;
    }

    string ICanUseSkill.MessageActionOnly(IHaveSameStat user)
    {
        return $"{user.NameStat()} used {skill.skillName}! ";
    }
}
