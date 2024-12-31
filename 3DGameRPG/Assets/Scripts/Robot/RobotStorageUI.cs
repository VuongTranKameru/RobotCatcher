using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class RobotStorageUI : MonoBehaviour
{
    [SerializeField] GameObject content;
    [SerializeField] Transform posRobot;
    [SerializeField] GameObject robotPrefab;

    [SerializeField] PlayerStat playerStat;
    
    private void OnEnable()
    {
        playerStat = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<PlayerStat>();

        for (int i = 0; i < playerStat.AmountOfRobots(); i++)
        {
            Instantiate(robotPrefab, posRobot);
        }
    }

}
