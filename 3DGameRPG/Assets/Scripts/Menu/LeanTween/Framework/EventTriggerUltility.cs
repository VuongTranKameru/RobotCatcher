using UnityEngine;
using UnityEngine.EventSystems;

public static class EventTriggerUtility
{
    public static void AddHoverEvent(GameObject target, UnityEngine.Events.UnityAction onEnter, UnityEngine.Events.UnityAction onExit)
    {
        EventTrigger trigger = target.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = target.AddComponent<EventTrigger>();
        }

        
        EventTrigger.Entry entryEnter = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        entryEnter.callback.AddListener((eventData) => onEnter.Invoke());
        trigger.triggers.Add(entryEnter);

    
        EventTrigger.Entry entryExit = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        entryExit.callback.AddListener((eventData) => onExit.Invoke());
        trigger.triggers.Add(entryExit);
    }
}
