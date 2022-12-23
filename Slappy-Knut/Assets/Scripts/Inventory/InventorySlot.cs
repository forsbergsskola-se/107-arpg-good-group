using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon; //Reference the icon in Unity
    public Button removeButton;
    
    //testing
    [HideInInspector] public InventoryItem _inventoryItem;
    public GameObject _descriptionBox;
    public TextMeshProUGUI _descriptionText;

    public void ShowDescription()
    {
        if (_inventoryItem != null)
        {
            _descriptionBox.SetActive(true);
            _descriptionText.text = _inventoryItem.weapon.Description;
        }
    }

    public void HideDescription()
    {
        _descriptionBox.SetActive(false);
    }
    public void AddItem(InventoryItem newInventoryItem)//Makes the icon appear
    {
        _inventoryItem = newInventoryItem;
        
        icon.sprite = _inventoryItem.weapon.Icon;
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