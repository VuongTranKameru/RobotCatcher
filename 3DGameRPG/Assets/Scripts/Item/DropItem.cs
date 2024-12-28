using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropItem : MonoBehaviour
{
    [Header("ItemDropped")]
    [SerializeField] internal List<ItemConfig> itemList = new();
    [SerializeField] UnityEvent<ItemConfig> PutItemToPlayer;

    public void TakeItem()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            PutItemToPlayer.Invoke(itemList[i]);
        }
    }

    public void AlreadyCollected()
    {
        itemList.Clear();
    }

    public bool EmptyLoot()
    {
        if (itemList.Count == 0)
            return true;
        return false;
    }
}
