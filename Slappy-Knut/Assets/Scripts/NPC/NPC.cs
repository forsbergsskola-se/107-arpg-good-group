using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class NPC : MonoBehaviour, IDamagable
{
    public float startHealth = 100; 
    //public Item[] loot; TODO: uncomment this when items are finished
    
    protected Random rand = new Random();
    protected float health;
    protected bool iFramesActive;
    private NPCAudioManager _audioManager;
   
    
    // Start is called before the first frame update
    void Start()
    {
        rand = new Random(System.DateTime.Today.Second); //Not strictly necessary but eh
        RandomizeValues();
        
        health = startHealth;
        _audioManager = GetComponent<NPCAudioManager>();
        iFramesActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void RandomizeValues()//This is where we want to generate loot later, dont remove this function
    {
        
    }

    protected void DropLoot() //We need actual loot to drop before implementing this
    {
        
    }

    protected void CheckForDeath()
    {
        if (health < 0)
        {
            GetComponent<NPCMovement>().ToggleAgentSpeed(true);
            foreach (var mesh in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                mesh.enabled = false;
            }
            GetComponent<BoxCollider>().enabled = false;
            //The NPC is reset in the respawnWait coroutine
            var tst = StartCoroutine(respawnWait());
            RandomizeValues();
        }
    }

    protected IEnumerator respawnWait()
    {
        yield return new WaitForSeconds(10);
        health = startHealth;
        GetComponent<NPCMovement>().ToggleAgentSpeed(false);
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
    }
    
    protected IEnumerator waitForiFrames()
    {
        yield return new WaitForSeconds(0.5f);
        iFramesActive = false;
    }

    public float DefenseRating { get; set; }
    
    public void TakeDamage(float damage, GameObject attacker)
    {
        health -= damage;
        Debug.Log("Took Damage!\n New health is: " + health.ToString());
        _audioManager.AS_Damage.Play();
        CheckForDeath();
    }
    
    public void OnDeath()//I dont know what to do with this function tbh
    {
        throw new System.NotImplementedException();
    }
}
