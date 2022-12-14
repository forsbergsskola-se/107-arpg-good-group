using Interfaces;
using UnityEngine;

public class Weapon : MonoBehaviour, IItem
{
    public float Power { get; set; }
    public string Description { get; set; }
    public float Cooldown { get; set; }
    public float Range { get; set; }
    public bool Equipable { get; set; }
    public bool Chargable { get; set; }

    protected static Weapon CurrEquippedWeapon;

    private void Update()
    {
        //Debug.Log(CurrEquippedWeapon);
        if(CurrEquippedWeapon!=null)
            Debug.Log(CurrEquippedWeapon.Power);
    }

    public Weapon CurrWeaponEquipped()
    {
        return CurrEquippedWeapon;
    }

    public void Use()
    {
        //equip the weapon
        //remove from inventory?

    }
}
