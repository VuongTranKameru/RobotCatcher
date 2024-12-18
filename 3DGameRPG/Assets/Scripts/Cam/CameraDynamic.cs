using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDynamic : MonoBehaviour
{
    [SerializeField] BattleManager btlState;
    CinemachineVirtualCamera cinam;

    void Awake()
    {
        cinam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        /*StartCoroutine(PlayerCam());
        StartCoroutine(WaitBattleTurn());
        Invoke(nameof(EnemyCam), 3f);*/
        if (btlState.CurrentState() == BattleState.PlayerTurn)
        {
            cinam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 0;
        }
        else if (btlState.CurrentState() == BattleState.EnemyTurn)
        {
            cinam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 2;
        }
    }
}
