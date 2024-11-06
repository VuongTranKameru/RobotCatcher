using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDynamic : MonoBehaviour
{
    [SerializeField] BattleManager btlState;
    CinemachineVirtualCamera cinam;
    BattleState state;

    void Awake()
    {
        cinam = GetComponent<CinemachineVirtualCamera>();
        state = BattleState.PlayerTurn;
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
