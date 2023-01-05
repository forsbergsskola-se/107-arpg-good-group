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
    public GameObject waypointsParent;
    public int idleTime = 5;
    public bool isDamaged;
    
    protected GameObject[] Waypoints; //This is where the target points for roaming are stored
    protected NavMeshAgent Agent;
    protected GameObject PlayerReference;
    protected Random Rand = new();
    protected bool AttackIsOnCooldown;
    protected Vector3 StartPosition;
    protected bool FledLastFrame, FleeingCooldownInProgress;
    protected NPCAudioManager AudioManager;
    protected bool Idling;
    protected float WalkSpeed = 2;
    
    private Animator _animator;
    private NPC npc;
    
    void Start()
    {
        npc = GetComponent<NPC>();
        Waypoints = GameObject.FindGameObjectsWithTag("NPCWayPoint");
        Agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        PlayerReference = GameObject.FindWithTag("Player");
        Idling = false;
        RandomizeValues();
        Roam();
        Agent.speed = movementSpeed;
        Rand = new Random();
        AttackIsOnCooldown = false;
        StartPosition = transform.position;
        AudioManager = GetComponent<NPCAudioManager>();
        Agent.destination = Waypoints[0].transform.position;
        
        GameObject player = GameObject.FindWithTag("Player");
        NavMeshAgent playerAgent = player.GetComponent<NavMeshAgent>();
        //Make sure the player can always reach the NPC
        //So if the NPC is too fast, set their max speed to 90% of the players speed
        if (movementSpeed > playerAgent.speed * 0.9)
        {
            movementSpeed = (float)(playerAgent.speed * 0.9);
        }
    }

    void Update()
    {
        if (npc.Health <= 0) return;
        Roam();
        if (isDamaged)
        {
            if (canFlee) Flee();
            if (canAttack) Attack();    
        }
    }
    
    protected void RandomizeValues()
    {
        //set default values
        canAttack = false;
        canFlee = true;
        
        //Randomise values
        if (Rand.NextDouble() > 0.8) //TODO: tweak odds
        {
            canAttack = true;
            canFlee = false;
        }
    }
    
    protected void Roam()
    {
        Vector3 GoalDelta = transform.position - Agent.destination;
        Agent.speed = WalkSpeed;
        if (Agent.velocity.magnitude < 0.1f)
        {
            _animator.SetBool("Walking", false);
            _animator.SetBool("Running", false);
        }
        else if (Agent.velocity.magnitude is > 0.5f and < 2.5f)
        {
            _animator.SetBool("Walking", true);
            _animator.SetBool("Running", false);

        }
        else if (Agent.velocity.magnitude > 3)
        {
            _animator.SetBool("Walking", false);
            _animator.SetBool("Running", true);
        }

        if (isDamaged && canFlee && GoalDelta.magnitude < 2 && !Idling)
        {
            //The idea here is to find a goal which is not near enough to the player to cause us to flee
            //This code is kinda ugly, but this is as good as its going to get
            List<Transform> goalsNotAtPlayer = new List<Transform>();
            for (int i = 0; i < Waypoints.Length; i++)
            {
                Vector3 delta = PlayerReference.transform.position - Waypoints[i].transform.position;
                if (delta.magnitude > detectionRange)
                {
                    goalsNotAtPlayer.Add(Waypoints[i].transform);
                }
            }

            if (goalsNotAtPlayer.Count > 0)
            {
                Agent.destination = goalsNotAtPlayer[Rand.Next(0, goalsNotAtPlayer.Count)].position;
                Agent.isStopped = true;
                StartCoroutine(IdleWait());
                
            }
            else
            {
                Agent.destination = transform.position;
            }
        }
        else if(!Idling)
        { //If we cant flee there is no reason to do such a check

            if (GoalDelta.magnitude < 2)
            {
                Agent.destination = Waypoints[Rand.Next(0, Waypoints.Length - 1)].transform.position;
                Agent.isStopped = true;
                StartCoroutine(IdleWait());
            }
        }
    }

    IEnumerator IdleWait()
    {
        yield return new WaitForSeconds(Rand.Next(System.Convert.ToInt32(idleTime*0.5), idleTime));
        Agent.isStopped = false;
        Idling = false;
    }
   
    void Flee()
    {
        
        //Gets a vector of the distance between the player and NPC, pointing away from the player towards the NPC
        Vector3 delta = transform.position - PlayerReference.transform.position;
        if (FledLastFrame && delta.magnitude > detectionRange)
        {
            
            //agent.speed = movementSpeed;
            FledLastFrame = false;
            FleeingCooldownInProgress = true;
            StartCoroutine(waitForFleeCooldown());
        }
        
        if (delta.magnitude < detectionRange || FleeingCooldownInProgress)
        {
            Agent.isStopped = false;
            Agent.speed *= 1.5f;
            Vector3 direction = delta.normalized; //Not sure if this is needed TBH, probably isnt
            //Making this point be further away from the NPCs current location will likely make it
            //better at navigating around obstacles
            Agent.destination = transform.position + direction * 5;
            if (delta.magnitude < detectionRange)
            {
                FledLastFrame = true;
            }
        }
    }
    
    protected void Attack()
    {
        //Check if the player is within detection range, if they are, start walking towards them
        Vector3 delta = transform.position - PlayerReference.transform.position;
        
        if (delta.magnitude < detectionRange)
        {
            Agent.speed = movementSpeed;
            
            if (delta.magnitude < attackRange)
            {
                Agent.isStopped = true; //We always want the NPC to be stopped when within range, even if its waiting on its cooldown
                if(!AttackIsOnCooldown){
                    transform.LookAt(PlayerReference.transform);
                    //If the player is inside our attack range and our attack isnt on cooldown we should attack them
                    _animator.SetTrigger("Attack"); //Triggers the attack animation, this should have priority over all other animations
                    AttackIsOnCooldown = true;
                    
                    StartCoroutine(WaitForAttackCooldown());
                }
            }
            else
            {
                Agent.isStopped = false;
                Agent.ResetPath();
            }
            
        }
    }
    
    IEnumerator waitForFleeCooldown()
    {
        yield return new WaitForSeconds(5);
        FleeingCooldownInProgress = false;
    }
    
    protected IEnumerator WaitForAttackCooldown()
    {
        yield return new WaitForSeconds(attackSpeed);
        AttackIsOnCooldown = false;
    }
    
    public void ToggleAgentSpeed(bool setZero)//this is used by NPCHealt
    {
        if (setZero)
        {
            Agent.speed = 0;
        }
        else
        {
            Agent.speed = movementSpeed;
            transform.position = StartPosition;
        }
    }
    
    public void PlayStepSound()
    {
        //Here we play footstep sounds from walking
        //This is triggered by the animation itself
        AudioManager?.AS_FootSteps.Play();
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
            var damagable = hitCollider.GetComponent<IDamagable>();
            if (damagable != null && hitCollider.gameObject.CompareTag("Player"))
            {
                damagable.TakeDamage(.09f, gameObject);
                playAttackSound = true;
            }
        }

        if (playAttackSound)
        {
            AudioManager.AS_Hit.Play();
        }
    }
    
}