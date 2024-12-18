using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfItem
{
    InBattle,
    Upgrade,
    Scrap,
    KeyItem
}

[CreateAssetMenu(fileName = "ItemScriptData", menuName = "ScriptableObjects/ItemData", order = 3)]
public class ItemConfig : ScriptableObject
{
    [SerializeField] internal string itemID, itemName;
    [SerializeField] internal TypeOfItem type;
    [SerializeField] internal string itDesc;
    [SerializeField] internal int value, amount;

    [Header("Png")]
    [SerializeField] internal Sprite icon;
}
