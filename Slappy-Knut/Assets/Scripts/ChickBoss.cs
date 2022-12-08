using UnityEngine;

public class ChickBoss : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float knockBackForce;
    private bool _once;
    private bool enRaged;

    private Animator _anim;
    private Rigidbody _rb;
    private Rigidbody _playerRb;
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
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _playerRb = player.GetComponent<Rigidbody>();
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
            case State.Angry:
                EnRaged();
                break;
            case State.OgreDeath: // should here change into idle state until player picks him up as a weapon or item.
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

    void EnRaged()
    {
        
        _anim.SetBool("Run", false);
        if(!enRaged)
        {
            _anim.Play("Jump W Root");
            _rb.transform.localScale += new Vector3(2,2,2) * Time.deltaTime; // <--- once so he only grows once
            if (_rb.transform.localScale.x > 15f) 
                enRaged = true;
        }
        
        
        //Todo: Charge move, he stops looksAt(player.transform.position) and charges in a straight line to that positions
        //Todo: direction until he hits something and repeats(so the player can dodge out the way).
    }
}
