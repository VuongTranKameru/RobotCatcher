using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AHeatWealdingSkillBtn : MonoBehaviour, ICanUseSkill
{
    [Header("Skill Setup")]
    [SerializeField] SkillConfig skill;
    [SerializeField] Image actBtnClr, actChk;
    [SerializeField] TMP_Text actName;

    int tempDamg, bonusShockDmg;
    string changeMes;
    bool isBurn;

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
        tempDamg = (user.ATKTemp + skill.power) - opp.DEFTemp;
        if (tempDamg > 0)
            opp.HPRemain -= tempDamg;

        if (Random.Range(0, 11) < 8)
        {
            opp.ReceiveStatusE(StatusEffect.Overheat);
            isBurn = true;
        }
    }

    string ICanUseSkill.MessageUsedSkill(IHaveSameStat user, IHaveSameStat opp)
    {
        if (tempDamg < 0)
            tempDamg = 0;

        if (isBurn)
        {
            changeMes = $"\n{opp.NameStat()} got burnt from this action.";
            isBurn = false;
        }
        else changeMes = "";

        return $"{user.NameStat()} used {skill.skillName}! " +
            $"\n{opp.NameStat()} take {tempDamg} damage." + changeMes;
    }

    string ICanUseSkill.MessageActionOnly(IHaveSameStat user)
    {
        return $"{user.NameStat()} used {skill.skillName}! ";
    }
}
