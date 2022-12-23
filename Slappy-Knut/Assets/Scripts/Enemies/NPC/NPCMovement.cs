using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;
public class NPCMovement : MonoBehaviour
{
    public bool canFlee;
    public bool canAttack;
    public float detectionRange = 6;
    public float attackSpeed = 2; //This is how long the time in seconds is between attacks, not attacks per minute or other some such measurement
    public float attackRange = 4; // this should be much lower than detection range, use your brain
    public float movementSpeed;
    public Transform[] waypoints; //This is where the target points for roaming are stored
    public int idleTime = 5;
    
    protected NavMeshAgent agent;
    protected GameObject playerReference;
    protected Random rand = new Random();
    protected bool attackIsOnCooldown;
    protected Vector3 startPosition;
    protected bool fledLastFrame, fleeingCooldownInProgress;
    protected NPCAudioManager _audioManager;
    protected bool ideling;
    protected float walkSpeed = 2;
    
    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        playerReference = GameObject.FindWithTag("Player");
        ideling = false;
        RandomizeValues();
        Roam();
        agent.speed = movementSpeed;
        agent.destination = waypoints[0].position;
        rand = new Random();
        attackIsOnCooldown = false;
        startPosition = transform.position;
        _audioManager = GetComponent<NPCAudioManager>();
        
        GameObject player = GameObject.FindWithTag("Player");
        NavMeshAgent playerAgent = player.GetComponent<NavMeshAgent>();
        //Make sure the player can always reach the NPC
        //So if the NPC is too fast, set their max speed to 90% of the players speed
        if (movementSpeed > playerAgent.speed * 0.9)
        {
            movementSpeed = (float)(playerAgent.speed * 0.9);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        agent.speed = walkSpeed;
        if (agent.velocity.magnitude > 0.5f)
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

        if (canFlee && GoalDelta.magnitude < 2 && !ideling)
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
                agent.isStopped = true;
                StartCoroutine(IdleWait());
                
            }
            else
            {
                agent.destination = transform.position;
            }
        }
        else if(!ideling)
        { //If we cant flee there is no reason to do such a check

            if (GoalDelta.magnitude < 2)
            {
                agent.destination = waypoints[rand.Next(0, waypoints.Length-1)].position;
                agent.isStopped = true;
                StartCoroutine(IdleWait());
            }
        }
    }

    IEnumerator IdleWait()
    {
        yield return new WaitForSeconds(rand.Next(System.Convert.ToInt32(idleTime-(idleTime*0.5)), idleTime));
        agent.isStopped = false;
        ideling = false;
    }
   
    void Flee()
    {
        
        //Gets a vector of the distance between the player and NPC, pointing away from the player towards the NPC
        Vector3 delta = transform.position - playerReference.transform.position;
        if (fledLastFrame && delta.magnitude > detectionRange)
        {
            
            //agent.speed = movementSpeed;
            fledLastFrame = false;
            fleeingCooldownInProgress = true;
            StartCoroutine(waitForFleeCooldown());
        }
        
        if (delta.magnitude < detectionRange || fleeingCooldownInProgress)
        {
            agent.isStopped = false;
            //agent.speed = movementSpeed;
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
            agent.speed = movementSpeed;
            
            if (delta.magnitude < attackRange)
            {
                agent.isStopped = true; //We always want the NPC to be stopped when within range, even if its waiting on its cooldown
                if(!attackIsOnCooldown){
                    //If the player is inside our attack range and our attack isnt on cooldown we should attack them
                    _animator.SetTrigger("Attack"); //Triggers the attack animation, this should have priority over all other animations
                    attackIsOnCooldown = true;
                    _audioManager.AS_Swing.Play();
                    StartCoroutine(waitForAttackCooldown());
                
                }
            }
            else
            {
                agent.isStopped = false;
                agent.destination = playerReference.transform.position;
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
        //Here we play footstep sounds from walking
        //This is triggered by the animation itself
        // if (_animator.GetBool("Running") == false)
        // {
        //     _audioManager.AS_FootSteps.volume = 0.5f;
        // }
        // else
        // {
        //     _audioManager.AS_FootSteps.volume = 1f;
        // }
        _audioManager?.AS_FootSteps.Play();
    }

    public void DealDamage()
    {
        //This is triggered by the attack animation, which is triggered by Attack();
        //This code has two problems
        //1: it hits equally far in all directions, not just in front on the NPC
        //2: it can hit all damagable objects, inluding other NPCs
        bool playAttackSound = false;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider is IDamagable)
            {
                var test = hitCollider as IDamagable;
                test.TakeDamage(50, this.gameObject);
                playAttackSound = true;
            }
        }

        if (playAttackSound)
        {
            _audioManager.AS_Hit.Play();
        }
    }
}
