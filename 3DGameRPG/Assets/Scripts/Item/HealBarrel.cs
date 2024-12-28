using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealBarrel : MonoBehaviour
{
    [SerializeField] GameObject activeLootFX, popUp; 
    //remember to add popup in 

    private void OnTriggerStay(Collider player)
    {
        if (player.CompareTag("PlayerModel") && player.GetComponent<PlayeeController>().inputAction.Player.Interact.triggered)
        {
            HealFullAll(player.GetComponent<PlayerStat>(), player.GetComponent<PlayeeController>().inputAction);
            player.GetComponent<PlayeeController>().inputAction.Player.Interact.Disable();
        }

        if (player.CompareTag("PlayerModel") && player.GetComponent<PlayeeController>().inputAction.Player.SkipDialogue.triggered)
        {
            activeLootFX.SetActive(false);
            player.GetComponent<PlayeeController>().inputAction.Player.Interact.Enable();
        }
    }

    void HealFullAll(PlayerStat stat, PlayerInput input)
    {
        stat.HPRemain = stat.MaxHPStat();
        Debug.Log("player heal" + stat.HPRemain);
        for (int i = 0; i < stat.AmountOfRobots(); i++)
        {
            stat.ChooseRobot(i).health = stat.ChooseRobot(i).maxHP;
        }
        activeLootFX.SetActive(true);
        StartCoroutine(AnnouceHealBox(input));
    }

    IEnumerator AnnouceHealBox(PlayerInput input)
    {
        yield return new WaitForSeconds(.3f);
        popUp.SetActive(true);

        //Time.timeScale = 0;
        input.Player.Movement.Disable();
        input.Player.Running.Disable();
    }
}
