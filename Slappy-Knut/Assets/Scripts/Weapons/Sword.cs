using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{

    private void Start()
    {
        Chargeable = false; // Do we want to charge the sword for heavier slap attack?
        Power = power;
        Description = $"Slapping Sword has {power} damage";
        Cooldown = 0;
        Range = 5;
    }
    private void Update()
    {

    }
}
