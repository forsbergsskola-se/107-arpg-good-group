using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Combrat : MonoBehaviour
{
    private GameObject[] _levels = new GameObject[5];
    private GameObject _getLevel;
    private Rigidbody _rb;
    private Animator _riggingAnimator;
    private GameObject _player;
    private NavMeshAgent _navPlayer;
    private Rigidbody _playerRb;
    private CombratAudioManager _audioManager;
    private Vector3 _tempPos;
    private Vector3 _moveCombrat;
    private int _currLevel;
    private float _shotTimeLeft;
    private float _cryTimer;
    private float _randomPos;
    private const float MinPosY = -31.2f;
    private const float MaxPosY = -29.38f;
    [SerializeField] private float moveSpeed;
    private bool _setOnce;
    private bool _hasReachedRandomPos;
    private bool _hasRolled;
    private bool _hasMovedInAir;
    private bool _isCloserToRightWall;
    public Transform resetPlayerPos;
    public Transform sandWave;
    public Image cryCdTimer;
    public Canvas canvas;
    public GameObject rockPrefab;
    public Transform combratHand;
    [Header("State")]
    [SerializeField] private State state;
    private enum State
    {
        Idle,
        RockThrowAttack,
        Cry,
        Scream,
        Death
    }
    
    
    private void Start()
    {
        _currLevel++;
        _audioManager = GetComponent<CombratAudioManager>();
        _audioManager.AS_Scream.time = 0.95f;
        _rb = GetComponent<Rigidbody>();
        _player = FindObjectOfType<PlayerAttack>().gameObject;
        _navPlayer = _player.GetComponent<NavMeshAgent>();
        _playerRb = _player.GetComponent<Rigidbody>();
        _riggingAnimator = transform.GetComponent<Animator>();
        state = State.Idle;
        Physics.IgnoreCollision(_player.GetComponent<CapsuleCollider>(), GetComponent<Collider>());

        //Finds gameObjects Levels and sets them in correct order in the array
        _getLevel = GameObject.FindGameObjectWithTag("Level");
        for (var i = 0; i < _levels.Length; i++)
        {
            _levels[i] = _getLevel.transform.GetChild(i).gameObject;
        }
    }
    
    private void FixedUpdate()
    {
        //testing
        if(RockBullet.HasBeenKnockedUp)
            KnockUpLogic();
        if(!_hasRolled)
            DetectZoneAroundCombrat();
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
                if(_currLevel > 2)
                {
                    if (_hasReachedRandomPos) MoveToWall();
                    else RandomMovement();
                }
                break;
           case State.Cry:
               CryBaby();
               break;
           case State.Scream:
               if(_hasRolled) ScreamPushPlayerBack();
               if (!PetRockHealth.petGotHit) return;
               LowerLastLevel();
               break;
            case State.Death:
                DeathThings();
                break;
            default:
                //unexpected things happened
                Application.Quit();
                break;
        }
    }

    private void KnockUpLogic() //<--- should be handled in playerMovement i think
    {
        if (RockBullet.HasBeenKnockedUp)
        {
            if (_navPlayer.destination != _navPlayer.nextPosition)
            {
                //If player moves when knockedUp(inAir) we save the location and when player lands he starts moving there
                _tempPos = _navPlayer.destination;
                _hasMovedInAir = true;
            }
            //If player is superKnockedUp(over y:8.5f) we clamp him down(little bit janky but it works)
            if (_player.transform.position.y > 8.5f) _playerRb.velocity = new Vector3(_playerRb.velocity.x,-1,_playerRb.velocity.z);
        }
        if (_playerRb.velocity.y != 0 || !RockBullet.HasBeenKnockedUp) return;
        //Reset navMeshAgent when player reaches ground so we can move again only called when a rock hits then checks when grounded then stops checking
        _navPlayer.updatePosition = true;
        //Warp puts the navMesh at the right place when player lands
        _navPlayer.Warp(_player.transform.position);
        if(_hasMovedInAir)
            _navPlayer.destination = _tempPos;
        _hasMovedInAir = false;
        RockBullet.HasBeenKnockedUp = false;
    }
    
    private void DetectZoneAroundCombrat()
    {
        Vector3 dir = _player.transform.position - transform.position;
        if (dir.magnitude < 5.5f)
            Roll();
        
    }

    private void Roll()
    {
        // 70% to Cry / 30% Scream
        if (Random.Range(0, 1f) <= 0.7)
        {
            state = State.Cry;
            _audioManager.AS_Cry.Play();
        }
        else
        {
            state = State.Scream;
            _audioManager.AS_Cry.Stop();
            _audioManager.AS_Scream.Play();
        }
        _hasRolled = true;
    }
    private void TimerToShoot()
    {
        _shotTimeLeft -= Time.deltaTime;
        if (_shotTimeLeft < 0 && state == State.RockThrowAttack)
        {
            _shotTimeLeft = 0.5f; //<-- how fast we want to shot
            RockThrowAttack();
        }
    }
    
    private void MoveToWall()
    {
        //Check if Player is closer to Left or Right wall and move combrat to that wall that player is closer to
        var position = _rb.position;
        //Change here to bool in collider that checks this and returns true or false for left or right moving to that wall after deciding
        _moveCombrat = Vector3.MoveTowards(position, _isCloserToRightWall ? new Vector3(8,position.y,position.z) :
            new Vector3(-8,position.y,position.z), moveSpeed * Time.deltaTime);
        _rb.MovePosition(_moveCombrat);
    }
    
    private void RandomMovement()
    {
        //min.x: -6.3 max.x: 6.06 // Random number between the walls and moves to it after reaching randPos switch to MoveToWall()
        //Could get lucky and the randNumber doesn't reach the player or we could force to playerPos if its smaller
        if (Vector3.Distance(_rb.position, new Vector3(_randomPos,_rb.position.y,_rb.position.z)) < 1f)
            _hasReachedRandomPos = true;
        else
            _rb.MovePosition(Vector3.MoveTowards(_rb.position, new Vector3(_randomPos,_rb.position.y,_rb.position.z), moveSpeed * Time.deltaTime));
    }
    
    private void CryBaby()
    {
        _riggingAnimator.Play("Crying");
        canvas.gameObject.SetActive(true);
        CryTimer();
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
        _audioManager.AS_Cry.Stop();
        _audioManager.AS_Scream.Play();
    }
    
    private void ScreamPushPlayerBack()
    {
        SandWave();
        
        _riggingAnimator.Play("Scream");
        if(!_setOnce)
        {
            //disable navMesh to move player
            _navPlayer.updatePosition = false;
            _navPlayer.ResetPath();
            //disables and resets all so its rdy to be used again after being pushed to reset
            canvas.gameObject.SetActive(false);
            _cryTimer = 0;
            cryCdTimer.fillAmount = 0;
            _setOnce = true;
            _playerRb.isKinematic = true;
        }
        
        //reached destination and reset all and call the next phase only if rock has been hit else just reset same
        if (Vector3.Distance(_playerRb.position, resetPlayerPos.position) < 1f)
        {
            _navPlayer.updatePosition = true;
            _navPlayer.Warp(_player.transform.position);
            _setOnce = false;
            sandWave.gameObject.SetActive(false);
            state = State.RockThrowAttack;
            _hasRolled = false;
            _playerRb.isKinematic = false;
        }
        else //moves to resetPlayerPos
            _playerRb.MovePosition(Vector3.MoveTowards(_playerRb.position , resetPlayerPos.position, moveSpeed * Time.deltaTime));
    }
    
    private void SandWave()
    {
        // Makes SandWaves from the back of sandcastle to the start
        sandWave.position = _player.transform.position + new Vector3(0,-3.5f,2.3f);
        sandWave.Rotate(0,-50 * Time.deltaTime,0, Space.Self);
        sandWave.gameObject.SetActive(true);
    }
    
    private void LowerLastLevel()
    {
        //Lowers lastLevel under BossArena by 1.1f until it reaches MinPosY. Then starts raising next level
        switch (_levels[_currLevel-1].transform.position.y)
        {
            case > MinPosY:
                _levels[_currLevel-1].transform.position -= new Vector3(0, 1.1f, 0) * Time.deltaTime;
                break;
            case < MinPosY:
                RaiseNextLevel();
                break;
            default:
                RaiseNextLevel();
                break;
        }
    }
    
    private void RaiseNextLevel()
    {
        if (_currLevel == 5) return;
            //Raises level from under BossArena by 0.6f until it reaches maxPosY. Then changes currLevel++
        switch (_levels[_currLevel].transform.position.y)
        {
            case < MaxPosY:
                _levels[_currLevel].transform.position += new Vector3(0, 0.6f, 0) * Time.deltaTime;
                break;
            case > MaxPosY:
                _currLevel++;
                PetRockHealth.petGotHit = false;
                break;
        }
    }
    
    private void DeathThings()
    {
        _audioManager.AS_Scream.Stop();
        //Play this once. maybe it resets when we go to the portal?
        _audioManager.AS_Cry.Play();
        _riggingAnimator.Play("CryFall");
        canvas.gameObject.SetActive(false);
        LowerLastLevel();
    }
    
    public void StartBossFight() => state = State.RockThrowAttack;

    public void StartScream()
    {
        //Gets called from PetRock when he gets hit to start the rageState
        state = State.Scream;
        _audioManager.AS_Cry.Stop();
        _audioManager.AS_Scream.Play();
    }

    public void StartDeath() => state = State.Death;

    private void RockThrowAttack()
    {
        //When animator plays the Animator Event calls Instantiate to match the throw
        _riggingAnimator.Play("Throw",-1,0);
    }
    
   public void InstantiateRock()
   {
       //Instantiates the rockBullet when "Throw" animation plays at the right moment
       Vector3 pos = combratHand.transform.position;
       pos.y = 8f;
       _audioManager.AS_Throw.Play();
       //Throws at players location if its level 1 or 2 else it throws in the random pattern
       if (_currLevel < 3)
       {
           rockPrefab.GetComponent<RockBullet>().speed = 10f;
           Vector3 position = new Vector3(_player.transform.position.x, 6.5f, _player.transform.position.z);
           transform.LookAt(position);
           Instantiate(rockPrefab, pos, transform.rotation);
       }
       else
       {
           rockPrefab.GetComponent<RockBullet>().speed = -5f;
           transform.LookAt(resetPlayerPos);
           Instantiate(rockPrefab, pos, Quaternion.identity);
       }
   }
   
   private void OnCollisionEnter()
   {
       //0.76f is the center and checks if player is right or left of it and sets position to move to that wall
       _isCloserToRightWall = _player.transform.position.x > 0.76f;
       //When Combrat collides with walls we get randomPos
       _randomPos = Random.Range(-5.3f, 6.05f);
       _hasReachedRandomPos = false;
   }
}
