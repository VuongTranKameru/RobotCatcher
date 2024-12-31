using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RobotStatMenu : MonoBehaviour
{
    [SerializeField] TMP_Text charName;
    [SerializeField] TMP_Text currentHp;
    [SerializeField] TMP_Text attack;
    [SerializeField] TMP_Text def;
    [SerializeField] TMP_Text speed;

    [SerializeField] PlayerStat playerStat;

    private void OnEnable()
    {
        playerStat = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<PlayerStat>();

        charName.text = playerStat.NameStat();
        currentHp.text = playerStat.HPRemain.ToString();
        attack.text = playerStat.AttackStat().ToString();
        def.text = playerStat.DefenseStat().ToString();
        speed.text = playerStat.SpeedStat().ToString();
    }
}
