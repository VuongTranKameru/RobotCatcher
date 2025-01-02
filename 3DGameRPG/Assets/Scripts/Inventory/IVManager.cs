using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IVManager : MonoBehaviour
{
    [Header("Insert field")]
    [SerializeField] Transform posItem;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] PlayerStat playerStat;
    ItemSlot slot;
    Button itemBtn;
    List<Button> groupI = new();
    bool isDup;
    bool isAlreadyLoad = false;

    [Header("Invoke")]
    [SerializeField] InventoryDecription inventoryDescription;

    private void OnEnable()
    {
        playerStat = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<PlayerStat>();

        if (!isAlreadyLoad)
        {
            for (int i = 0; i < playerStat.AmountOfItems(); i++)
            {
                isDup = false;
                if (groupI.Count > 0)
                {
                    for (int j = 0; j < groupI.Count; j++)
                        if (playerStat.ClickOnItem(i).itemName == groupI[j].GetComponent<ItemSlot>().nameItem)
                        {
                            PlusItem(groupI[j].GetComponent<ItemSlot>());
                            isDup = true;
                            break;
                        }
                }

                if (!isDup)
                    CallCharInfoIntoSlot(playerStat.ClickOnItem(i));
            }
            isAlreadyLoad = true;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < groupI.Count; i++) //with player
        {
            Destroy(groupI[i].gameObject);
        }
        groupI.Clear();
        isAlreadyLoad = false;
        //itemBtn?.onClick.RemoveAllListeners();
    }

    void CallCharInfoIntoSlot(ItemConfig item)
    {
        GameObject newPanel = Instantiate(itemPrefab, posItem);
        slot = newPanel.GetComponent<ItemSlot>();
        itemBtn = newPanel.GetComponent<Button>();
        groupI.Add(itemBtn);

        slot.nameItem = item.itemName;
        slot.ItemImage.sprite = item.icon;
        slot.AmountCount = 1;
        slot.NumCountAmount();

        //when click, info of robot will change
        itemBtn.onClick.AddListener(() =>
        {
            inventoryDescription.OnClickReadItem(item, playerStat);
        });
    }

    void PlusItem(ItemSlot itemCount)
    {
        itemCount.AmountCount += 1;
        itemCount.NumCountAmount();
    }
}
