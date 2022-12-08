using UnityEngine;

public class AntiAnxietyPotion : Consumable
{
    public int count;
    [SerializeField] private float power;
    
    public override bool Chargeable { get; }
    public override float Power { get; }
    public override string Description { get; }
    public override float Cooldown { get; }
    public override float Range { get; }
    public override int Count { get; set; }

    public AntiAnxietyPotion()
    {
        Chargeable = false;
        Power = power;
        Description = $"Potion that lowers rage by {power}";
        Cooldown = 10;
        Range = 0;
    }
    
    public override void Use(GameObject player)
    {
        player.GetComponent<PlayerCore>()._currentRage -= Power;
        Count--;
    }

    public override void Charge()
    {
        throw new System.NotImplementedException();
    }
}
