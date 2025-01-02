using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatMenu : MonoBehaviour
{
    [Header("Player Part")]
    [SerializeField] TMP_Text charName;
    [SerializeField] TMP_Text currentHp;
    [SerializeField] TMP_Text attack;
    [SerializeField] TMP_Text def;
    [SerializeField] TMP_Text speed;

    [SerializeField] PlayerStat playerStat;

    [Header("1st Robot Part")] 
    [SerializeField] TMP_Text robotName;
    [SerializeField] TMP_Text botDes, botHp, botAtk, botDef, botSpeed, botSpSkill;
    [SerializeField] Image botAva;

    private void OnEnable()
    {
        playerStat = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<PlayerStat>();

        PlayerInfo();
        if (playerStat.AmountOfRobots() > 0)
            RobotInfo(playerStat.ChooseRobot(0));
    }

    void PlayerInfo()
    {
        charName.text = playerStat.NameStat();
        currentHp.text = playerStat.HPRemain.ToString();
        attack.text = playerStat.AttackStat().ToString();
        def.text = playerStat.DefenseStat().ToString();
        speed.text = playerStat.SpeedStat().ToString();
    }

    void RobotInfo(StatConfig robot)
    {
        robotName.text = robot.nameChar;
        botDes.text = robot.description;
        botHp.text = robot.health.ToString();
        botAtk.text = robot.attack.ToString();
        botDef.text = robot.defense.ToString();
        botSpeed.text = robot.speed.ToString();
        botSpSkill.text = robot.maxSP.ToString();
        botAva.sprite = robot.Avatar();
    }
}
