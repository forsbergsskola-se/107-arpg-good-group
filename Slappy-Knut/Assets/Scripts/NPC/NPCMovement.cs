using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;
public class NPCMovement : MonoBehaviour
{
    public bool canFlee;
    public bool canAttack;
    public float detectionRange = 20;
    public float attackSpeed; //This is how long the time in seconds is between attacks, not attacks per minute or some such measurement
    public float attackRange = 2; // this should be much lower than detection range, use your brain
    public float movementSpeed;
    public Transform[] waypoints; //This is where the target points for roaming are stored
    
    protected NavMeshAgent agent;
    protected GameObject playerReference;
    protected Random rand = new Random();
    protected bool attackIsOnCooldown;
    protected Vector3 startPosition;
    protected bool fledLastFrame, fleeingCooldownInProgress;
    protected NPCAudioManager _audioManager;
    
    private Animator _animator;
    
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
        _audioManager = GetComponent<NPCAudioManager>();

        // _animator.SetTrigger("Run");
    }

    // Update is called once per frame
    void Update()
    {
        canFlee = true; //THIS NEEDS TO BE DELETED WHEN DEBUGGING ENDS
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
        agent = GetComponent<NavMeshAgent>();//This shouldnt be needed, it wasnt needed, now its needed again. wtf?
        Vector3 GoalDelta = transform.position - agent.destination;

        if (agent.velocity.magnitude > 0.1)
        {
            _animator.SetBool("Walking", true);
            _animator.speed = 1f;
        }
        else
        {
            _animator.SetBool("Walking", false);
            _animator.speed = 1f;
        }

        if (agent.velocity.magnitude > 3)
        {
            _animator.SetBool("Running", true);
            _animator.speed = 0.75f;
        }
        else
        {
            _animator.SetBool("Running", false);
            _animator.speed = 1;
        }

        if (canFlee && GoalDelta.magnitude < 2)
        {
            //The idea here is to find a goal which is not near enough to the player to cause us to flee
            //This code is kinda ugly, but this is as good as its going to get
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

    public void ToggleAgentSpeed(bool setZero)//this is used by NPCHealt
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
        if (_animator.GetBool("Running") == false)
        {
            _audioManager.AS_FootSteps.volume = 0.5f;
        }
        else
        {
            _audioManager.AS_FootSteps.volume = 1f;
        }
        _audioManager.AS_FootSteps.Play();
    }
}
