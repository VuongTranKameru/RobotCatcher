using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwnerMenu : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    PlayerStat owner;

    [Header("Calling Robotcatcher")]
    [SerializeField] Transform contentOwnLocation;
    [SerializeField] ToggleGroup grpOwned;
    [SerializeField] GameObject panelHolder;
    PanelOfOwnedRobot robotPanel;
    List<Toggle> emptyPanel = new List<Toggle>();

    void Awake()
    {
        playerPrefab = FindObjectOfType<PlayerStat>().gameObject;
        owner = playerPrefab.GetComponent<PlayerStat>();
    }

    private void OnEnable()
    {
        InsertRobot();
    }

    private void OnDisable()
    {
        for (int i = 0; i <= owner.AmountOfRobots(); i++) //with player
        {
            Destroy(emptyPanel[i].gameObject);
        }
        emptyPanel.Clear();
    }

    void InsertRobot()
    {
        CallCharInfoIntoPanel(owner.PlayerStats());
        for (int i = 0; i < owner.AmountOfRobots(); i++)
        {
            CallCharInfoIntoPanel(owner.ChooseRobot(i));
        }
    }

    void CallCharInfoIntoPanel(StatConfig robot)
    {
        GameObject newPanel = Instantiate(panelHolder, contentOwnLocation);
        newPanel.GetComponent<Toggle>().group = grpOwned;
        robotPanel = newPanel.GetComponent<PanelOfOwnedRobot>();
        emptyPanel.Add(newPanel.GetComponent<Toggle>());

        robotPanel.NameTag.text = robot.nameChar;
        robotPanel.LevelHolder.text = robot.lv.ToString();
        robotPanel.HPNum.text = robot.health.ToString() + "/" + robot.maxHP.ToString();
        robotPanel.SPNum.text = robot.specialPoint.ToString() + "/" + robot.maxSP.ToString();
        robotPanel.CharAva.sprite = robot.Avatar();
    }
}
