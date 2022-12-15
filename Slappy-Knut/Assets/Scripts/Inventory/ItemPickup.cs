using UnityEngine;

public class ItemPickup : Interactable
{
    public InventoryItem inventoryItem;

    protected override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up item " + inventoryItem.name);
        bool wasPickedUp = Inventory.Instance.Add(inventoryItem);
        //Add to inventory
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
        
    }
}