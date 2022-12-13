using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public float power;
    public override bool Chargeable { get; }
    public override float Power { get; }
    public override string Description { get; }
    public override float Cooldown { get; }
    public override float Range { get; }


    public Sword()
    {
        Chargeable = false; // Do we want to charge the sword for heavier slap attack?
        Power = power;
        Description = $"Slapping Sword has {power} damage";
        Cooldown = 0;
        Range = 5;
    }
    
    private void Awake()
    {

    }

   
    
    private void Update()
    {

    }
    
    public override void Use()
    {

    }
    
    public override void Charge()
    {
        // the item can not be charged
        throw new System.NotImplementedException();
    }
}
