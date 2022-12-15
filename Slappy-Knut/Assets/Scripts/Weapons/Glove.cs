using UnityEngine;

public class Glove : Weapon
{
    [SerializeField] private float _power = 4f;
    public GameObject glovePrefab;
    private void Start()
    {
        Chargable = true;
        ChargeTime = 2;
        Power = 10;
        Description = $"Knut can really hit as hard as he charges, the soft glove protects his hand -glove has {Power} damage";
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
        //could maybe call this in weapon from every inherited from it, but needs then to find its own prefab and find it
        
        
        //find attackPoint(the hand of Knut)
        GameObject target = GameObject.FindWithTag("AttackPoint");
        // deletes old currWeapon from hand
        Destroy(target.transform.GetChild(0).gameObject);
        // spawn prefab of weapon and put it as child of attackPoint(hand)
        Instantiate(glovePrefab,target.transform);
      
    }
}
