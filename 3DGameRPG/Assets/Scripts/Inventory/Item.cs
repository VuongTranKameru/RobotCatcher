using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private int quantity;
    [SerializeField] private Sprite sprite;

    protected InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    /*private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            inventoryManager.AddItem(itemName, quantity, sprite);
            Destroy(gameObject);
        }
    }*/
}
