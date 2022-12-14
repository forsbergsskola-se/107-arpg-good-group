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

    public static Weapon CurrEquippedWeapon;
}
