using UnityEngine;

public class Sword : Weapon
{
    [SerializeField] private float _power;
    
    private void Start()
    {
        Chargable = false; // Do we want to charge the sword for heavier slap attack?
        Power = _power;
        Description = $"Slapping Sword has {_power} damage";
        Cooldown = 0;
        Range = 5;
        Equipable = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
            EquipWeapon();
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
