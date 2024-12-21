using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class ConversationInput : MonoBehaviour
{
    void Update()
    {
        if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                ConversationManager.Instance.SelectNextOption();
            if (Input.GetKeyDown(KeyCode.DownArrow))
                ConversationManager.Instance.SelectPreviousOption();
            if (Input.GetKeyDown(KeyCode.RightArrow))
                ConversationManager.Instance.PressSelectedOption();
            if (Input.GetKeyDown(KeyCode.Escape))
                ConversationManager.Instance.EndConversation();
        }
    }
}
