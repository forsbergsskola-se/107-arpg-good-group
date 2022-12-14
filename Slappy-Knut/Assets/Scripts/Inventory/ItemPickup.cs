using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public InventoryItem inventoryItem;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up item " + inventoryItem.name);
        bool wasPickedUp = Inventory.instance.Add(inventoryItem);
        //Add to inventory
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
        
    }
}
