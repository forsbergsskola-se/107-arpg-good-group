using UnityEngine;

public class Hand : Weapon
{
    private void Start()
    {
        Chargable = false; // Do we want to charge the sword for heavier slap attack?
        Power = 10;
        Description = $"Knut only needs his hands of fury. -Hand has {Power} damage";
        Cooldown = 0;
        Range = 2;
        Equipable = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) EquipWeapon();
    }

    public void EquipWeapon()
    {
        //Should be called in inventory to equip a weapon
        CurrEquippedWeapon = this;
        
        EquippingWeapon();
    }

    private void EquippingWeapon()
    {
        // spawn prefab of weapon and put it on player 
    }
}
