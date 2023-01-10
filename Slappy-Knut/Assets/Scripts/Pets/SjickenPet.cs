using UnityEngine;
using UnityEngine.AI;

public class SjickenPet : Pet
{
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


    protected override void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
        Agent = GetComponent<NavMeshAgent>();
    }
}
