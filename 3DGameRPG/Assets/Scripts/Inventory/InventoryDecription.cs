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

    private void Awake()
    {
        ResetDecription();
    }

    public void ResetDecription()
    {
        this.itemImage.gameObject.SetActive(false);
        this.title.text = "";
        this.description.text = "";
    }

    public void SetDecription(Sprite sprite, string itemName, string itemDecription)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.title.text = itemName;
        this.description.text = itemDecription;
    }
}
