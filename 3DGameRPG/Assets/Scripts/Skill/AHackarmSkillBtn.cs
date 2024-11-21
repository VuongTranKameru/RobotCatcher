using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AHackarmSkillBtn : MonoBehaviour, ICanUseSkill
{
    [Header("Skill Setup")]
    [SerializeField] SkillConfig skill;
    [SerializeField] Image actBtnClr, actChk;
    [SerializeField] TMP_Text actName;

    [Header("Direct To Player")]
    [SerializeField] UnityEvent<StatConfig> HackedRobot;
    bool isSuccess;

    public int CostOfSP() { return 0; }

    void Awake()
    {
        actName.text = skill.skillName;
        actBtnClr.sprite = skill.listBtnClr[0]; //normal color
        actChk.sprite = skill.listBtnClr[1]; //darker color
    }

    void ICanUseSkill.SkillUsed(IHaveSameStat user, IHaveSameStat opp)
    {
        CatchingRobot(opp);
        if (isSuccess)
            HackedRobot?.Invoke(opp.CreatingNewRobotcatcher()); //not finish yet
    }

    string ICanUseSkill.MessageUsedSkill(IHaveSameStat user, IHaveSameStat opp)
    {
        if (isSuccess)
            return $"{user.NameStat()} use {skill.skillName}! " +
            $"\n{user.NameStat()} capture {opp.NameStat()} successfully.";
        else return $"{user.NameStat()} use {skill.skillName}! " +
            $"\n{user.NameStat()} fail to capture {opp.NameStat()}.";
    }

    string ICanUseSkill.MessageActionOnly(IHaveSameStat user)
    {
        return $"{user.NameStat()} use {skill.skillName}! ";
    }

    void CatchingRobot(IHaveSameStat bot)
    {
        float hpLost = bot.MaxHPStat() - bot.HPRemain;
        float hitRate = (float)System.Math.Round(Random.Range(0f, 100f), 2) - (hpLost/bot.MaxHPStat())*10f;
        Debug.Log("rate:" + hitRate + " & " + hpLost / bot.MaxHPStat() * 10f);
        if (hitRate <= bot.ChanceToCatch())
        {
            bot.HPRemain = 0;
            isSuccess = true;
        }
        else isSuccess = false;
    }
}
