using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public Weapon weapon;
    private static InventorySlot _equippedSlot;
    
    public virtual void Use()
    {
        InventoryUI iUi = FindObjectOfType<InventoryUI>();
        InventorySlot newSlot = null;
        
        foreach (var t in iUi._slots)
        {
            if (t._inventoryItem.name == name)
            {
                newSlot = t;
                break;
            }
        }

        if (newSlot == _equippedSlot)
        {
            Debug.Log("Equipping " + name);
            Weapon.Switch(Weapon.DefaultWeapon);
            _equippedSlot = null;
        }
        else
        {
            Debug.Log("Equipping " + name);
            Weapon.Switch(weapon);
            _equippedSlot = newSlot;
        }

        
        //Rest is set to white to display which is the equipped one.
        // shit mix to make everything other than equipped white, because of above nullReference error when trying to compare to null, works atm!
        foreach (var t in iUi._slots)
        {
            
            //inventorySlot.icon.color = Color.red;
            t.icon.color = Color.white;
        }

        if (_equippedSlot != null) _equippedSlot.icon.color = Color.red;
    }
}
