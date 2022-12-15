using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    
    public virtual void Use()
    {
       

        //Use the item
        //Something might happen
        //Todo: find the current script attached to the object. --> FindObjectOfType<Glove>().EquipWeapon();
        
        //Todo: find icon parent red after equipped

        Debug.Log("Equipping " + name);
    }
}
