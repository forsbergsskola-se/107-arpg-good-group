using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    
    public static Inventory Instance; //singleton
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        Instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;
    public int space = 6;
    public List<InventoryItem> items = new List<InventoryItem>();

    public bool Add(InventoryItem inventoryItem)
    {
        if (items.Count >= space)
        {
            Debug.Log("Not enough room.");
            return false;
        }
        items.Add(inventoryItem);

        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(InventoryItem inventoryItem)
    {
        items.Remove(inventoryItem);
        
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
    }
}