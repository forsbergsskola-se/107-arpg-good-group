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
    public List<InventoryItem> items = new();

    public bool Add(InventoryItem inventoryItem)
    {
        if (items.Count >= space)
        {
            Debug.Log("Not enough room.");
            return false;
        }

        foreach (InventoryItem item in items)
        {
            if (item.weaponName == inventoryItem.weaponName)
            {
                Debug.Log("Weapon already owned.");
                return false;
            }
        }
        items.Add(inventoryItem);

        if (OnItemChangedCallback != null) OnItemChangedCallback.Invoke();
        return true;
    }

    public void Remove(InventoryItem inventoryItem)
    {
        Weapon.Switch(Weapon.DefaultWeapon.Name);
        InventoryItem.EquippedSlot.icon.color = Color.white;
        InventoryItem.EquippedSlot = null;
        items.Remove(inventoryItem);
        
        if (OnItemChangedCallback != null) OnItemChangedCallback.Invoke();
    }
}