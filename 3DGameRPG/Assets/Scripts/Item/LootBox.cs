using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    [SerializeField] DropItem drop;
    [SerializeField] Animator anim;
    [SerializeField] GameObject lootItems, emptyPopUp; //remember to add popup in 
    [SerializeField] GameObject activeLootFX, emptyLootFx;

    private void OnTriggerStay(Collider player)
    {
        if (!drop.EmptyLoot())
        {
            if (player.CompareTag("PlayerModel") && player.GetComponent<PlayeeController>().inputAction.Player.Interact.triggered)
            {
                drop.TakeItem();
                anim.SetTrigger("isOpen");
                activeLootFX.SetActive(true);
                StartCoroutine(AnnouceDropItemBox());
            }
        }
        else
        {
            if (player.CompareTag("PlayerModel") && player.GetComponent<PlayeeController>().inputAction.Player.Interact.triggered)
            {
                emptyPopUp.SetActive(true);
                emptyLootFx.SetActive(true);
                DeactiveMovement(player.GetComponent<PlayeeController>().inputAction);
            }

            if (player.CompareTag("PlayerModel") && player.GetComponent<PlayeeController>().inputAction.Player.SkipDialogue.triggered)
            {
                emptyLootFx.SetActive(false);
                player.GetComponent<PlayeeController>().inputAction.Player.Interact.Enable();
            }
        }
    }

    IEnumerator AnnouceDropItemBox()
    {
        lootItems.SetActive(true);
        LoadItemNameIn();

        yield return new WaitForSeconds(.3f);
        drop.AlreadyCollected();
        activeLootFX.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        anim.SetTrigger("isClose");
        lootItems.SetActive(false);
    }

    void LoadItemNameIn()
    {
        string anItem, getList;
        getList = "GET: ";
        for (int i = 0; i < drop.itemList.Count; i++)
        {
            if (i == drop.itemList.Count - 1)
                anItem = drop.itemList[i].itemName + ".";
            else anItem = drop.itemList[i].itemName + "; ";
            getList += anItem;
        }
        lootItems.GetComponent<TMP_Text>().text = getList;
    }

    void DeactiveMovement(PlayerInput input)
    {
        input.Player.Movement.Disable();
        input.Player.Running.Disable();
        input.Player.Interact.Disable();
    }
}
