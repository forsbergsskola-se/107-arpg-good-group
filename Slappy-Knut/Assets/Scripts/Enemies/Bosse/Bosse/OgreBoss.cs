
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class OgreBoss : Interactable, IDamagable
{
    public GameObject runawayCheckPoint;
    public Image healthBar;
    private GameObject _player1;
    private OgreAudioManager _audioManager;
    private ChickBoss _chick;
    private Rigidbody _rb;
    private Animator _anim;
    
    [SerializeField] private bool _runAway;
    private float _health;
    [SerializeField] private float _maxHealth = 10;
    private bool _hasRaged;

    private void Start()
    {
        _audioManager = GetComponent<OgreAudioManager>();
        _player1 = GameObject.FindWithTag("Player");
        _health = _maxHealth;
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _chick = FindObjectOfType<ChickBoss>();
        Physics.IgnoreCollision(_player1.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
    }
    
    private void FixedUpdate()
    {
        if (_runAway)
            Runaway();
        else
            if (Health > 0) FaceTarget();
    }

    private void FaceTarget()
    {
        Vector3 direction = (_player1.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    
    private void Runaway()
    {
        //Half health ogre runs to the other side of arena while chicken gets larger
        if (Vector3.Distance(_rb.position, runawayCheckPoint.transform.position) < 1f)
        {
            _runAway = false;
            _anim.SetBool("Run", false);
        }
        else
        {
            _anim.SetBool("Run", true);
            var position = runawayCheckPoint.transform.position;
            transform.LookAt(position);
            _rb.MovePosition(Vector3.MoveTowards(_rb.position, position, 0.1f));
        }
    }

    public void TakeDamage(float damage, GameObject attacker) => Health -= damage; //<--- look into attacker thing

    public float Health
    {
        get => _health;
        set
        {
            _health = value;
            healthBar.fillAmount = value / _maxHealth;
            HealthLogic();
        }
    }

    private void HealthLogic()
    {
        if(_health >= 0)
        {
            _audioManager.AS_GetHit.Play();
            _anim.Play("damage");
        }
        if (_health <= _maxHealth / 2 && !_hasRaged)
        {
            _chick.StartRage(); 
            _runAway = true; 
            _hasRaged = true;
        }
        if(_health <= 0)
            AfterDeathLogic();
    }

    private void AfterDeathLogic()
    {
        _audioManager.AS_Death.Play();
        _runAway = false;
        _anim.SetBool("Death", true);
        
        //To stop the body from interacting with the player and still stay on the field as a corpse
        Destroy(_rb.GetComponent<CapsuleCollider>());
        Destroy(_rb);
        healthBar.GetComponentInParent<Canvas>().enabled = false;
        //Call OgreDeath state in the chicken
        _chick.OgreDead();
        //Open the gate after bosse is dead
        GameObject.FindWithTag("Gate").SetActive(false);
    }
    
    public void PlayStepSound()
    {
        //using this in the event listener on the animation to play on every footstep
        _audioManager.AS_FootSteps.Play();
    }
    
    public float DefenseRating { get; set; }
    

    public void OnDeath()
    {
        throw new System.NotImplementedException();
    }
}
