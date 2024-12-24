using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaneltemBattle : MonoBehaviour
{
    [SerializeField] ItemConfig item;
    [SerializeField] TMP_Text nameItem, itemDes, amountNum;
    [SerializeField] Image itemImage;
    int amountCount;

    public ItemConfig Item
    {
        get { return item; }
        set { item = value; }
    }

    public TMP_Text NameItem
    {
        get { return nameItem; }
        set { nameItem = value; }
    }

    public TMP_Text ItemDes
    {
        get { return itemDes; }
        set { itemDes = value; }
    }

    public Image ItemImage
    {
        get { return itemImage; }
        set { itemImage = value; }
    }

    public int AmountCount
    {
        get { return amountCount; }
        set { amountCount = value; }
    }

    public void NumCountAmount()
    {
        amountNum.text = "x" + amountCount.ToString();
    }
}
