using Interfaces;
using UnityEngine;
using UnityEngine.UI;

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
        bool wasPickedUp = Inventory.Instance.AddToInventory(wpn);
        if (wasPickedUp) Destroy(interactable.gameObject);
    }

    void ConsumablePickUp(Interactable interactable)
    {
        IConsumable c = interactable.gameObject.GetComponent<IConsumable>();
        c.IncreaseCount();
        Destroy(interactable.gameObject);
    }
}
