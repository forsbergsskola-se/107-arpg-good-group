
using UnityEngine;
using Random = UnityEngine.Random;

public class Combrat : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private State state;
    
    private Animator _riggingAnimator;
    private GameObject _player;
    private float _timeLeft;
    private bool _hasRolled;
    private bool _hasCryTimerBeenSet;
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
        _player = FindObjectOfType<PlayerAttack>().gameObject;
        _riggingAnimator = transform.GetComponent<Animator>();
        state = State.Idle;
    }
    
    private void Update()
    {
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

    private void RockThrowAttack()
    {
        //When animator plays the Animator Event calls Instantiate to match the throw
        _riggingAnimator.Play("Throw",-1,0);
    }

   private void TimerToShoot()
   {
       _timeLeft -= Time.deltaTime;
       switch (_timeLeft)
       {
           case < 0 when state == State.RockThrowAttack:
               _timeLeft = 1f;
               RockThrowAttack();
               break;
           case < 0 when state == State.Cry:
               state = State.RockThrowAttack; // <-- if he starts go cry maybe we dont want always to default to this one, test and see
               _hasRolled = false; // <--- hope this works and we go again if we roll again if close enough? and
               _timeLeft = 5f;
               //Roll(); //maybe scream if player doesn't go in after 5secs, or we Roll and is close enough, and if he isn't close enough we start just rockThrowAttack again?
               break;
       }
   }

   public void InstantiateRock()
   {
       //Instantiates the rockBullet when "Throw" plays at the right moment (the 7f is so the rock is not triggering on the ground
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
       //todo: timer 5sec then reRoll
       if (!_hasCryTimerBeenSet)
       {
           _timeLeft = 5f;
           _hasCryTimerBeenSet = true;
       }
      
       TimerToShoot();
   }

   private void Screamo()
   {
       _riggingAnimator.Play("Scream");
   }
}
