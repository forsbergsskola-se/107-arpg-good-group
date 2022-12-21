using UnityEngine;

public class Hand : Weapon
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
        Name = "Hand";
        Icon = _icon;
        Power = 4;
        Range = 2;
        Chargable = false;
        ChargeTime = 0;
        Description = $"Knut only needs his hands of fury. -Hand has {Power} damage";
        Cooldown = 0;
        IsEquipped = false;
    }
}
