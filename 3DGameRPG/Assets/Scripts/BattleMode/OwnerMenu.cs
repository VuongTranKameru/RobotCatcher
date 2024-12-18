using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OwnerMenu : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    PlayerStat owner;

    [Header("Call Robotcatcher")]
    [SerializeField] Transform contentOwnLocation;
    [SerializeField] ToggleGroup grpOwned;
    [SerializeField] GameObject panelHolder;
    PanelOfOwnedRobot robotPanel;
    List<Toggle> emptyPanel = new List<Toggle>();
    [SerializeField] Sprite unplayable, isPlaying;

    [Header("Call From BattleManager")]
    [SerializeField] GameObject ownerBoard;
    [SerializeField] GameObject playerBoard;
    [SerializeField] UnityEvent<int> switchPlayer; //dynamic int
    [SerializeField] UnityEvent notChooseYet;
    bool isChoose;

    void Awake()
    {
        playerPrefab = FindObjectOfType<PlayerStat>().gameObject;
        owner = playerPrefab.GetComponent<PlayerStat>();
    }

    private void OnEnable()
    {
        isChoose = false;
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
        if(owner.RemainOnBattle().nameChar == owner.NameStat())
            CheckCharAlreadyUsed();

        for (int i = 0; i < owner.AmountOfRobots(); i++)
        {
            CallCharInfoIntoPanel(owner.ChooseRobot(i));

            if (owner.RemainOnBattle() == owner.ChooseRobot(i) && owner.ChooseRobot(i).health > 0)
                CheckCharAlreadyUsed();
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

        if (robot.health <= 0)
            robotPanel.PlayableOrDead(unplayable);
    }

    void CheckCharAlreadyUsed()
    {
        robotPanel.PlayableOrDead(isPlaying);
    }

    public void OnConfirmChosingNewRobot()
    {
        for (int i = 0; i < emptyPanel.Count; i++)
        {
            if (emptyPanel[i].isOn)
            {
                switchPlayer?.Invoke(i); //pull variable onto battlemanager
                ownerBoard.SetActive(false);
                playerBoard.SetActive(true);
                isChoose = true;
                break;
            }
        }

        if (!isChoose)
            notChooseYet?.Invoke();
    }
}
