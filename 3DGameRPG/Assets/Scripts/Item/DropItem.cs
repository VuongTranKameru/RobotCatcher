using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [Header("ItemDropped")]
    [SerializeField] internal List<ItemConfig> itemList = new();

    public void AlreadyCollected()
    {
        itemList.Clear();
    }
}
