using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class NPC : Interactable, IDamagable
{
    public float startHealth = 100; 
    public float health;
    //public Item[] loot; TODO: uncomment this when items are finished
    
    protected Random Rand = new();
    protected bool iFramesActive;
    private NPCAudioManager _audioManager;
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        Rand = new Random(System.DateTime.Today.Second); //Not strictly necessary but eh
        RandomizeValues();
        
        health = startHealth;
        _audioManager = GetComponent<NPCAudioManager>();
        iFramesActive = false;
    }

    protected void RandomizeValues()//This is where we want to generate loot later, dont remove this function
    {
        
    }

    protected void DropLoot() //We need actual loot to drop before implementing this
    {
        
    }

    protected IEnumerator WaitForiFrames()
    {
        yield return new WaitForSeconds(0.5f);
        iFramesActive = false;
    }

    public float DefenseRating { get; set; }
    
    public void TakeDamage(float damage, GameObject attacker)
    {
        health -= damage;
        _audioManager.AS_Damage.Play();
        if (health < 1)
        {
            OnDeath();
        }
    }
    
    public void OnDeath()
    {
        GetComponent<NavMeshAgent>().destination = transform.position;
        GetComponent<Collider>().enabled = false;
        GetComponent<NPCMovement>().enabled = false;
        _animator.Play("Death");
        Invoke("Destroy", 3);
    }

    private void Destroy() => Destroy(gameObject);
}
