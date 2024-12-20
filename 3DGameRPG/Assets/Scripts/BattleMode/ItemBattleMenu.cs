using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    bool isDupplicant;

    void Awake()
    {
        playerPrefab = FindObjectOfType<PlayerStat>().gameObject;
        owner = playerPrefab.GetComponent<PlayerStat>();
    }

    private void OnEnable()
    {
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

    /*public void OnConfirmUsingItem()
    {
        for (int i = 0; i < itemToggle.Count; i++)
        {
            if (itemToggle[i].isOn)
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
    }*/
}
