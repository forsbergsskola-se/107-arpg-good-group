using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class NPC : MonoBehaviour
{
    public bool canFlee;
    public bool canAttack;

    public float movementSpeed;
    public float attackSpeed; //This is how long the time in seconds is between attacks, not attacks per minute or some such measurement
    public float attackRange = 2; // this should be much lower than detection range, use your brain
    public float detectionRange = 20;

    public float startHealth = 100; 
    //public Item[] loot; TODO: uncomment this when items are finished
    public Transform[] goals; //This is where the target points for roaming are stored
    public GameObject playerReference;

   // private Vector3 targetPosition;
    protected Rigidbody rigidbody;
    protected Vector3 rigidbodyVelocity;
    protected Random rand = new Random();
    protected NavMeshAgent agent;
    protected float health;
    protected Vector3 startPosition;
    protected bool iFramesActive;
    protected bool attackIsOnCooldown;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        rand = new Random(System.DateTime.Today.Second); //Not strictly necessary but eh
        RandomizeValues();
        rigidbody = GetComponent<Rigidbody>();
        rigidbodyVelocity = rigidbody.velocity;
        agent = GetComponent<NavMeshAgent>();
        Roam();
        agent.speed = movementSpeed;
        agent.destination = goals[0].position;
        health = startHealth;
        startPosition = transform.position;
        iFramesActive = false;
        attackIsOnCooldown = false;

    }

    // Update is called once per frame
    void Update()
    {
        CheckForDeath();
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
        if (delta.magnitude < detectionRange)
        {
            Vector3 direction = delta.normalized; //Not sure if this is needed TBH, probably isnt
            agent.destination = transform.position + direction * 5;
        }
    }

    

    protected void Attack()
    {
        //Check if the player is within detection range, if they are, start walking towards them
        Vector3 delta = transform.position - playerReference.transform.position;
        if (delta.magnitude < detectionRange)
        {
            agent = GetComponent<NavMeshAgent>();
            agent.destination = playerReference.transform.position;
            //If the player is inside our attack range and our attack isnt on cooldown we should attack them
            if (delta.magnitude > attackRange && !attackIsOnCooldown)
            {
                //TODO: damage the player here
                attackIsOnCooldown = true;
                waitForAttackCooldown();
            }
        }
    }

    protected IEnumerable waitForAttackCooldown()
    {
        yield return new WaitForSeconds(attackSpeed);
        attackIsOnCooldown = false;
    }

    protected void Roam()
    {
        //If we dont run this line we get a crash, but I dont understand why its needed
        //Its also expensive, so we should find a way to not use it
        agent = GetComponent<NavMeshAgent>();
        if (canFlee)
        {
            //The idea here is to find a goal which is not near enough to the player to cause us to flee
            //This code is kinda ugly, we should perhaps refactor it
            List<Transform> goalsNotAtPlayer = new List<Transform>();
            for (int i = 0; i < goals.Length; i++)
            {
                Vector3 delta = playerReference.transform.position - goals[i].position;
                if (delta.magnitude > detectionRange)
                {
                    goalsNotAtPlayer.Add(goals[i]);
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
            Vector3 currentPosition = transform.position;
            currentPosition = currentPosition - agent.destination;//agegoal.position;

            if (currentPosition.magnitude < 2)
            {
                agent.destination = goals[rand.Next(0, goals.Length-1)].position;
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

    protected void DropLoot() //We need actual loot to drop before implenting this
    {
        
    }

    protected void CheckForDeath()
    {
        if (health < 0)
        {
            agent = GetComponent<NavMeshAgent>(); //This shouldnt be necessary, check tomorrow if needed
            agent.speed = 0;
            agent.velocity = Vector3.zero;
            respawnWait();
            RandomizeValues();
        }
    }

    protected IEnumerator respawnWait()
    {
        yield return new WaitForSeconds(10);
        health = startHealth;
        transform.position = startPosition;
        agent.speed = movementSpeed;
    }

    protected void Damageable(float damage)
    {
        if (!iFramesActive)
        {
            health -= damage;
            iFramesActive = true;
            waitForiFrames();
        }
        
    }
    protected IEnumerator waitForiFrames()
    {
        yield return new WaitForSeconds(0.5f);
        iFramesActive = false;
    }
}
