using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public Weapon weapon;
    
    public virtual void Use()
    {
        // checks if weapon is Glove and if its glove it equips it,
        // if we have more items we can do if(weapon is Sword) maybe better ways but this works atm.
        // need to differentiate from the consumables.
        if (weapon is Glove)
        {
            Debug.Log("Equipping " + name);
            FindObjectOfType<Glove>().EquipWeapon();
        }
        if(weapon is Hand)
        {
            Debug.Log("Equipping " + name);
            FindObjectOfType<Hand>().EquipWeapon();
        }
        //Use the item
        //Something might happen

        //Todo: find icon parent red after equipped
        InventoryUI iUi = FindObjectOfType<InventoryUI>();
        InventorySlot inventorySlot = null;
        foreach (var t in iUi._slots)
        {
            //Debug.Log(iUi._slots[i]._inventoryItem);
            if (t._inventoryItem.name == name)
            {
                t.icon.color = Color.red;
                inventorySlot = t;
                break;
            }
            //t.icon.color = Color.white;
        }

        // shit mix to make everthing other than equipped white, because of above nullReference error when trying to compare to null, works atm!
        foreach (var t in iUi._slots)
        {
            inventorySlot.icon.color = Color.red;
            t.icon.color = Color.white;
        }
    }
}
