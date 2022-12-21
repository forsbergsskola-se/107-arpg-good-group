using UnityEngine;

public class Glove : Weapon
{
    public Sprite icon;
    [SerializeField] private float _power = 4f;
    public GameObject glovePrefab;
    private void Start()
    {
        Icon = icon;
        Power = _power;
        Chargable = true;
        ChargeTime = 2;
        Power = _power;
        Description = $"Knut can really hit as hard as he charges, the soft glove protects his hand -glove has {Power} damage";
        Cooldown = 0;
        Range = 2;
        Equipable = true;
    }
}
