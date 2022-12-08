using System;
using UnityEngine;

public class ChickBoss : MonoBehaviour
{
    [SerializeField] 
    private float timer;
    private readonly float _maxTimer = 2f;
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

    private bool _gotHit;

    private Animator _anim;
    private Rigidbody _rb;
    private Rigidbody _playerRb;
    private LineRenderer _lineRenderer;
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
        _lineRenderer = GetComponent<LineRenderer>();
        _playerRb = player.GetComponent<Rigidbody>();
        _state = State.Idle;
    }
    
    private void Update()
    {
        switch (_state)
        {
            case State.Idle:
                _anim.SetBool("Eat", true);
                break;
            case State.Attack:
                Attack();
                break;
            case State.Angry:
                Enraged();
                break;
            case State.OgreDeath: // should here change into idle state until player picks him up as a weapon or item.
                break;
            default:
                Application.Quit();
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

    public void StartBossFight() => _state = State.Attack;

    public void StartRage()
    {
        //ignoring player and ogre colliders so chicken can charge through them
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
        Physics.IgnoreCollision(FindObjectOfType<OgreBoss>().GetComponent<Collider>(), GetComponent<Collider>());
        _state = State.Angry;
    }  

    public void OgreDead() => _state = State.OgreDeath;

    private void Enraged()
    {
        if(!_enRaged)
        {
            _anim.SetBool("Run", false);
            _anim.Play("Jump W Root");
            //making sjicken bigger when angry
            _rb.transform.localScale += new Vector3(2,2,2) * Time.deltaTime;
            if (_rb.transform.localScale.x > 15f) 
            {
                _enRaged = true;
                _anim.SetBool("Run", true);
            }
        }

        //LookAt once and start charging resets when hitting the fence.
        if (!_hasChargeDirection && _enRaged)
        {
            //Trying to make him not go up or down just at players position, so chick doesn't rotate upwards
            Vector3 position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            transform.LookAt(position);
            _hasChargeDirection = true;

            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, position);
        }       
        // charge timer until he can charge
        if (timer > 0 && _enRaged)
            timer -= Time.deltaTime;
        if (timer < 0)
            _canCharge = true;

        if(_enRaged && _canCharge)
            _rb.velocity = transform.forward * chargeSpeed;


        if (Vector3.Distance(_rb.position, player.transform.position) < 1.5f)
        {
            if(!_gotHit)
            {
                Debug.Log("Player got hit!");
                Vector3 force = Vector3.up * (knockBackForce * 6f);
                _playerRb.AddForce(force, ForceMode.Impulse);
                _gotHit = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Fence"))
        {
            _hasChargeDirection = false;
            timer = _maxTimer;
            _canCharge = false;
            _gotHit = false;
            
            //Knock back the Sjicken  when he hits a fence
            Vector3 difference = -(transform.forward);
            difference.y = 1f;
            Vector3 force = difference * knockBackForce * 0.5f;
            _rb.AddForce(force, ForceMode.Impulse); 
        }
    }
}
