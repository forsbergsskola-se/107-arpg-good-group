
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Combrat : MonoBehaviour
{
    private NavMeshAgent _navPlayer;
    private Rigidbody _playerRb;
    private Rigidbody _rb;
    private Animator _riggingAnimator;
    private GameObject _player;
    private Vector3 _tempPos;
    private float _shotTimeLeft;
    private float _cryTimer;
    [SerializeField] private float moveSpeed;
    private bool _hasRolled;
    private bool _hasMovedInAir;
    public Image cryCdTimer;
    public Canvas canvas;
    
    //testing
    float temp;
    
    [Header("State")]
    [SerializeField] private State state;
    private enum State
    {
        Idle,
        RockThrowAttack,
        Cry,
        Scream,
        RockAirStrike,
        SandWave,
        Death
    }
    
    public GameObject rockPrefab;
    public Transform combratHand;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = FindObjectOfType<PlayerAttack>().gameObject;
        _navPlayer = _player.GetComponent<NavMeshAgent>();
        _playerRb = _player.GetComponent<Rigidbody>();
        _riggingAnimator = transform.GetComponent<Animator>();
        state = State.Idle;
        Physics.IgnoreCollision(_player.GetComponent<CapsuleCollider>(), GetComponent<Collider>());
        
    }
    
    private void Update()
    {
        if (RockBullet._hasBeenKnockedUp)
        {
            if (_navPlayer.destination != _navPlayer.nextPosition)
            {
                //If player moves when knockedUp(inAir) we save the location and when player lands he starts moving there
                _tempPos = _navPlayer.destination;
                _hasMovedInAir = true;
            }
        }
        if(_playerRb.velocity.y == 0 && RockBullet._hasBeenKnockedUp)
        {
            //Reset navMeshAgent when player reaches ground so we can move again only called when a rock hits then checks when grounded then stops checking
            _navPlayer.updatePosition = true;
            //Warp puts the navMesh at the right place when player lands
            _navPlayer.Warp(_player.transform.position);
            if(_hasMovedInAir)
                _navPlayer.destination = _tempPos;
            _hasMovedInAir = false;
            RockBullet._hasBeenKnockedUp = false;
        }
        
        if(!_hasRolled)
            RngCryOrScream();
        ChangeState();
    }
    
    private void ChangeState()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.RockThrowAttack:
                TimerToShoot();
                RandomMovement();
                break;
           case State.Cry:
               CryBaby();
               break;
           case State.Scream:
               Screamo();
               break;
           case State.RockAirStrike:
               break;
           case State.SandWave:
               break;
            case State.Death:
            default:
                //unexpected things happened
                Application.Quit();
                break;
        }
    }

    public void StartBossFight() => state = State.RockThrowAttack;

    public void StartScream() => state = State.Scream;

    private void RockThrowAttack()
    {
        //When animator plays the Animator Event calls Instantiate to match the throw
        _riggingAnimator.Play("Throw",-1,0);
    }

   private void TimerToShoot()
   {
       _shotTimeLeft -= Time.deltaTime;
       if (_shotTimeLeft < 0 && state == State.RockThrowAttack)
       {
           _shotTimeLeft = 0.5f;
           RockThrowAttack();
       }
   }

   private void RandomMovement()
   {
       //min.x: -6.3 max.x: 7.06
       if (Vector3.Distance(_rb.position, new Vector3(temp,_rb.position.y,_rb.position.z)) < 1f)
       {
           temp = Random.Range(-6.3f, 7.05f);
           Debug.Log(temp);
       }
       else
           _rb.MovePosition(Vector3.MoveTowards(_rb.position, new Vector3(temp,_rb.position.y,_rb.position.z), moveSpeed * Time.deltaTime));
   }

   private void CryTimer()
   {
       //If combrat cries, timer will start for 5secs for the player to hurt the Rock,
       //else the combrat will go into Scream phase and push the player back to starting pos and we go again.
       _cryTimer += Time.deltaTime;
       cryCdTimer.fillAmount = _cryTimer / 5f;

       if (!(_cryTimer > 5) || state != State.Cry) return;
       state = State.Scream;
       _cryTimer = 0f;
   }

   public void InstantiateRock()
   {
       //Instantiates the rockBullet when "Throw" animation plays at the right moment (the 7f is so the rock is not triggering by the ground and destroyed)
       Vector3 pos = combratHand.transform.position;
       pos.y = 7f;
       Instantiate(rockPrefab, pos, Quaternion.identity);
   }

   private void RngCryOrScream()
   {
       Vector3 dir = _player.transform.position - transform.position;
       //Debug.Log(dir.magnitude);
       if (dir.magnitude < 4.5f)
       {
           Roll();
       }
   }

   private void Roll()
   {
       // 80% to Cry / 20% Scream
       //state = Random.Range(0, 1f) <= 0.7 ? State.Cry : State.Scream;
       float tala = Random.Range(0, 1f);
       Debug.Log(tala);
       state = tala <= 0.7 ? State.Cry : State.Scream;
       _hasRolled = true;
   }

   private void CryBaby()
   {
       _riggingAnimator.Play("Crying");
       canvas.gameObject.SetActive(true);
       CryTimer();
   }

   private void Screamo()
   {
       _riggingAnimator.Play("Scream");
       
       //todo: call next state and then change hasRolled to false
       //Todo: Make SandWaves from the back of sandcastle to the start
       canvas.gameObject.SetActive(false);
       _cryTimer = 0;
       cryCdTimer.fillAmount = 0;
       //_hasRolled = false;
   }
}
