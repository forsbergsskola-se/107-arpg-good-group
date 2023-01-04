
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public abstract class Pet : Interactable, IItem
{
    public string Type => "Pet";
    public abstract string Name { get; set; }
    public abstract Sprite Icon { get; set; }
    public abstract float Power { get; set; }
    public abstract float Range { get; set; }
    public abstract string Description { get; set; }
    public abstract float Cooldown { get; set; }
    
    public abstract bool IsEquipped { get; set; }
    
    public static GameObject CurrEquippedPet;
    public static List<Pet> AllPets = new();

    protected abstract void Start();
    
    public static void Switch(string newPetName, bool replacePet)
    {
        SjickenPet pet = FindObjectOfType<SjickenPet>();
        if(CurrEquippedPet == null) // if no pet CurrEquipped we spawn it
            pet.SpawnPet();
        else if(replacePet) // if pet is CurrEquipped and we equip another pet in inventory we kill and spawn it
        {
            pet.KillPet();
            pet.SpawnPet();
        }
        else // if we have pet CurrEquipped and we unEquip it in inventory we kill it
            pet.KillPet();
        
        //if we are going to have more pets we can implement this like in weapon.
        /* foreach (var pet in AllPets)
        {
            if (newPetName == pet.name)
            {
                CurrEquippedPet = pet;
                FindObjectOfType<SjickenPet>().SpawnPet();
            }
        }*/
    }
}
