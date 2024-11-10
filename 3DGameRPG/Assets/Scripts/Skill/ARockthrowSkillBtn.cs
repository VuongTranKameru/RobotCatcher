using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ARockthrowSkillBtn : MonoBehaviour, ICanUseSkill
{
    [Header("Skill Setup")]
    [SerializeField] SkillConfig skill;
    [SerializeField] Image actBtnClr, actChk;
    [SerializeField] TMP_Text actName;

    int tempDamg;

    void Awake()
    {
        actName.text = skill.skillName;
        actBtnClr.sprite = skill.listBtnClr[0]; //normal color
        actChk.sprite = skill.listBtnClr[1]; //darker color
    }

    void ICanUseSkill.SkillUsed(IHaveSameStat user, IHaveSameStat opp)
    {
        int totalATK = Random.Range(0, 6) * skill.power;
        //Debug.Log("rock: " + totalATK);

        tempDamg = (totalATK + user.ATKTemp) - opp.DEFTemp;
        //hp = hp - (def - ([atk doi phuong] + [power doi phuong])
        if (tempDamg > 0)
            opp.HPRemain -= tempDamg;
    }

    string ICanUseSkill.MessageUsedSkill(IHaveSameStat user, IHaveSameStat opp)
    {
        if (tempDamg < 0)
            tempDamg = 0;

        return $"{user.NameStat()} use {skill.skillName}! " +
            $"\n{opp.NameStat()} take {tempDamg} damage.";
    }
}
