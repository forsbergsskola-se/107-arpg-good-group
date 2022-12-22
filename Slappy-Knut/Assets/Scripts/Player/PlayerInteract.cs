using Interfaces;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public void Interact(Interactable interactable)
    {
        if (interactable.GetComponent<Weapon>()) WeaponPickUp(interactable);
        else if (interactable.GetComponent<IConsumable>() != null) ConsumablePickUp(interactable);
        else if (interactable.GetComponent<IDamagable>() != null) GetComponent<PlayerAttack>().AttackAnimation();
    }
    
    void WeaponPickUp(Interactable interactable)
    {
        Weapon wpn = interactable.GetComponent<Weapon>();
        InventoryItem inventoryItem = ScriptableObject.CreateInstance<InventoryItem>();
        inventoryItem.icon = wpn.Icon;
        inventoryItem.name = wpn.Name;
        
        bool wasPickedUp = Inventory.Instance.Add(inventoryItem);
        
        // Sets gameObject to the weapon in inventoryItem
        inventoryItem.weapon = wpn;
        //Add to inventory
        if (wasPickedUp)
        { 
            interactable.gameObject.SetActive(false);
        }
    }

    void ConsumablePickUp(Interactable interactable)
    {
        IConsumable c = interactable.gameObject.GetComponent<IConsumable>();
        c.IncreaseCount();
        Destroy(interactable.gameObject);
    }
}
