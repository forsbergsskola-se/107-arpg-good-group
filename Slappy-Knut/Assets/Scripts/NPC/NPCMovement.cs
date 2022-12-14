using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;
public class NPCMovement : MonoBehaviour
{
    public bool canFlee;
    public bool canAttack;
    
    protected NavMeshAgent agent;
    public float movementSpeed;
    public Transform[] waypoints; //This is where the target points for roaming are stored
    protected GameObject playerReference;
    protected Random rand = new Random();
    public float detectionRange = 20;
    public float attackSpeed; //This is how long the time in seconds is between attacks, not attacks per minute or some such measurement
    public float attackRange = 2; // this should be much lower than detection range, use your brain
    protected bool attackIsOnCooldown;
    private Animator _animator;
    
    protected Vector3 startPosition;
    
    
    protected bool fledLastFrame, fleeingCooldownInProgress;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        playerReference = GameObject.FindWithTag("Player");
        RandomizeValues();
        Roam();
        agent.speed = movementSpeed;
        agent.destination = waypoints[0].position;
        rand = new Random(System.DateTime.Today.Second); //Not strictly necessary but eh
        attackIsOnCooldown = false;
        startPosition = transform.position;
        _animator.SetTrigger("Run");
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
    protected void Roam()
    {
        Vector3 GoalDelta = transform.position - agent.destination;
        
        _animator.speed = 0.75f;
        agent = GetComponent<NavMeshAgent>();
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
    IEnumerator waitForFleeCooldown()
    {
        yield return new WaitForSeconds(5);
        fleeingCooldownInProgress = false;
    }
    

    

    protected IEnumerator waitForAttackCooldown()
    {
        yield return new WaitForSeconds(attackSpeed);
        attackIsOnCooldown = false;
    }

    public void setAgentSpeed(bool setZero)//this is used by NPCHealth
    {
        if (setZero)
        {
            agent.speed = 0;
        }
        else
        {
            agent.speed = movementSpeed;
            transform.position = startPosition;
        }
    }

    public void PlayStepSound()
    {
        
    }
}
