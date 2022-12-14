using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class NPC : MonoBehaviour, IDamagable
{
    public bool canFlee;
    public bool canAttack;

    public float movementSpeed;
    public float attackSpeed; //This is how long the time in seconds is between attacks, not attacks per minute or some such measurement
    public float attackRange = 2; // this should be much lower than detection range, use your brain
    public float detectionRange = 20;

    public float startHealth = 100; 
    //public Item[] loot; TODO: uncomment this when items are finished
    public Transform[] waypoints; //This is where the target points for roaming are stored
    

    protected Random rand = new Random();
    protected NavMeshAgent agent;
    protected float health;
    protected Vector3 startPosition;
    protected bool iFramesActive;
    protected bool attackIsOnCooldown;
    protected bool fledLastFrame, fleeingCooldownInProgress;
    protected GameObject playerReference;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerReference = GameObject.FindWithTag("Player");
        rand = new Random(System.DateTime.Today.Second); //Not strictly necessary but eh
        RandomizeValues();
        agent = GetComponent<NavMeshAgent>();
        Roam();
        agent.speed = movementSpeed;
        agent.destination = waypoints[0].position;
        health = startHealth;
        startPosition = transform.position;
        iFramesActive = false;
        attackIsOnCooldown = false;

    }

    // Update is called once per frame
    void Update()
    {
        canFlee = true;
        
        
        // The order of the calls below is important, it essentially gives the NPC priorities, the later a function is
        // called the higher the priority is
        Roam();
        if (canFlee)
        {
            Flee();
        }
        if (canAttack)
        {
            Attack();
        }
    }

    void Flee()
    {
        
        //Gets a vector of the distance between the player and NPC, pointing away from the player towards the NPC
        Vector3 delta = transform.position - playerReference.transform.position;
        
        if (fledLastFrame && delta.magnitude > detectionRange)
        {
            fledLastFrame = false;
            fleeingCooldownInProgress = true;
            StartCoroutine(waitForFleeCooldown());
        }
        
        if (delta.magnitude < detectionRange || fleeingCooldownInProgress)
        {
            Vector3 direction = delta.normalized; //Not sure if this is needed TBH, probably isnt
            //Making this point be further away from the NPCs current location will likely make it
            //better at navigating around obstacles
            agent.destination = transform.position + direction * 5;
            if (delta.magnitude < detectionRange)
            {
                fledLastFrame = true;
            }
        }

        
    }

    IEnumerator waitForFleeCooldown()
    {
        yield return new WaitForSeconds(5);
        fleeingCooldownInProgress = false;
    }
    

    protected void Attack()
    {
        //Check if the player is within detection range, if they are, start walking towards them
        Vector3 delta = transform.position - playerReference.transform.position;
        if (delta.magnitude < detectionRange)
        {
            agent.destination = playerReference.transform.position;
            //If the player is inside our attack range and our attack isnt on cooldown we should attack them
            if (delta.magnitude > attackRange && !attackIsOnCooldown)
            {
                //TODO: damage the player here
                attackIsOnCooldown = true;
                StartCoroutine(waitForAttackCooldown());
            }
        }
    }

    protected IEnumerator waitForAttackCooldown()
    {
        yield return new WaitForSeconds(attackSpeed);
        attackIsOnCooldown = false;
    }

    protected void Roam()
    {
        Vector3 GoalDelta = transform.position - agent.destination;
        if (canFlee && GoalDelta.magnitude < 2)
        {
            //The idea here is to find a goal which is not near enough to the player to cause us to flee
            //This code is kinda ugly, we should perhaps refactor it
            List<Transform> goalsNotAtPlayer = new List<Transform>();
            for (int i = 0; i < waypoints.Length; i++)
            {
                Vector3 delta = playerReference.transform.position - waypoints[i].position;
                if (delta.magnitude > detectionRange)
                {
                    goalsNotAtPlayer.Add(waypoints[i]);
                }
            }

            if (goalsNotAtPlayer.Count > 0)
            {
                agent.destination = goalsNotAtPlayer[rand.Next(0, goalsNotAtPlayer.Count)].position;
            }
            else
            {
                agent.destination = transform.position;
            }
        }
        else
        { //If we cant flee there is no reason to do such a check

            if (GoalDelta.magnitude < 2)
            {
                agent.destination = waypoints[rand.Next(0, waypoints.Length-1)].position;
            }
        }
    }

    protected void RandomizeValues()
    {
        //set default values
        canAttack = false;
        canFlee = true;
        
        //Randomise values
        if (rand.NextDouble() > 0.8)//TODO: tweak odds
        {
            canAttack = true;
        }
        if (rand.NextDouble() > 0.8)
        {
            canFlee = false;
        }
    }

    protected void DropLoot() //We need actual loot to drop before implementing this
    {
        
    }

    protected void CheckForDeath()
    {
        if (health < 0)
        {
            agent.speed = 0;
            agent.velocity = Vector3.zero;
            GetComponent<MeshRenderer>().enabled = false;
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
        transform.position = startPosition;
        agent.speed = movementSpeed;
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
        CheckForDeath();
    }
    
    public void OnDeath()//
    {
        throw new System.NotImplementedException();
    }
}
