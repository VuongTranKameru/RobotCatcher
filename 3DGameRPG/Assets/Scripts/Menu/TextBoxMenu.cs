using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxMenu : MonoBehaviour
{
    [SerializeField] PlayerInput input;
    [SerializeField] GameObject popUpBox, emptyLootBox;

    void Start()
    {
        input = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<PlayeeController>().inputAction;
    }

    void FixedUpdate()
    {
        if (popUpBox.activeInHierarchy && input.Player.SkipDialogue.triggered)
        {
            popUpBox.SetActive(false);
            EnabledMoving();
        }

        if (emptyLootBox.activeInHierarchy && input.Player.SkipDialogue.triggered)
        {
            emptyLootBox.SetActive(false);
            EnabledMoving();
        }
    }

    void UnenabledMoving()
    {
        Time.timeScale = 0;
        input.Player.Movement.Disable();
        input.Player.Running.Disable();
        input.Player.Jumping.Disable();
    }

    void EnabledMoving()
    {
        Time.timeScale = 1;
        input.Player.Movement.Enable();
        input.Player.Running.Enable();
        input.Player.Jumping.Enable();
    }
}
