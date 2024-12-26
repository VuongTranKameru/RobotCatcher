using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemBattleMenu : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    PlayerStat owner;

    [Header("Put Item In")]
    [SerializeField] Transform contentItemLocation;
    [SerializeField] ToggleGroup grpItem;
    [SerializeField] GameObject itemHolder;
    PaneltemBattle itemPanel;
    List<Toggle> itemToggle = new();
    ItemConfig chosenI;
    bool isDupplicant;

    [Header("Select Robots")]
    [SerializeField] Transform contentRobotLocation;
    [SerializeField] GameObject buttonHolder;
    PanelOfOwnedRobot robotPanel;
    List<Button> robotButton = new();
    [SerializeField] Sprite unplayable;

    [Header("Active The Function")]
    [SerializeField] GameObject itemMenu;
    [SerializeField] GameObject itemBoard, robotSelectBoard, firstChooseBoard;
    [SerializeField] UnityEvent notChooseYet, notCorrectItem, finishItemFunction;
    bool isChoose;

    void Awake()
    {
        playerPrefab = FindObjectOfType<PlayerStat>().gameObject;
        owner = playerPrefab.GetComponent<PlayerStat>();
    }

    private void OnEnable()
    {
        isChoose = false;
        InsertItem();
    }

    private void OnDisable()
    {
        for (int i = 0; i < itemToggle.Count; i++)
            Destroy(itemToggle[i].gameObject);

        itemToggle.Clear();
    }

    void InsertItem()
    {
        for (int i = 0; i < owner.AmountOfItems(); i++)
        {
            if (owner.ClickOnItem(i).type != TypeOfItem.InBattle)
                continue;

            isDupplicant = false;
            if (itemToggle.Count > 0)
            {
                for (int j = 0; j < itemToggle.Count; j++)
                    if (owner.ClickOnItem(i).itemName == itemToggle[j].GetComponent<PaneltemBattle>().NameItem.text)
                    {
                        PlusItem(itemToggle[j].GetComponent<PaneltemBattle>());
                        isDupplicant = true;
                        break;
                    }
            }
            
            if (!isDupplicant)
                PutItemInfoIntoPanel(owner.ClickOnItem(i));
        }
    }

    void PutItemInfoIntoPanel(ItemConfig item)
    {
        GameObject newPanel = Instantiate(itemHolder, contentItemLocation);
        newPanel.GetComponent<Toggle>().group = grpItem;
        itemPanel = newPanel.GetComponent<PaneltemBattle>();
        itemToggle.Add(newPanel.GetComponent<Toggle>());

        itemPanel.Item = item;
        itemPanel.NameItem.text = item.itemName;
        itemPanel.ItemDes.text = item.itDesc;
        itemPanel.ItemImage.sprite = item.icon;

        itemPanel.AmountCount = 1;
        itemPanel.NumCountAmount();
    }

    void PlusItem(PaneltemBattle itemCount)
    {
        itemCount.AmountCount += 1;
        itemCount.NumCountAmount();
    }

    public void OnConfirmUsingItem()
    {
        for (int i = 0; i < itemToggle.Count; i++)
        {
            if (itemToggle[i].isOn)
            {
                //disable first menu to open second menu
                itemBoard.SetActive(false);
                firstChooseBoard.SetActive(false);
                robotSelectBoard.SetActive(true);
                isChoose = true;

                //call robot out to choose
                InsertRobotToUsedItem();
                chosenI = itemToggle[i].GetComponent<PaneltemBattle>().Item;
                //switchPlayer?.Invoke(i);

                break;
            }
        }

        if (!isChoose)
            notChooseYet?.Invoke();
    }

    void UseItemOn()
    {
        owner.DeleteItem(chosenI);

        //restore item menu to begin state
        DeleteRobotToUsedItem();
        robotSelectBoard.SetActive(false);
        itemBoard.SetActive(true);
        itemMenu.SetActive(false);

        finishItemFunction?.Invoke();
    }

    #region Summon Robotcatcher to Select
    void InsertRobotToUsedItem()
    {
        CallCharInfoIntoPanel(owner.PlayerStats());

        for (int i = 0; i < owner.AmountOfRobots(); i++)
            CallCharInfoIntoPanel(owner.ChooseRobot(i));
    }

    public void DeleteRobotToUsedItem()
    {
        for (int i = 0; i <= owner.AmountOfRobots(); i++) //with player
            Destroy(robotButton[i].gameObject);

        robotButton.Clear();
    }

    void CallCharInfoIntoPanel(StatConfig robot)
    {
        GameObject newBtn = Instantiate(buttonHolder, contentRobotLocation);
        newBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (chosenI.Healing(robot, chosenI))
                UseItemOn();
            else notCorrectItem?.Invoke();
        });

        robotPanel = newBtn.GetComponent<PanelOfOwnedRobot>();
        robotButton.Add(newBtn.GetComponent<Button>());

        robotPanel.NameTag.text = robot.nameChar;
        robotPanel.LevelHolder.text = robot.lv.ToString();
        robotPanel.HPNum.text = robot.health.ToString() + "/" + robot.maxHP.ToString();
        robotPanel.SPNum.text = robot.specialPoint.ToString() + "/" + robot.maxSP.ToString();
        robotPanel.CharAva.sprite = robot.Avatar();

        if (robot.health <= 0)
            robotPanel.DeadStatus(unplayable);
    }
    #endregion
}
