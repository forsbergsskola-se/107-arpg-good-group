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
        
        // Sets gameObject to the weapon in inventoryItem
        inventoryItem.weapon = gameObject.GetComponent<Weapon>();
        //Add to inventory
        if (wasPickedUp)
        { 
            //hide the gameObject instead of destroying so i dont lose reference to it in the inventoryItem class
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
        
    }
}