using UnityEngine;

public class ChickBoss : MonoBehaviour
{
    Animator _anim;
    public GameObject player;
    Rigidbody _rb;
    
    //private static readonly int Eat = Animator.StringToHash("Eat");
    //private static readonly int Run = Animator.StringToHash("Run");
   
    [Header("State")]
    [SerializeField]
    private State _state;
    private enum State
    {
        Idle,
        Defend,
        Attack,
        Angry,
    }

    
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        //player = FindObjectOfType<Player>();
        _state = State.Idle;
    }

    // Update is called once per frame
    void Update()
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
            case State.Defend:
                break;
            case State.Angry:
                break;
        }

        //anim.SetBool("Run", true);
    }

    void Attack()
    {
        if (Vector3.Distance(_rb.position, player.transform.position) < 1.5f)
        {
            _anim.SetBool("Jump", true);
            _anim.SetBool("Eat",true);
            
            _anim.SetBool("Walk", false);
        }
        else
        {
            _anim.SetBool("Walk", true);
            float speed = 0.008f;
            transform.LookAt(player.transform);
            Vector3 newPos = Vector3.MoveTowards(_rb.position, player.transform.position, speed);
            _rb.MovePosition(newPos);

            _anim.SetBool("Jump", false);
            _anim.SetBool("Eat", false);
        }
    }

    public void StartBossFight()
    {
        _state = State.Attack;
    }
}
