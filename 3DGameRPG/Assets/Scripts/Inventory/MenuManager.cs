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
        }
        
        else if(Input.GetKeyDown(KeyCode.U) && !menuActivated)
        {
            Time.timeScale = 0;
            inventoryPanel.SetActive(true);
            menuActivated = true;
        }
    }
}
