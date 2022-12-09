using System;
using Unity.VisualScripting;
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
    private bool _hasChargeDirection;
    private bool _gotHit;

    private ChickAudioManager _audioManager;
    private Animator _anim;
    private Rigidbody _rb;
    private Rigidbody _playerRb;
    private LineRenderer _lineRenderer;
    private GameObject _player;

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
        _player = GameObject.FindWithTag("Player");
        _audioManager = GetComponent<ChickAudioManager>();
        timer = _maxTimer;
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _lineRenderer = GetComponent<LineRenderer>();
        _playerRb = _player.GetComponent<Rigidbody>();
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
                BackToNormal();
                break;
            default:
                //unexpected things happened
                Debug.Log("Unhandled things");
                Application.Quit();
                break;
        }
    }

    private void Attack()
    {
        if (Vector3.Distance(_rb.position, _player.transform.position) < 1.5f)
        {
            //the chick is close enough to attack the player
            //Todo: player loses health
            if(!_once)
            {
                //timer to make damage once every few msecs ? or iframes
                Debug.Log("Player took damage!");
                _audioManager.AS_AttackChirp.Play();
                _once = true;
                
                //knock backs the player when hit
                Vector3 difference = (_player.transform.position-transform.position).normalized;
                Vector3 force = difference * (knockBackForce * 5);
                _playerRb.AddForce(force, ForceMode.Impulse); 
            
                _anim.SetBool("Run", false);
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

    public void StartBossFight() => _state = State.Attack;

    public void StartRage()
    {
        //ignoring player and ogre colliders so chicken can charge through them
        Physics.IgnoreCollision(_player.GetComponent<Collider>(), GetComponent<Collider>());
        Physics.IgnoreCollision(FindObjectOfType<OgreBoss>().GetComponent<Collider>(), GetComponent<Collider>());
        _state = State.Angry;
        _audioManager.AS_RageChirp.Play();
    }  

    public void OgreDead() => _state = State.OgreDeath;

    private void Enraged()
    {
        if(!_enRaged)
        {
            _anim.SetBool("Run", false);
            _anim.Play("Jump W Root");
            //making sjicken bigger when angry
            _rb.transform.localScale += new Vector3(2.5f,2.5f,2.5f) * Time.deltaTime;
            if (_rb.transform.localScale.x > 15f) 
            {
                _enRaged = true;
                _anim.SetBool("Run", true);
            }
        }

        // calling this once: LookAt, lineRenderer and startCharging resets when hitting the fence.
        if (!_hasChargeDirection && _enRaged)
        {
            //Trying to make him not go up or down just at players position, so chick doesn't rotate upwards/downwards when targeting
            Vector3 position = new Vector3(_player.transform.position.x, 0, _player.transform.position.z);
            transform.LookAt(position);
            _hasChargeDirection = true;

            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, position);
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
            if(!_gotHit)
            {
                Debug.Log("Player got hit!");
                _audioManager.AS_AttackChirp.Play();
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
            //resets all
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

    private void BackToNormal()
    {
        _anim.SetBool("Run", false);
        //Making sjicken small again
        if(_enRaged)
            _rb.transform.localScale -= new Vector3(2.5f,2.5f,2.5f) * Time.deltaTime;
        if (_rb.transform.localScale.x < 5f)
            _enRaged = false;

        _lineRenderer.enabled = false;
    }
    
    public void PlayStepSound()
    {
        //using this in the event listener on the animation to play on every footstep
        _audioManager.AS_FootSteps.Play();
    }
}
