using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    BeginBattle,
    PlayerTurn,
    EnemyTurn,
    DrawBattle,
    LeaveBattle,
    WonBattle,
    LoseBattle
}

public enum AffectSkill
{
    Normal,
    Dizzy
}