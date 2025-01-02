using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RobotBoxPartySlot : MonoBehaviour
{
    [SerializeField] TMP_Text robotName;
    [SerializeField] TMP_Text lvRobot;
    [SerializeField] Image robotImage;

    public TMP_Text RobotName
    {
        get { return robotName; }
        set { robotName = value; }
    }

    public TMP_Text LvRobot
    {
        get { return lvRobot; }
        set { lvRobot = value; }
    }

    public Image RobotImage
    {
        get { return robotImage; }
        set
        {
            robotImage = value;
            robotImage.transform.position = new Vector3(0, 0, 0);
        }
    }

}
