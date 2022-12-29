using System.Collections.Generic;
using Interfaces;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance; //singleton
    public static List<InventorySlot> Slots = new();
    public static InventorySlot EquippedSlot;
    public static InventorySlot EquippedPetSlot;
    public static GameObject DescriptionBox;
    public static TextMeshProUGUI DescriptionText;
    public static GameObject inventoryUI;

    void Start()
    {
        inventoryUI = GameObject.FindGameObjectWithTag("Inventory");
        Instance = this;
        Slots.AddRange(GetComponentsInChildren<InventorySlot>());
        DescriptionBox = GameObject.FindGameObjectWithTag("DescriptionBox");
        DescriptionText = DescriptionBox.GetComponentInChildren<TextMeshProUGUI>();
        
        // hide on start
        DescriptionBox.SetActive(false);
        inventoryUI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && PauseGame.IsPaused == false)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            if (inventoryUI.activeSelf) DescriptionBox.SetActive(false);
        }
    }
    
    public bool AddToInventory(IItem item)
    {
        InventorySlot firstAvailableSlot = null;
        
        foreach (InventorySlot slot in Slots)
        {
            if (slot.itemName == "")
            {
                firstAvailableSlot = slot;
                break;
            }
            if (slot == Slots[^1] && slot.itemName != "")
            {
                Debug.Log("Not enough space in inventory."); // test this
                return false;
            }
        }
        firstAvailableSlot.AddItem(item);
        
        return true;
    }
}