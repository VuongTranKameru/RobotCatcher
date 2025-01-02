using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RobotInfoMenu : MonoBehaviour
{
    [SerializeField] TMP_Text charName;
    [SerializeField] TMP_Text descript;
    [SerializeField] TMP_Text currentHp;
    [SerializeField] TMP_Text attack;
    [SerializeField] TMP_Text def;
    [SerializeField] TMP_Text speed;
    [SerializeField] TMP_Text spSkill;
    [SerializeField] Image expBar;

    public void OnClickReadRobot(StatConfig robot)
    {
        charName.text = robot.nameChar;
        descript.text = robot.description;
        currentHp.text = robot.health.ToString();
        attack.text = robot.attack.ToString();
        def.text = robot.defense.ToString();
        speed.text = robot.speed.ToString();
        spSkill.text = robot.maxSP.ToString();

        RaiseEXPBar(robot);
    }

    public void RaiseEXPBar(StatConfig robot)
    {
        float spRatio = (float)robot.expProgress / (robot.expStandard * robot.lv); //tim % nang luong sau khi tu dong hoi
        expBar.rectTransform.localPosition = new Vector3(0, expBar.rectTransform.rect.height * spRatio - expBar.rectTransform.rect.height,
            0); //day thanh image len tren, bang (tong thanh image * 0.so sp tang - tong thanh image hien tai)
    }
}
