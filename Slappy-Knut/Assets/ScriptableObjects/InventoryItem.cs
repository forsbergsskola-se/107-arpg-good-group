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
        
        InventorySlot inventorySlot = FindObjectOfType<InventorySlot>();
        //inventorySlot.icon.color = Color.red;
        //inventorySlot.icon.GetComponentInParent<Image>().color = Color.red;

        InventoryUI iUi = FindObjectOfType<InventoryUI>();
        foreach (var t in iUi._slots)
        {
            //Debug.Log(iUi._slots[i]._inventoryItem);
            if (t._inventoryItem.name == name)
            {
                t.icon.color = Color.red;
                break;
            }
            t.icon.color = Color.white;
        }
    }
}
