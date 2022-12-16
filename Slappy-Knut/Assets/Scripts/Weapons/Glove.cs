using UnityEngine;

public class Glove : Weapon
{
    [SerializeField] private float _power = 4f;
    public GameObject glovePrefab;
    private void Start()
    {
        Power = _power;
        Chargable = true;
        ChargeTime = 2;
        Power = 10;
        Description = $"Knut can really hit as hard as he charges, the soft glove protects his hand -glove has {Power} damage";
        Cooldown = 0;
        Range = 2;
        Equipable = true;
    }
}
