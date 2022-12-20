using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Weapon : MonoBehaviour, IItem
{
    public string Name { get; set; }
    public Sprite Icon { get; set; }
    public float Power { get; set; }
    public string Description { get; set; }
    public float Cooldown { get; set; }
    public float ChargeTime { get; set; }
    public float Range { get; set; }
    public bool Equipable { get; set; }
    public bool Chargable { get; set; }
    
    public static Weapon CurrEquippedWeapon;
    public static List<Weapon> AllWeapons = new List<Weapon>();
    public static Weapon DefaultWeapon;

    public static void Switch(Weapon newWeapon)
    {
        foreach (var weapon in AllWeapons)
            if (newWeapon.name == weapon.name)
                newWeapon = weapon;
        CurrEquippedWeapon.gameObject.SetActive(false);
        CurrEquippedWeapon = newWeapon;
        CurrEquippedWeapon.gameObject.SetActive(true);
    }

}
