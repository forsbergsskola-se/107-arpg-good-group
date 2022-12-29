using UnityEngine;

public class Rock : Weapon
{
    public override string Name { get; set; }
    public override Sprite Icon { get; set; }
    public override float Power { get; set; }
    public override float Range { get; set; }
    public override bool Chargable { get; set; }
    public override string Description { get; set; }
    public override float Cooldown { get; set; }
    public override float ChargeTime { get; set; }
    public override bool IsEquipped { get; set; }

    [SerializeField] private Sprite _icon;
    
    protected override void Start()
    {
        Name = "Rock";
        Icon = _icon;
        Power = 15;
        Range = 10;
        Chargable = false;
        Cooldown = 5;
        IsEquipped = false;
        Description = $"Knut can throw the {Name} - Rock tires his arm so he can't attack for {Cooldown} seconds. " +
                      $"{Name} has {Power} damage";
    }
}
