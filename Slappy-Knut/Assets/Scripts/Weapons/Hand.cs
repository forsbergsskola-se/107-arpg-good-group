using UnityEngine;

public class Hand : Weapon
{
    [SerializeField] private float _power = 4f;
    public GameObject handPrefab;
    private void Start()
    {
        Power = _power;
        Chargable = false; // Do we want to charge the sword for heavier slap attack?
        Power = 10;
        Description = $"Knut only needs his hands of fury. -Hand has {Power} damage";
        Cooldown = 0;
        Range = 2;
        Equipable = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) EquipWeapon();
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
        //could maybe call this in weapon from every inherited from it, but needs then to find its own prefab and find it
        
        //find attackPoint(the hand of Knut)
        GameObject target = GameObject.FindWithTag("AttackPoint");
        // deletes old currWeapon from hand
        Destroy(target.transform.GetChild(0).gameObject);
        // spawn prefab of weapon and put it as child of attackPoint(hand)
        Instantiate(handPrefab,target.transform);
      
    }
}
