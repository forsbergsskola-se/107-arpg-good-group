using UnityEngine;

public class Hand : Weapon
{
    [SerializeField] private float _power = 2f; 
    public GameObject handPrefab;
   // private GameObject _attackPoint;
    
    private void Start()
    {
        Chargable = false; // Do we want to charge the sword for heavier slap attack?
        Power = _power;
        Description = $"Knut only needs his hands of fury. -Hand has {_power} damage";
        Cooldown = 0;
        Range = 2;
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
        //find attackPoint(the hand of Knut)
        GameObject target = GameObject.FindWithTag("AttackPoint");
        // spawn prefab of weapon and put it as child of attackPoint(hand)
        Instantiate(handPrefab,target.transform);
        // deletes old currWeapon from hand
        Destroy(target.transform.GetChild(0).gameObject);
    }
}
