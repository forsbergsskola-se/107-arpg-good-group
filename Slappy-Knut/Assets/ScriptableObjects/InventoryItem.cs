using UnityEngine;

public class InventoryItem : ScriptableObject
{
    public Weapon weapon;
    
    private static InventorySlot _equippedSlot;
    
    public virtual void Use()
    {
        InventoryUI iUi = FindObjectOfType<InventoryUI>();
        InventorySlot newSlot = null;
        
        foreach (var t in iUi._slots)
        {
            if (t._inventoryItem.weapon.Name == weapon.Name)
            {
                newSlot = t;
                break;
            }
        }

        if (newSlot == _equippedSlot)
        {
            _equippedSlot.icon.color = Color.white;
            //unequip if selected slot is the same as the currently equipped one
            Weapon.Switch(Weapon.DefaultWeapon);
            _equippedSlot = null;
        }
        else
        {
            if(_equippedSlot) _equippedSlot.icon.color = Color.white;
            newSlot.icon.color = Color.red;
            Weapon.Switch(weapon);
            _equippedSlot = newSlot;
        }
    }
}
