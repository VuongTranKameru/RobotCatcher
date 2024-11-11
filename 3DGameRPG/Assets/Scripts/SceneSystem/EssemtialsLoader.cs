using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssemtialsLoader : MonoBehaviour
{
    [SerializeField] GameObject Player;

    //only using to spawn out the character for the first time
    private void Awake()
    {
        //load ui system manager (health, inventory, option..)

        //load player
        if (PlayerManager.instance == null)
        {
            Instantiate(Player);
            PlayerStat initialStat = Player.GetComponent<PlayerStat>();
            initialStat.HPRemain = initialStat.MaxHPStat();

            FullHPRobotOwned(initialStat);
        }
    }

    #region For TestManager Only
    void FullHPRobotOwned(PlayerStat stat)
    {
        stat.UsedThatRobot().HPRemain = stat.UsedThatRobot().MaxHPStat();
    }
    #endregion
}
