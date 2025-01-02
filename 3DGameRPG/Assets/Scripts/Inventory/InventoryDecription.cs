using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDecription : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] Button itemUseBtn;
    [SerializeField] GameObject robotSelectBoard, menuInventory;

    [Header("Select Robots")]
    [SerializeField] Transform contentRobotLocation;
    [SerializeField] GameObject buttonHolder;
    PanelOfOwnedRobot robotPanel;
    List<Button> robotButton = new();
    [SerializeField] Sprite unplayable;

    private void OnEnable()
    {
        ResetDecription();
    }

    public void ResetDecription()
    {
        itemImage.gameObject.SetActive(false);
        title.text = " ";
        description.text = " ";

        /*itemUseBtn.gameObject.SetActive(false);
        robotSelectBoard.SetActive(false);*/
    }

    public void SetDecription(Sprite sprite, string itemName, string itemDecription)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.title.text = itemName;
        this.description.text = itemDecription;
    }

    public void OnClickReadItem(ItemConfig item, PlayerStat player)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = item.icon;
        title.text = item.itemName;
        description.text = item.itDesc;

        /*itemUseBtn.gameObject.SetActive(true);
        itemUseBtn.onClick.AddListener(() =>
        {
            OnClickOnUseBtn(item, player);
        });*/
    }

    //DO NOT USE THIS
    void OnClickOnUseBtn(ItemConfig item, PlayerStat player)
    {
        //disable first menu to open second menu
        robotSelectBoard.SetActive(true);

        //call robot out to choose
        CallCharInfoIntoPanel(player.PlayerStats(), item, player);

        for (int i = 0; i < player.AmountOfRobots(); i++)
            CallCharInfoIntoPanel(player.ChooseRobot(i), item, player);
    }

    void CallCharInfoIntoPanel(StatConfig robot, ItemConfig chosenI, PlayerStat player)
    {
        GameObject newBtn = Instantiate(buttonHolder, contentRobotLocation);
        newBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (chosenI.Healing(robot, chosenI)) //the item function will run while checking bool
            {
                player.DeleteItem(chosenI);
                OnExitRobotUsedItem();
                robotSelectBoard.SetActive(false);
                menuInventory.SetActive(false);
            }
            //else notCorrectItem?.Invoke();
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

    public void OnExitRobotUsedItem()
    {
        for (int i = 0; i < robotButton.Count; i++) //with player
            Destroy(robotButton[i].gameObject);

        robotButton.Clear();

        itemUseBtn.onClick.RemoveAllListeners(); //reset the button
    }
}
