using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    BattleState state;

    [Header("Location Spawn")]
    [SerializeField] GameObject playerPrefab, enemyPrefab;
    [SerializeField] Transform playerStand, enemyStand;
    RobotStat pStats, eStats;

    [Header("Player Info Screen")]
    [SerializeField] TMP_Text playerNameScr;
    [SerializeField] TMP_Text hpRemainScr, levelScr;

    [Header("Skill")]
    [SerializeField] ToggleGroup groupSkill;
    [SerializeField] Toggle attackSkill, defenseSkill;

    void Awake()
    {
        state = BattleState.BeginBattle;
        WrapCharactersIn();
    }

    void Start()
    {
        Debug.Log($"{pStats.NameStat()} turns.");
        state = BattleState.PlayerTurn;
    }

    void Update()
    {
        if (state == BattleState.EnemyTurn)
        {
            attackSkill.GetComponent<ICanUseSkill>().SkillUsed(eStats, pStats);
            Debug.Log($"{eStats.NameStat()} attack {pStats.NameStat()} with {eStats.AttackStat()} dmg!");
            Debug.Log($"{pStats.NameStat()} drop down from {pStats.MaxHPStat()} to {pStats.HPRemain}.");

            UpdatingStatOnScreen();

            if (pStats.HPRemain <= 0)
                state = BattleState.LoseBattle;
            else
                state = BattleState.PlayerTurn;
        }
    }

    void WrapCharactersIn()
    {
        GameObject playerInfo = Instantiate(playerPrefab, playerStand);
        pStats = playerInfo.GetComponent<RobotStat>();

        GameObject enemyInfo = Instantiate(enemyPrefab, enemyStand);
        eStats = enemyInfo.GetComponent<RobotStat>();

        pStats.HPRemain = pStats.MaxHPStat();
        eStats.HPRemain = eStats.MaxHPStat();

        playerNameScr.text = pStats.NameStat();
        levelScr.text = pStats.LvStat().ToString();
        UpdatingStatOnScreen();

        Debug.Log($"{pStats.NameStat()} meeting {eStats.NameStat()}!");
    }

    void UpdatingStatOnScreen()
    {
        hpRemainScr.text = pStats.HPRemain.ToString();
    }

    public void OnAttackButton()
    {
        if (groupSkill.AnyTogglesOn())
        {
            if (attackSkill.isOn)
                attackSkill.GetComponent<ICanUseSkill>().SkillUsed(pStats, eStats);
            else if (defenseSkill.isOn)
                defenseSkill.GetComponent<ICanUseSkill>().SkillUsed(pStats, eStats);

            Debug.Log($"{pStats.NameStat()} attack {eStats.NameStat()} with {pStats.AttackStat()} dmg!");
            Debug.Log($"{eStats.NameStat()} drop down from {eStats.MaxHPStat()} to {eStats.HPRemain}.");

            if (eStats.HPRemain <= 0)
                state = BattleState.WonBattle;
            else
                state = BattleState.EnemyTurn;
        }
    }

    public void OnLevelUp()
    {
        pStats.LevelUp();
        Debug.Log($"{pStats.MaxHPStat()} up");
    }
}
