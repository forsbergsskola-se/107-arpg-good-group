using Interfaces;
using UnityEngine;
using UnityEngine.AI;

public abstract class Pet : Interactable, IItem
{
    public string Type => "Pet";
    public abstract string Name { get; set; }
    public abstract Sprite Icon { get; set; }
    public abstract float Power { get; set; }
    public abstract float Range { get; set; }
    public abstract string Description { get; set; }
    public abstract float Cooldown { get; set; }
    public abstract GameObject Prefab { get; set; }
    public abstract bool IsEquipped { get; set; }
    public abstract GameObject Player { get; set; }
    public abstract Rigidbody Rb { get; set; }
    public abstract Animator Anim { get; set; }
    public abstract NavMeshAgent Agent { get; set; }


    public static Pet CurrEquippedPet;
    public bool movementDisabled;

    

    protected abstract void Start();
    
    public static void SpawnPet(GameObject prefab)
    {
        GameObject go = Instantiate(prefab, FindObjectOfType<PlayerAttack>().transform.position + new Vector3(0, 0, -1.5f), Quaternion.identity);
        CurrEquippedPet = go.GetComponent<Pet>();
    }

    public static void Unequip()
    {
        Destroy(CurrEquippedPet.gameObject);
        CurrEquippedPet = null;
    }
    
    void MoveToPlayer()
    {
        if (Vector3.Distance( transform.position, Player.transform.position) < 4f)
        {
            //stop the chicken 4f away from player else follow him
            Anim.SetBool("Run", false);
            //_anim.SetBool("Eat", true); // dno if we want eat idle
            Agent.ResetPath();
        }
        else
        {
            Anim.SetBool("Run", true);
            Agent.SetDestination(Player.transform.position);
        }
        
    }
    void LateUpdate()
    {
        if (!movementDisabled) MoveToPlayer();
    }
}
