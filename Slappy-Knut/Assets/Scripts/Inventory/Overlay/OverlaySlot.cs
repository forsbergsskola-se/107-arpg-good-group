using UnityEngine;
using UnityEngine.UI;

public class OverlaySlot : MonoBehaviour
{
    public Image icon; //Reference the icon in Unity

    
    //testing
    public InventoryItem _inventoryItem;
    //private InventoryItem _inventoryItem;
    
    public void EnableItem(InventoryItem newInventoryItem)//Makes the icon appear
    {
        //TODO: Colorize the icon
        _inventoryItem = newInventoryItem;
        icon.sprite = _inventoryItem.icon;
        icon.enabled = true;
    }

    public void DisableSlot()
    {
        //TODO: gray out the icon
        // _inventoryItem = null;
        // icon.sprite = null;
        // icon.enabled = false;
    }

    public void UseItem()
    {
        if (_inventoryItem != null)
        {
            _inventoryItem.Use();
        }
    }
}