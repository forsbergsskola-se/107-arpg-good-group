using UnityEngine;

public class FishLandmine : Consumable
{
    public float power;
    
    public override bool Chargeable { get; }
    public override float Power { get; }
    public override string Description { get; }
    public override float Cooldown { get; }
    public override float Range { get; }
    public override int Count { get; set; }

    public FishLandmine()
    {
        Chargeable = false;
        Power = power;
        Description = "Fish that causes damage when you get too close.";
        Cooldown = 20;
        Range = 10;
    }

    public override void Use(GameObject player)
    {
        gameObject.transform.position = player.transform.position;
        Count--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other is not IDamagable damageable) return;
        damageable.TakeDamage(Power, gameObject);
        Destroy(gameObject);
    }

    public override void Charge()
    {
        throw new System.NotImplementedException();
    }
}
