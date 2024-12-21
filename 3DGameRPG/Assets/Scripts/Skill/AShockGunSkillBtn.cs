using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AShockGunSkillBtn : MonoBehaviour, ICanUseSkill
{
    [Header("Skill Setup")]
    [SerializeField] SkillConfig skill;
    [SerializeField] Image actBtnClr, actChk;
    [SerializeField] TMP_Text actName;

    int tempDamg, bonusShockDmg;
    string changeMes;
    bool isShock;

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
        if (opp.StatusEffectState() == StatusEffect.Shock)
            bonusShockDmg = 2;
        else bonusShockDmg = 1;

        tempDamg = (user.ATKTemp * bonusShockDmg + skill.power) - opp.DEFTemp;
        if (tempDamg > 0)
            opp.HPRemain -= tempDamg;

        if (Random.Range(0, 21) <= 20)
        {
            opp.ReceiveStatusE(StatusEffect.Shock);
            isShock = true;
        }
    }

    string ICanUseSkill.MessageUsedSkill(IHaveSameStat user, IHaveSameStat opp)
    {
        if (tempDamg < 0)
            tempDamg = 0;

        if (isShock)
        {
            changeMes = $"\n{opp.NameStat()} got electrocuted from this action.";
            isShock = false;
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
