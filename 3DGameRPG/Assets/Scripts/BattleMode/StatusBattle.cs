using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBattle : MonoBehaviour
{
    [SerializeField] BattleManager btl;

    [Header("Player Side")]
    [SerializeField] GameObject robotPPrefab;
    [SerializeField] GameObject playerPrefab;
    PlayerStat playerStat; RobotStat robotStat;
    [SerializeField] GameObject userAtk, userDef, userStatus;
    [SerializeField] Image urAtkChange, urDefChange, urStatusChange;

    [Header("Enemy Side")]
    [SerializeField] GameObject enemyPrefab;
    RobotStat enemyStat;
    [SerializeField] GameObject oppAtk, oppDef, oppStatus;
    [SerializeField] Image oppAtkChange, oppDefChange, oppStatusChange;

    [Header("PNG")]
    [SerializeField] Sprite nonChange;
    [SerializeField] Sprite atkUp, atkDown, defUp, defDown, overheatStatus, shockStatus, overheatColor, shockColor;

    void OnEnable()
    {
        enemyPrefab = GameObject.FindGameObjectWithTag("Enemy");
        enemyStat = enemyPrefab.GetComponent<RobotStat>();

        robotPPrefab = GameObject.FindGameObjectWithTag("Ally");
        robotStat = robotPPrefab?.GetComponent<RobotStat>();

        playerPrefab = GameObject.FindGameObjectWithTag("PlayerModel");
        playerStat = playerPrefab.GetComponent<PlayerStat>();

        //updating
        if (btl.RobotOnStage())
        {
            UserAtkStatus(robotStat);
            UserDefStatus(robotStat);
            UserStatusEffect(robotStat);
        }
        else
        {
            PlayerStatus();
            UserStatusEffect(playerStat);
        }
        EnemyStatus();
    }

    void PlayerStatus()
    {
        userDef.SetActive(false);
        userAtk.SetActive(false);
    }

    void EnemyStatus()
    {
        OppAtkStatus(enemyStat);
        OppDefStatus(enemyStat);
        OppStatusEffect(enemyStat);
    }

    void UserAtkStatus(RobotStat chosen)
    {
        if (chosen.ATKTemp == chosen.AttackStat())
            userAtk.SetActive(false);
        else if (chosen.ATKTemp > chosen.AttackStat())
        {
            userAtk.SetActive(true);
            urAtkChange.sprite = atkUp;
        }
        else
        {
            userAtk.SetActive(true);
            urAtkChange.sprite = atkDown;
        }
    }

    void UserDefStatus(RobotStat chosen)
    {
        if (chosen.DEFTemp == chosen.DefenseStat())
            userDef.SetActive(false);
        else if (chosen.DEFTemp > chosen.DefenseStat())
        {
            userDef.SetActive(true);
            urDefChange.sprite = defUp;
        }
        else
        {
            userDef.SetActive(true);
            urDefChange.sprite = defDown;
        }
    }

    void OppAtkStatus(RobotStat enemy)
    {
        if (enemy.ATKTemp == enemy.AttackStat())
            oppAtk.SetActive(false);
        else if (enemy.ATKTemp > enemy.AttackStat())
        {
            oppAtk.SetActive(true);
            oppAtkChange.sprite = atkUp;
        }
        else
        {
            oppAtk.SetActive(true);
            oppAtkChange.sprite = atkDown;
        }
    }

    void OppDefStatus(RobotStat enemy)
    {
        if (enemy.DEFTemp == enemy.DefenseStat())
            oppDef.SetActive(false);
        else if (enemy.DEFTemp > enemy.DefenseStat())
        {
            oppDef.SetActive(true);
            oppDefChange.sprite = defUp;
        }
        else
        {
            oppDef.SetActive(true);
            oppDefChange.sprite = defDown;
        }
    }

    void UserStatusEffect(IHaveSameStat user)
    {
        if (user.StatusEffectState() == global::StatusEffect.Overheat)
        {
            userStatus.SetActive(true);
            userStatus.GetComponent<Image>().sprite = overheatColor;
            urStatusChange.sprite = overheatStatus;
        }
        else if (user.StatusEffectState() == global::StatusEffect.Shock)
        {
            userStatus.SetActive(true);
            userStatus.GetComponent<Image>().sprite = shockColor;
            urStatusChange.sprite = shockStatus;
        }
        else userStatus.SetActive(false);
    }

    void OppStatusEffect(RobotStat opp)
    {
        if (opp.StatusEffectState() == StatusEffect.Overheat)
        {
            oppStatus.SetActive(true);
            oppStatus.GetComponent<Image>().sprite = overheatColor;
            oppStatusChange.sprite = overheatStatus;
        }
        else if (opp.StatusEffectState() == StatusEffect.Shock)
        {
            oppStatus.SetActive(true);
            oppStatus.GetComponent<Image>().sprite = shockColor;
            oppStatusChange.sprite = shockStatus;
        }
        else oppStatus.SetActive(false);
    }
}
