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
        Power = 10;
        Range = 10;
        Chargable = false;
        Description = $"Knut can throw the {Name} - {Name} has {Power} damage";
        Cooldown = 0;
        IsEquipped = false;
    }
}
