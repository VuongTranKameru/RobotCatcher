using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AHitSkillBtn : MonoBehaviour, ICanUseSkill
{
    ToggleGroup tgGrp;
    [Header("Skill Setup")]
    [SerializeField] SkillConfig skill;
    [SerializeField] Image actBtnClr, actChk;
    [SerializeField] TMP_Text actName;

    void Awake()
    {
        tgGrp = FindObjectOfType<ToggleGroup>();
        this.GetComponent<Toggle>().group = tgGrp; //tim group cua toggle

        actName.text = skill.skillName;
        actBtnClr.sprite = skill.listBtnClr[0]; //normal color
        actChk.sprite = skill.listBtnClr[1]; //darker color
    }

    public void SkillUsed(RobotStat user, RobotStat opp)
    {
        //hp = hp - (def - ([atk doi phuong] + [power doi phuong])
        opp.HPRemain -= (user.ATKTemp + skill.power) - opp.DEFTemp;
    }

    public string MessageUsedSkill(RobotStat user, RobotStat opp = null)
    {
        return $"{user.NameStat()} use {skill.skillName}! " +
            $"\n{opp.NameStat()} take {(user.ATKTemp + skill.power) - opp.DEFTemp} damage.";
    }
}
