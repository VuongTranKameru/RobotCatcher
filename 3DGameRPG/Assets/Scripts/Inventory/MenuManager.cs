using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] protected GameObject inventoryPanel;
    protected bool menuActivated;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U) && menuActivated)
        {
            Time.timeScale = 1;
            inventoryPanel.SetActive(false);
            menuActivated = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        else if(Input.GetKeyDown(KeyCode.U) && !menuActivated)
        {
            Time.timeScale = 0;
            inventoryPanel.SetActive(true);
            menuActivated = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void AddItem(string itemName, int value, Sprite itemSprite)
    {
        Debug.Log("itemName: " + itemName + "value: " + value + "itemSprite: " + itemSprite);
    }
}
