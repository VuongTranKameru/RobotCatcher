using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class RobotStorageUI : MonoBehaviour
{
    [Header("Insert field")]
    [SerializeField] Transform posRobot;
    [SerializeField] GameObject boxParty;
    [SerializeField] PlayerStat playerStat;
    RobotBoxPartySlot robotPanel;
    Button robotBtn;
    List<Button> groupR = new();
    bool isAlreadyLoad = false;

    [Header("Invoke RobotStatMenu")]
    [SerializeField] RobotInfoMenu menuRight;

    private void Awake()
    {
        playerStat = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<PlayerStat>();
    }

    private void OnEnable()
    {
        if (!isAlreadyLoad)
        {
            for (int i = 0; i < playerStat.AmountOfRobots(); i++)
            {
                CallCharInfoIntoBoxParty(playerStat.ChooseRobot(i));
            }
            isAlreadyLoad = true;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < groupR.Count; i++) //with player
        {
            Destroy(groupR[i].gameObject);
        }
        groupR.Clear();
        isAlreadyLoad = false;
    }

    void CallCharInfoIntoBoxParty(StatConfig robot)
    {
        GameObject newPanel = Instantiate(boxParty, posRobot);
        robotPanel = newPanel.GetComponent<RobotBoxPartySlot>();
        robotBtn = newPanel.GetComponent<Button>();
        groupR.Add(robotBtn);

        robotPanel.RobotName.text = robot.nameChar;
        robotPanel.LvRobot.text = robot.lv.ToString();
        robotPanel.RobotImage.sprite = robot.Avatar();

        //when click, info of robot will change
        robotBtn.onClick.AddListener(() =>
        {
            menuRight.OnClickReadRobot(robot);
        });
    }
}
