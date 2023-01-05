using UnityEngine;
using UnityEngine.AI;

public class ChickBoss : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private float maxTimer = 1.5f;
    [SerializeField] private float speed = 0.02f;
    [SerializeField] private float chargeSpeed = 10;
    [SerializeField] private float knockBackForce = 1.4f;
    [Header("Attacks")]
    [SerializeField] private float normalDamage;
    [SerializeField] private float chargeDamage;
    
    private bool _once;
    private bool _enRaged;
    private bool _canCharge;
    private bool _hasChargeDirection;
    private bool _gotHit;

    private ChickAudioManager _audioManager;
    private Animator _anim;
    private Rigidbody _rb;
    private Rigidbody _playerRb;
    private LineRenderer _lineRenderer;
    private GameObject _player;
    private PlayerRage _playerRage;
    private NavMeshAgent _navPlayer;

    [Header("State")] 
    [SerializeField] private StateEnum stateEnum;

    private StateEnum State
    {
        get => stateEnum;
        set
        {
            stateEnum = value;
            ChangeState();
        }
    }
    private enum StateEnum
    {
        Idle,
        Attack,
        Angry,
        OgreDeath
    }
    
    private void Start()
    {
        //for caching
        _player = GameObject.FindWithTag("Player");
        //turning the collider on or the boss fight
        _player.GetComponent<CapsuleCollider>().isTrigger = false;
        _navPlayer = _player.GetComponent<NavMeshAgent>();
        //ignoring player and ogre colliders so chicken can charge through them
        Physics.IgnoreCollision(_player.GetComponent<CapsuleCollider>(), GetComponent<Collider>());
        Physics.IgnoreCollision(FindObjectOfType<OgreBoss>().GetComponent<Collider>(), GetComponent<Collider>());
        _playerRage = _player.GetComponent<PlayerRage>();
        _audioManager = GetComponent<ChickAudioManager>();
        timer = maxTimer;
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _lineRenderer = GetComponent<LineRenderer>();
        _playerRb = _player.GetComponent<Rigidbody>();
        stateEnum = StateEnum.Idle;
    }
    
    private void FixedUpdate()
    {
        //to turn on the navmesh update position when player isGrounded
        if(_playerRb.velocity.y == 0)
        {
            _navPlayer.updatePosition = true;
            _playerRb.velocity = Vector3.zero;
            _gotHit = false;
        }
        ChangeState();
    }
    private void ChangeState()
    {
        switch (stateEnum)
        {
            case StateEnum.Attack:
                Attack();
                break;
            case StateEnum.Angry:
                Enraged();
                break;
            case StateEnum.OgreDeath: // should here change into idle state until player picks him up as a weapon or item.
                BackToNormal();
                break;
            case StateEnum.Idle:
            default:
                //unexpected things happened
                Application.Quit();
                break;
        }
    }
    private void Attack()
    {
        if (PlayerRage.IsDead) return;
        if (Vector3.Distance(_rb.position, _player.transform.position) < 1.5f)
        {
            //the chick is close enough to attack the player
            if(!_once)
            {
                _playerRage.TakeDamage(normalDamage,gameObject);
                _audioManager.AS_AttackChirp.Play();
                _once = true;
                //turns the navmesh off to get the physics on player
                _navPlayer.updatePosition = false;
                //resets the path to nothing
                _navPlayer.ResetPath();
                //knock backs the player when hit
                Vector3 difference = (_player.transform.position-transform.position).normalized;
                difference.y = 1f;
                Vector3 force = difference * (knockBackForce * 3);
                _playerRb.AddForce(force, ForceMode.Impulse);
            }
        }
        else
        {
            //Chicken Running at player
            _anim.SetBool("Eat", false);
            _anim.SetBool("Run", true);
            transform.LookAt(_player.transform);
            Vector3 newPos = Vector3.MoveTowards(_rb.position, _player.transform.position, speed);
            _rb.MovePosition(newPos);

            _once = false;
        }
    }
    public void StartBossFight() => stateEnum = StateEnum.Attack;

    public void StartRage()
    {
        stateEnum = StateEnum.Angry;
        _audioManager.AS_RageChirp.Play();
    }  

    public void OgreDead() => stateEnum = StateEnum.OgreDeath;

    private void Enraged()
    {
        if(!_enRaged)
        {
            _anim.SetBool("Run", false);
            _anim.Play("Jump W Root");
            //making sjicken bigger when angry
            _rb.transform.localScale += new Vector3(2.5f,2.5f,2.5f) * Time.deltaTime;
            //making the pitch lower when raging
            _audioManager.AS_RageChirp.pitch -= .08f * Time.deltaTime;
            if (_rb.transform.localScale.x > 15f) 
            {
                _enRaged = true;
                _anim.SetBool("Run", true);
            }
        }

        _lineRenderer.SetPosition(0, transform.position);
        // calling this once: LookAt, lineRenderer and startCharging resets when hitting the fence and touching the ground
        if (!_hasChargeDirection && _enRaged && _rb.velocity == Vector3.zero)
        {
            //Trying to make him not go up or down just at players position, so chick doesn't rotate upwards/downwards when targeting
            Vector3 position = new Vector3(_player.transform.position.x, 0, _player.transform.position.z);
            transform.LookAt(position);
            _hasChargeDirection = true;

           _lineRenderer.SetPosition(1, transform.forward * 25 + transform.position);

        }       
        // charge timer until he can start charging
        if (timer > 0 && _enRaged)
            timer -= Time.deltaTime;
        if (timer < 0)
            _canCharge = true;
        //Charging player
        if(_enRaged && _canCharge)
            _rb.velocity = transform.forward * chargeSpeed;

        //Hits player and knocks him upwards
        if (Vector3.Distance(_rb.position, _player.transform.position) < 1.5f)
        {
            if(!_gotHit && _rb.velocity != Vector3.zero)
            {
                _playerRage.TakeDamage(chargeDamage,gameObject);
                _audioManager.AS_AttackChirp.Play();
                //test to make chicken sound deeper when bigger
                _audioManager.AS_AttackChirp.pitch = 0.45f;
                
                Vector3 force = Vector3.up * (knockBackForce * 4f);
                _playerRb.AddForce(force, ForceMode.Impulse);

                _gotHit = true;
                
                //turns the navmesh off to get the physics on player
                _navPlayer.updatePosition = false;
                //resets the path to nothing
                _navPlayer.ResetPath();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Fence"))
        {
            //resets all
            _hasChargeDirection = false;
            timer = maxTimer;
            _canCharge = false;
            //_gotHit = false;
            //Knock back the Sjicken  when he hits a fence
            Vector3 difference = collision.transform.forward;
            difference.y = 1f;
            Vector3 force = difference * knockBackForce * 0.5f;
            _rb.AddForce(force, ForceMode.Impulse); 
        }
    }

    private void BackToNormal()
    {
        _anim.SetBool("Run", false);
        //Making sjicken small again
        if (_rb.transform.localScale.x > 2f)
            _rb.transform.localScale -= new Vector3(2.5f,2.5f,2.5f) * Time.deltaTime;

        _lineRenderer.enabled = false;
        //turn collider off we are basically only using it so player cant through the fence and to trigger boss (after player has landed then turn off)
        if(_playerRb.velocity.y == 0)
            _player.GetComponent<CapsuleCollider>().isTrigger = true;
    }
    
    public void PlayStepSound()
    {
        //using this in the event listener on the animation to play on every footstep
        _audioManager.AS_FootSteps.Play();
    }
}
