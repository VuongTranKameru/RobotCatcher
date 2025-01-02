using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public Image[] tabImages;
    public GameObject[] tabContainers;

    private void Start()
    {
        ActiveTab(0);
    }

    public void ActiveTab(int tabNo)
    {
        for(int i = 0; i < tabContainers.Length; i++)
        {
            tabContainers[i].SetActive(false);
            tabImages[i].color = Color.grey;
        }
        tabContainers[tabNo].SetActive(true);
        tabImages[tabNo].color = Color.white;
    }
}
