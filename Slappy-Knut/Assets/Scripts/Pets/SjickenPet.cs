
using UnityEngine;

public class SjickenPet : Pet
{
    public override string Name { get; set; }
    public override Sprite Icon { get; set; }
    public override float Power { get; set; }
    public override float Range { get; set; }
    public override string Description { get; set; }
    public override float Cooldown { get; set; }
    public override bool IsEquipped { get; set; }

    [SerializeField] private Sprite _icon;

    protected override void Start()
    {
        Name = "Sjicken";
        Icon = _icon;
        Power = 9001; //over 9000?
        Range = 3;
        Description = $"This sjicken is a tool of mass destruction, point it at things u want destroyed -sjicken does {Power} damage";
        Cooldown = 0;
        IsEquipped = false;
    }
}
