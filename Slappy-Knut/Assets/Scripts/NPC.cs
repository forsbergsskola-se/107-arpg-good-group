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
    public float attackSpeed;
    public float attackRange;
    
    public float startHealth = 100; 
    //public Item[] loot; TODO: uncomment this when items are finished
    public Transform[] goals;
    public Transform goal;
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
        rand = new Random(System.DateTime.Today.Second);
        RandomizeValues();
        rigidbody = GetComponent<Rigidbody>();
        rigidbodyVelocity = rigidbody.velocity;
        Roam();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;
        agent.destination = goal.position;
        health = startHealth;
        startPosition = transform.position;
        iFramesActive = false;
        attackIsOnCooldown = false;

    }

    // Update is called once per frame
    void Update()
    {
        //moveTowardsPositon();
        ChangeGoalIfFinished();
        CheckForDeath();
        Attack();
    }

    void Flee()
    {
        
    }

    protected void Roam()
    {
        goal = goals[rand.Next(0, goals.Length-1)];
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }

    protected void Attack()
    {
        if (canAttack)
        {
            //TODO: Come up with a better 
            agent = GetComponent<NavMeshAgent>();
            agent.destination = playerReference.transform.position;
            Vector3 currentPosition = transform.position;
            currentPosition = currentPosition - playerReference.transform.position;
            if (currentPosition.magnitude > attackRange && !attackIsOnCooldown)
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

    protected void ChangeGoalIfFinished()
    {
        Vector3 currentPosition = transform.position;
        currentPosition = currentPosition - goal.position;

        if (currentPosition.magnitude < 2)
        {
            Roam();
        }
    }

    protected void RandomizeValues()
    {
        //set default values
        canAttack = false;
        canFlee = true;
        
        //Randomise values
        //Random rand = new Random();
        if (rand.NextDouble() > 0.8)
        {
            canAttack = true;
        }
        if (rand.NextDouble() > 0.8)
        {
            canFlee = false;
        }
    }

    protected void DropLoot()
    {
        
    }

    protected void CheckForDeath()
    {
        if (health < 0)
        {
            agent = GetComponent<NavMeshAgent>();
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
