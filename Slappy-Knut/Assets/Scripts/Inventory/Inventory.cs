using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    
    public static Inventory instance; //singleton
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();

    public OnItemChanged onItemChangedCallback;

    public int space = 20;
    
    public List<InventoryItem> items = new List<InventoryItem>();

    public bool Add(InventoryItem inventoryItem)
    {
        if (!inventoryItem.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }
            items.Add(inventoryItem);

            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
            
            
        }

        return true;

    }

    public void Remove(InventoryItem inventoryItem)
    {
        items.Remove(inventoryItem);
        
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
