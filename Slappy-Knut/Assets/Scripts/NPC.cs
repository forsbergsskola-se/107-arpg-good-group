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

    public float startHealth = 100; 
    //public Item[] loot; TODO: uncomment this when items are finished
    public Transform[] goals;
    public Transform goal;
    public Transform playerReference;

   // private Vector3 targetPosition;
    private Rigidbody rigidbody;
    private Vector3 rigidbodyVelocity;
    Random rand = new Random();
    private NavMeshAgent agent;
    private float health;
    private Vector3 startPosition;
    
    
    
    
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

    }

    // Update is called once per frame
    void Update()
    {
        //moveTowardsPositon();
        ChangeGoalIfFinished();
    }

    void Flee()
    {
        
    }

    void Roam()
    {
        goal = goals[rand.Next(0, goals.Length-1)];
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }

    void Attack()
    {
        if (canAttack)
        {
            agent = GetComponent<NavMeshAgent>();
            agent.destination = playerReference.position;
        }
    }

    void ChangeGoalIfFinished()
    {
        Vector3 currentPosition = transform.position;
        currentPosition = currentPosition - goal.position;

        if (currentPosition.magnitude < 2)
        {
            Roam();
        }
    }

    void RandomizeValues()
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

    void DropLoot()
    {
        
    }

    void CheckForDeath()
    {
        if (health > 0)
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = 0;
            agent.velocity = Vector3.zero;
            respawnWait();
            RandomizeValues();
        }
    }

    IEnumerator respawnWait()
    {
        yield return new WaitForSeconds(10);
        health = startHealth;
        transform.position = startPosition;
        agent.speed = movementSpeed;
    }

    void Damageable(float damage)
    {
        health -= damage;
    }
}
