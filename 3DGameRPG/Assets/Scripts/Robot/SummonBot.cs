using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBot : MonoBehaviour
{
    [SerializeField] PlayerStat player;
    [SerializeField] Transform rPobotStand;
    [SerializeField] GameObject robotPPrefab;
    RobotStat rPobots;

    void Awake()
    {
        robotPPrefab = Instantiate(player.TestRobot().Itself(), rPobotStand.position, rPobotStand.rotation);
        rPobots = robotPPrefab.GetComponent<RobotStat>();
        rPobots.RobotStats = player.TestRobot();
        rPobots.CallOutTempStat();
        /*rPobots = robotPPrefab.GetComponent<RobotStat>();
        rPobots.RobotStats = player.TestRobot();*/
    }

    void Update()
    {
        
    }
}
