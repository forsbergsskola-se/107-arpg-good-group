using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class NPC : Interactable, IDamagable
{
    public float startHealth = 100; 
    public List<GameObject> loot;
    public int[] percentageTable; //this has to have same amount of fields as loot
    
    protected Random Rand = new();
    protected bool iFramesActive;
    private NPCAudioManager _audioManager;
    private Animator _animator;
    private int total = 100;
    private int randomNumber;
    void Start()
    {
        _animator = GetComponent<Animator>();
        Rand = new Random(System.DateTime.Today.Second); //Not strictly necessary but eh
        
        
        Health = startHealth;
        _audioManager = GetComponent<NPCAudioManager>();
        iFramesActive = false;
    }

    protected void DropLoot()
    {
        randomNumber = new Random().Next(0,total);
        for (int i = 0; i < percentageTable.Length; i++)
        {
            if (randomNumber <= percentageTable[i])
            {
                GameObject item = Instantiate(loot[i], transform.position, Quaternion.identity);
                var light = item.GetComponentInChildren<Light>();
                if(light) light.enabled = true;
                return;
            }
            randomNumber -= percentageTable[i];
        }
    }

    protected IEnumerator WaitForiFrames()
    {
        yield return new WaitForSeconds(0.5f);
        iFramesActive = false;
    }

    public float Health { get; set; }
    public float DefenseRating { get; set; }
    
    public void TakeDamage(float damage, GameObject attacker)
    {
        GetComponent<NPCMovement>().isDamaged = true;
        Health -= damage;
        _audioManager.AS_Damage.Play();
        if (Health < 1)
        {
            OnDeath();
        }
    }
    
    public void OnDeath()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().ResetPath();
        GetComponent<NPCMovement>().enabled = false;
        _animator.Play("Death");
        DropLoot();
        Spawner.CurrentNpcCount--;
        Invoke("Destroy", 3);
    }

    private void Destroy() => Destroy(gameObject);
}
