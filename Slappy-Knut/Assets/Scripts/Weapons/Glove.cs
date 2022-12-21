using UnityEngine;

public class Glove : Weapon
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
        Name = "Glove";
        Icon = _icon;
        Power = 10;
        Range = 2;
        Chargable = false;
        ChargeTime = 2;
        Description = $"Knut can really hit as hard as he charges, the soft glove protects his hand -glove has {Power} damage";
        Cooldown = 0;
        IsEquipped = false;
    }
}
