using System;
using UnityEngine;

public class ChickBoss : MonoBehaviour
{
    [SerializeField] 
    private float timer;
    private readonly float _maxTimer = 3f;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float chargeSpeed;
    [SerializeField]
    private float knockBackForce;
    private bool _once;
    private bool _enRaged;
    private bool _canCharge;
    [SerializeField]
    private bool _hasChargeDirection;

    private Animator _anim;
    private Rigidbody _rb;
    private Rigidbody _playerRb;
    private LineRenderer lineRenderer;
    public GameObject player;

    [Header("State")]
    [SerializeField]
    private State _state;
    private enum State
    {
        Idle,
        Attack,
        Angry,
        OgreDeath
    }

    
    private void Start()
    {
        timer = _maxTimer;
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        _playerRb = player.GetComponent<Rigidbody>();
        _state = State.Idle;
    }
    
    private void Update()
    {
        switch (_state)
        {
            default:
            case State.Idle:
                _anim.SetBool("Eat", true);
                break;
            case State.Attack:
                Attack();
                break;
            case State.Angry:
                EnRaged();
                break;
            case State.OgreDeath: // should here change into idle state until player picks him up as a weapon or item.
                break;
        }
    }

    private void Attack()
    {
        if (Vector3.Distance(_rb.position, player.transform.position) < 1.5f)
        {
            //here the chick is close enough to attack the player
            //Todo: player loses health
            if(!_once)
            {
                //timer to make damage once every few msecs ? or iframes
                Debug.Log("Player took damage!");
                _once = true;
            }
            
            //knock backs the player when hit
            Vector3 difference = (player.transform.position-transform.position).normalized;
            Vector3 force = difference * knockBackForce;
            _playerRb.AddForce(force, ForceMode.Impulse); 
            
            _anim.SetBool("Run", false);
        }
        else
        {
            _anim.SetBool("Eat", false);
            _anim.SetBool("Run", true);
            transform.LookAt(player.transform);
            Vector3 newPos = Vector3.MoveTowards(_rb.position, player.transform.position, speed);
            _rb.MovePosition(newPos);

            _once = false;
        }
    }

    public void StartBossFight()
    {
        _state = State.Attack;
    }

    public void StartRage()
    {
        _state = State.Angry;
    }

    public void OgreDead()
    {
        _state = State.OgreDeath;
    }

    private void EnRaged()
    {
       
        if(!_enRaged)
        {
            _anim.SetBool("Run", false);
            _anim.Play("Jump W Root");
            _rb.transform.localScale += new Vector3(2,2,2) * Time.deltaTime;
            if (_rb.transform.localScale.x > 15f) 
            {
                _enRaged = true;
                _anim.SetBool("Run", true);
            }
        }
        
        //Todo: Make A lineRenderer that shows the direction the chicken is going to charge attack to for a sec then he zooms
        //Todo: maybe make the lines somehow big and getting shorter while he is charging until he does it.
        
        //Lookat once and start charging resets when hitting the fence.
        if (!_hasChargeDirection && _enRaged)
        {
            //Trying to make him not go up or down just at players position, so chick doesn't rotate upwards
            Vector3 test = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            transform.LookAt(test);
            _hasChargeDirection = true;
            
            //line
            //lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, player.transform.position); // * chargeSpeed);
        }
        // charge timer until he can charge
        if (timer > 0 && _enRaged)
            timer -= Time.deltaTime;
        if (timer < 0)
            _canCharge = true;

        if(_enRaged && _canCharge)
        {
            _rb.velocity = transform.forward * chargeSpeed;
/*
           // if (Vector3.Distance(_rb.position, player.transform.position) < 1.5f)
           // {
                Vector3 force = Vector3.up * knockBackForce;
                _playerRb.AddForce(force, ForceMode.Impulse);
          //  }*/
        }
        //Todo: direction until he hits something and repeats(so the player can dodge out the way). 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") )
        {
            Debug.Log("Player got hit!");

            Vector3 force = Vector3.up * knockBackForce * 5f;
            _playerRb.AddForce(force, ForceMode.Impulse);
        }

        if(collision.collider.CompareTag("Ogre"))
            Physics.IgnoreCollision(collision.collider.GetComponent<Collider>(), GetComponent<Collider>());
        
        if (collision.collider.CompareTag("Fence")) 
        {
            _hasChargeDirection = false;
            timer = _maxTimer;
            _canCharge = false;
        }
    }
}
