using Interfaces;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    public string itemName;
    public string itemDesc;
    public string itemType;
    public Image icon;
    public Button removeButton;
    
    public void ShowDescription()
    {
        if (itemName != "")
        {
            Inventory.DescriptionText.text = itemDesc;
            Inventory.DescriptionBox.SetActive(true);
        }
    }
    public void HideDescription() => Inventory.DescriptionBox.SetActive(false);
    public void AddItem(IItem item)
    {
        itemName = item.Name;
        itemDesc = item.Description;
        itemType = item.Type;
        icon.sprite = item.Icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }
    public void OnRemoveButton()
    {
        itemName = "";
        itemDesc = "";
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        if (Inventory.EquippedSlot == this) Unequip();
        if (Inventory.EquippedPetSlot == this) UnequipPet();
    }
    public void UnequipPet()
    {
        Inventory.EquippedPetSlot = null;
        icon.color = Color.white;
        Pet.Switch(itemName,false);
    }
    public void Unequip()
    {
        Inventory.EquippedSlot = null;
        icon.color = Color.white;
        Weapon.Switch(Weapon.DefaultWeapon.name);
    }

    public void UseItem()
    {
        if (this == null) return; // no item in slot, return
        if (this == Inventory.EquippedSlot) Unequip();
        if(this == Inventory.EquippedPetSlot) UnequipPet();
        else
        {
            // equipping new weapon
            if (itemType == "Pet")
            {
                if (Inventory.EquippedPetSlot)
                    Inventory.EquippedPetSlot.icon.color = Color.white;
                
                Inventory.EquippedPetSlot = this;
                //If we have a pet equipped and push another slot that contains pet it does nothing but change the equipped slot to green.
                //Else if Pet.CurrEquippedPet is null we instantiate it
                //need to change this if we have more pets, then we just need to call the void KillPet() and then void SpawnPet() to replace the pet
                if(Pet.CurrEquippedPet == null)
                    Pet.Switch(itemName, false);
                else
                    Pet.Switch(itemName,true);
                icon.color = Color.green;
            }
            else if(itemType == "Weapon")
            {
                if (Inventory.EquippedSlot)
                    Inventory.EquippedSlot.icon.color = Color.white; // if other weapon  already equipped, color it white
                Debug.Log(this);
                Inventory.EquippedSlot = this;
                Weapon.Switch(itemName);
                icon.color = Color.red;
            }
        }
    }
}