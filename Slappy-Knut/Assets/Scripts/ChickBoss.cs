using UnityEngine;

public class ChickBoss : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float knockBackForce;
    private bool _once;

    private Animator _anim;
    private Rigidbody _rb;
    public GameObject player;
    
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

    
    private void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        //player = FindObjectOfType<Player>();
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
            case State.Defend:
                break;
            case State.Angry:
                break;
        }
    }

    private void Attack()
    {
        if (Vector3.Distance(_rb.position, player.transform.position) < 1f)
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
            player.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse); 
            
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

    public void Rage()
    {
        _state = State.Angry;
        
    }
}
