
using UnityEngine;
using UnityEngine.UI;

public class OgreBoss : MonoBehaviour
{
    public GameObject runawayCheckPoint;
    public Image healthBar;

    private ChickBoss _chick;
    private Rigidbody _rb;
    private Animator _anim;
    [SerializeField]
    private bool _runAway;
    [SerializeField]
    private int _health;
    private int _maxHealth = 10;

    void Start()
    {
        _health = _maxHealth;
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _chick = FindObjectOfType<ChickBoss>();
        //_runAway = true;
    }
    
    void Update()
    {
        if (_runAway)
            Runaway();
        else
        {
            if(Health > 0)
                transform.LookAt(_chick.player.transform.position);
            //_anim.SetBool("Idle", true); //Mayb dont need if he autos at idle
        }
    }

    void Runaway()
    {
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
            _rb.MovePosition(Vector3.MoveTowards(_rb.position, position, 0.02f));
        }
    }
    
    public void TakeDamage(int damage) => Health -= damage;

    int Health
    {
        get => _health;
        set
        {
            _health = value;
            healthBar.fillAmount = value / (float)_maxHealth;
            HealthLogic();
        }
    }

    void HealthLogic()
    {
        //_anim.SetBool("TookDamage", true); //<--- run everytime he gets hit
       // _anim["AnimationName"].wrapMode = WrapMode.Once;
        _anim.Play("damage");

        if (_health <= _maxHealth / 2)
        {
            _chick.Rage(); //<--- call this only once
            _runAway = true; //<--- call this only once
        }
        if(_health <= 0)
        {
            _anim.SetBool("Death", true); //<--- call this only once
            _runAway = false;
            //gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
            TakeDamage(1);
    }
}
