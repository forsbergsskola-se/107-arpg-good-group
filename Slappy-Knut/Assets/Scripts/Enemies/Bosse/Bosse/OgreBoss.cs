
using UnityEngine;
using UnityEngine.UI;

public class OgreBoss : MonoBehaviour
{
    public GameObject runawayCheckPoint;
    public Image healthBar;
    private GameObject _player;
    private OgreAudioManager _audioManager;
    private ChickBoss _chick;
    private Rigidbody _rb;
    private Animator _anim;
    
    [SerializeField] private bool _runAway;
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth = 10;
    private bool _hasRaged;

    private void Start()
    {
        _audioManager = GetComponent<OgreAudioManager>();
        _player = GameObject.FindWithTag("Player");
        _health = _maxHealth;
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _chick = FindObjectOfType<ChickBoss>();
    }
    
    private void FixedUpdate()
    {
        if (_runAway)
            Runaway();
        else
        {
            if(Health > 0)
                transform.LookAt(new Vector3(_player.transform.position.x,0,_player.transform.position.z));
        }
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
    
    public void TakeDamage(int damage) => Health -= damage; //temp placeholder TODO: implement IDamagable here

    private int Health //temp placeholder
    {
        get => _health;
        set
        {
            _health = value;
            healthBar.fillAmount = value / (float)_maxHealth;
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

    private void OnCollisionEnter(Collision collision)
    {
        //temp placeholder
        if(collision.collider.CompareTag("Player"))
            TakeDamage(1);
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
    }
    
    public void PlayStepSound()
    {
        //using this in the event listener on the animation to play on every footstep
        _audioManager.AS_FootSteps.Play();
    }
}
