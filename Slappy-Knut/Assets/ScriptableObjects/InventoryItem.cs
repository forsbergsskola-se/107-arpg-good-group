using UnityEngine;

public class InventoryItem : ScriptableObject
{
    public string weaponName;
    public string weaponDesc;
    public Sprite weaponIcon;
    
    public static InventorySlot EquippedSlot;
    
    public virtual void Use()
    {
        InventoryUI iUi = FindObjectOfType<InventoryUI>();
        InventorySlot newSlot = null;
        
        foreach (var t in iUi._slots)
        {
            if (t._inventoryItem.weaponName == weaponName)
            {
                newSlot = t;
                break;
            }
        }

        if (newSlot == EquippedSlot)
        {
            EquippedSlot.icon.color = Color.white;
            //unequip if selected slot is the same as the currently equipped one
            Weapon.Switch(Weapon.DefaultWeapon.Name);
            EquippedSlot = null;
        }
        else
        {
            if(EquippedSlot) EquippedSlot.icon.color = Color.white;
            newSlot.icon.color = Color.red;
            Weapon.Switch(weaponName);
            EquippedSlot = newSlot;
        }
    }
}
