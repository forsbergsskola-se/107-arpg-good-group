using UnityEngine;

public class Glove : Weapon
{
    [SerializeField] private float _power = 4f;
    public GameObject glovePrefab;
    private void Start()
    {
        Chargable = true;
        Power = _power;
        Description = $"Knut can really hit as hard as he charges, the soft glove protects his hand -glove has {_power} damage";
        Cooldown = 0;
        Range = 2;
        Equipable = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) 
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
        Instantiate(glovePrefab,target.transform);
        // deletes old currWeapon from hand
        Destroy(target.transform.GetChild(0).gameObject);
    }
}
