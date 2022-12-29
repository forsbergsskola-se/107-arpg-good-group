
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
    
    public static Pet CurrEquippedPet;
    public static List<Pet> AllPets = new();

    protected abstract void Start();
    
    public static void Switch(string newPetName)
    {
        if(CurrEquippedPet == null)
        {
            FindObjectOfType<SjickenPet>().SpawnPet();
            
            AllPets.Add(FindObjectOfType<SjickenPet>());
            Debug.Log(AllPets[0]);
            CurrEquippedPet = AllPets[0];
        }
        else
        {
            //Destroy(AllPets[0].gameObject);
            //Debug.Log("hvenær kem eg hingað?");
            //FindObjectOfType<SjickenMovement>().gameObject.SetActive(false);
           //Destroy(FindObjectOfType<SjickenMovement>().gameObject);
        }
        // foreach (var pet in AllPets)
        //{
            //if (newPetName == pet.name)
           // {
            //    CurrEquippedPet = pet;
            //    FindObjectOfType<SjickenPet>().SpawnPet();
           //     Debug.Log("kem eg hingað?");
           // }
        //}
    }
    
}
