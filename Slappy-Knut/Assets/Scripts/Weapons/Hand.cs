using System;
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
}
