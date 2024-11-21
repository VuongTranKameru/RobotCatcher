using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    BeginBattle,
    PlayerTurn,
    EnemyTurn,
    LeaveBattle,
    WonBattle,
    LoseBattle
}

public enum AffectSkill
{
    Normal,
    Dizzy
}