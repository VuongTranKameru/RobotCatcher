using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    BattleState state;

    [SerializeField] GameObject playerPrefab, enemyPrefab;
    [SerializeField] Transform playerStand, enemyStand;
    CharStats pStats, eStats;

    void Awake()
    {
        state = BattleState.BeginBattle;
        WrapCharactersIn();
    }

    void Start()
    {
        PlayerTurn();
    }

    void Update()
    {
        
    }

    void WrapCharactersIn()
    {
        GameObject playerInfo = Instantiate(playerPrefab, playerStand);
        pStats = playerInfo.GetComponent<CharStats>();

        GameObject enemyInfo = Instantiate(enemyPrefab, enemyStand);
        eStats = enemyInfo.GetComponent<CharStats>();

        Debug.Log($"{pStats.nameChar} meeting {eStats.nameChar}!");
    }

    void PlayerTurn()
    {
        Debug.Log($"{pStats.nameChar} turns.");
        state = BattleState.PlayerTurn;
    }

    public void OnAttackButton()
    {
        eStats.maxHP -= pStats.damage;
        Debug.Log($"{pStats.nameChar} attack {eStats.nameChar} with {pStats.damage} dmg!");
        Debug.Log($"{eStats.nameChar} drop down to {eStats.maxHP}.");

        if (eStats.maxHP == 0)
            state = BattleState.WonBattle;
        else
            state = BattleState.EnemyTurn;
    }
}
