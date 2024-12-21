using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusOnScreen : MonoBehaviour
{
    [Header("Player Info Screen")]
    [SerializeField] GameObject player;
    PlayerStat stat;
    [SerializeField] TMP_Text hpRemainScr;
    [SerializeField] Image hpBar;
    int curHP; //max always 100

    [Header("Robot Info Screen")]
    [SerializeField] Transform robotOwnedLocation;
    [SerializeField] GameObject robotIcon;
    [SerializeField] Sprite unavailable;

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("PlayerModel").gameObject;
            stat = player.GetComponent<PlayerStat>();

            ShowHP();
            ShowRobot();
        }
    }

    void ShowHP()
    {
        curHP = stat.HPRemain;

        float ratio = (float)curHP / 100; //tim % mau sau khi mat hp
        hpBar.rectTransform.localPosition = new Vector3(hpBar.rectTransform.rect.width * ratio - hpBar.rectTransform.rect.width,
            0, 0); //day thanh image qua trai, bang (tong thanh image * 0.so mau mat - tong thanh image hien tai)
        hpRemainScr.text = curHP.ToString() + "/100";
    }

    void ShowRobot()
    {
        for (int i = 0; i < stat.AmountOfRobots(); i++)
        {
            GameObject newIcon = Instantiate(robotIcon, robotOwnedLocation);
            if (stat.ChooseRobot(i).health <= 0)
                newIcon.GetComponent<Image>().sprite = unavailable;
        }
    }
}
