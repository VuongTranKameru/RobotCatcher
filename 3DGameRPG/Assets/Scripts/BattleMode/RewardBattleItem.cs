using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RewardBattleItem : MonoBehaviour
{
    [SerializeField] DropItem drop;
    GameObject robotEnemy, playerPrefab;

    [Header("Show Item Info")]
    [SerializeField] Transform contentLootLocation;
    [SerializeField] ToggleGroup grpLoot;
    [SerializeField] GameObject itemHolder;
    List<Toggle> itemRwToggle = new();
    PaneltemBattle itemPanel;
    bool isDupplicant;

    [Header("Change Scene")]
    [SerializeField] SceneBattleEnd scene;
    [SerializeField] UnityEvent<ItemConfig> PutItemToPlayer;

    private void Start()
    {
        playerPrefab = GameObject.FindGameObjectWithTag("PlayerModel");

        robotEnemy = GameObject.FindGameObjectWithTag("Enemy");
        drop = robotEnemy.GetComponent<DropItem>();

        for (int i = 0; i < drop.itemList.Count; i++)
        {
            isDupplicant = false;
            if (itemRwToggle.Count > 0)
            {
                for (int j = 0; j < itemRwToggle.Count; j++)
                    if (drop.itemList[i].itemName == itemRwToggle[j].GetComponent<PaneltemBattle>().NameItem.text)
                    {
                        PlusItem(itemRwToggle[j].GetComponent<PaneltemBattle>());
                        isDupplicant = true;
                        break;
                    }
            }

            if (!isDupplicant)
                PutItemRewardUI(drop.itemList[i]);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < itemRwToggle.Count; i++)
            Destroy(itemRwToggle[i].gameObject);

        itemRwToggle.Clear();
    }

    public void RewardForTutorial()
    {
        Reward();
        scene.WinningTutorialScene();
    }

    public void RewardForBattle()
    {
        Reward();
        scene.ReturnFromBattle();
    }

    void Reward()
    {
        for (int i = 0; i < drop.itemList.Count; i++)
        {
            PutItemToPlayer.Invoke(drop.itemList[i]);
        }

        Destroy(robotEnemy);
        Destroy(playerPrefab);
    }
    #region UI
    void PutItemRewardUI(ItemConfig item)
    {
        GameObject newPanel = Instantiate(itemHolder, contentLootLocation);
        newPanel.GetComponent<Toggle>().group = grpLoot;
        itemPanel = newPanel.GetComponent<PaneltemBattle>();
        itemRwToggle.Add(newPanel.GetComponent<Toggle>());

        itemPanel.NameItem.text = item.itemName;
        itemPanel.ItemImage.sprite = item.icon;

        itemPanel.AmountCount = 1;
        itemPanel.NumCountAmount();
    }

    void PlusItem(PaneltemBattle itemCount)
    {
        itemCount.AmountCount += 1;
        itemCount.NumCountAmount();
    }
    #endregion
}
