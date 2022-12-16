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
        //Works if we only have 1 of the same things, it finds the first name that matches the uiName and makes it red
        InventoryUI iUi = FindObjectOfType<InventoryUI>();
        InventorySlot equipped = null;
        foreach (var t in iUi._slots)
        {
            if (t._inventoryItem.name == name)
            {
                //t.icon.color = Color.red;
                equipped = t;
                break;
            }
            //t.icon.color = Color.white;
        }

        //Rest is set to white to display which is the equipped one.
        // shit mix to make everything other than equipped white, because of above nullReference error when trying to compare to null, works atm!
        foreach (var t in iUi._slots)
        {
            //inventorySlot.icon.color = Color.red;
            t.icon.color = Color.white;
        }

        if (equipped != null) equipped.icon.color = Color.red;
    }
}
