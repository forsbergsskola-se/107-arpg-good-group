using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public abstract class Weapon : Interactable, IItem
{
    public abstract string Name { get; set; }
    public abstract Sprite Icon { get; set; }
    public abstract float Power { get; set; }
    public abstract string Description { get; set; }
    public abstract float Cooldown { get; set; }
    public abstract bool Chargable { get; set; }
    public abstract float ChargeTime { get; set; }
    public abstract float Range { get; set; }
    public abstract bool IsEquipped { get; set; }

    protected abstract void Start();

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
