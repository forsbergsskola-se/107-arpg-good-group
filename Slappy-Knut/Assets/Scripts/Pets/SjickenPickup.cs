using UnityEngine;
using UnityEngine.AI;

public class SjickenPickup : Pet
{
    // Start is called before the first frame update
    public override string Name { get; set; }
    public override Sprite Icon { get; set; }
    public override float Power { get; set; }
    public override float Range { get; set; }
    public override string Description { get; set; }
    public override float Cooldown { get; set; }
    public override GameObject Prefab { get; set; }
    public override bool IsEquipped { get; set; }
    public override GameObject Player { get; set; }
    public override Rigidbody Rb { get; set; }
    public override Animator Anim { get; set; }
    public override NavMeshAgent Agent { get; set; }

    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _prefab;
    protected override void Start()
    {
        Name = "Sjicken";
        Icon = _icon;
        Power = 9001; //over 9000?
        Range = 3;
        Description =
            $"{Name} is a tool of mass destruction, but it refuses to fight for you. It will still follow you though";
        Cooldown = 0;
        IsEquipped = false;
        Prefab = _prefab;
    }
}
