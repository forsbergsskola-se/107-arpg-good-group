using UnityEngine;

public class Glove : Weapon
{
    [SerializeField] private float _power;
    
    private void Start()
    {
        Chargable = true;
        ChargeTime = 5;
        Power = _power;
        Description = $"Knut can really hit as hard as he charges, the soft glove protects his hand -glove has {_power} damage";
        Cooldown = 0;
        Range = 2;
        Equipable = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("equipped");
            EquipWeapon();
        }
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
