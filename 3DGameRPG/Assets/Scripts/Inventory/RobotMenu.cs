using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RobotMenu : MonoBehaviour
{
    [SerializeField] TMP_Text robotName;
    [SerializeField] TMP_Text lvRobot;
    [SerializeField] Image robotImage;

    [SerializeField] PlayerStat playerStat;

    private void OnEnable()
    {
        playerStat = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<PlayerStat>();
    }

    private void Update()
    {
        robotName.text = playerStat.NameStat();
        lvRobot.text = playerStat.LvStat().ToString();
    }
}
