using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon; //Reference the icon in Unity
    public Button removeButton;
    
    //testing
    public InventoryItem _inventoryItem;
    //private InventoryItem _inventoryItem;
    
    public void AddItem(InventoryItem newInventoryItem)//Makes the icon appear
    {
        _inventoryItem = newInventoryItem;
        
        icon.sprite = _inventoryItem.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        _inventoryItem = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.Instance.Remove(_inventoryItem);
    }

    public void UseItem()
    {
        if (_inventoryItem != null)
        {
            _inventoryItem.Use();
        }
    }
}