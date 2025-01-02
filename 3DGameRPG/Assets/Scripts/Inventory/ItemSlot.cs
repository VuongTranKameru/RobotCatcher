using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public GameObject currentItem;
    internal string nameItem; //save data only

    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text amountTxt;
    int amountCount;

    public Image ItemImage
    {
        get { return itemImage; }
        set
        {
            itemImage = value;
            itemImage.transform.position = new Vector3(0, 0, 0);
        }
    }

    public int AmountCount
    {
        get { return amountCount; }
        set { amountCount = value; }
    }

    public void NumCountAmount()
    {
        amountTxt.text = "x" + amountCount.ToString();
    }
}
