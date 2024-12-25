using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent; //save OG parent
        transform.SetParent(transform.root); //above other canvas
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f; //semi-transparent during drag
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // follow the mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // enable raycasts
        canvasGroup.alpha = 1f; //no longer transparent

        ItemSlot dropSlot = eventData.pointerEnter?.GetComponent<ItemSlot>(); //Slot where item dropped

        if (dropSlot == null)
        {
            GameObject dropItem = eventData.pointerEnter;
            if(dropItem != null )
            {
                dropSlot = dropItem.GetComponentInParent<ItemSlot>();
            }
        }

        ItemSlot originalSlot = originalParent.GetComponent<ItemSlot>();

        if (dropSlot != null)
        {
            // is a slot under drop point
            if(dropSlot.currentItem != null)
            {
                //slot has an item - swap items
                dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                originalSlot.currentItem = dropSlot.currentItem;
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                originalSlot.currentItem = null;
            }

            //move item into drop Slot
            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject;
        }
        else
        {
            //no slot under drop point
            transform.SetParent(originalParent);
        }

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // center
    }
}
