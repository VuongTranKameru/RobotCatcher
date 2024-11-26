using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] protected GameObject inventoryMenu;
    protected bool menuActivated;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && menuActivated)
        {
            Time.timeScale = 1;
            inventoryMenu.SetActive(false);
            menuActivated = false;
        }
        
        else if(Input.GetKeyDown(KeyCode.E) && !menuActivated)
        {
            Time.timeScale = 0;
            inventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        Debug.Log(" itemName = " + itemName + " quantity = " + quantity + " itemSprite = " + itemSprite);
    }
}
