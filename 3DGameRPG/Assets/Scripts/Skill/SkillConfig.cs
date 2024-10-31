using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfSkill
{
    RegularDmg,
    SpecialPointDmg,
    AffectCst
}

[CreateAssetMenu(fileName = "SkillScriptData", menuName = "ScriptableObjects/SkillData", order = 2)]
public class SkillConfig : ScriptableObject
{
    [SerializeField] internal string skillID, skillName;
    [SerializeField] internal TypeOfSkill type;
    [SerializeField] internal string sDesc;
    [SerializeField] internal int power;

    [Header("Png")]
    [SerializeField] internal Sprite[] listBtnClr;
}