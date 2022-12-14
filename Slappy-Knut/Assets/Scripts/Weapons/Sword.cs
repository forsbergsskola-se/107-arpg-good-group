using UnityEngine;

public class Sword : Weapon
{
    private float _power;
    
    private void Start()
    {
        Chargable = false; // Do we want to charge the sword for heavier slap attack?
        Power = _power;
        Description = $"Slapping Sword has {_power} damage";
        Cooldown = 0;
        Range = 5;
        Equipable = false;
    }
    private void Update()
    {
        if (Equipable)
            EquippingWeapon(); //<---once
    }

    public void EquipAWeapon(bool equip)
    { 
        //for testing purpose
        if (Input.GetKeyDown(KeyCode.T)) 
            Equipable = true;

        //Should be changed in inventory to control the equipped weapon.
        Equipable = equip;
        CurrEquippedWeapon = Equipable ? this : null;
    }

    private void EquippingWeapon()
    {
        // spawn prefab of weapon and put it on player 
    }
}
