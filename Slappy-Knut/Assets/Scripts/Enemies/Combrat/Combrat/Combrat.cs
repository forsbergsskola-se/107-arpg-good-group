
using UnityEngine;

public class Combrat : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private State state;
    
    private Animator _riggingAnimator;
    private Animator _animator;
    private float _timeLeft;
    private enum State
    {
        Idle,
        RockThrowAttack,
        CryOrScream,
        RockAirStrike,
        SandWave,
        Death
    }
    
    public GameObject rockPrefab;
    public Transform combratHand;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _riggingAnimator = transform.GetChild(6).GetComponent<Animator>();
        state = State.Idle;
    }
    
    void Update()
    {
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
           case State.CryOrScream:
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
        //rockPrefab, position, no rotation
        //todo: Add animation when instantiating 
        _riggingAnimator.SetBool("Throwing", true);
        Vector3 pos = combratHand.transform.position;
        pos.y = 7f;
        Instantiate(rockPrefab, pos, Quaternion.identity);
    }

   private void TimerToShoot()
    {
        _timeLeft -= Time.deltaTime;

        if (_timeLeft < 0 && state == State.RockThrowAttack)
        {
            _riggingAnimator.SetBool("Throwing", false);
            _timeLeft = 1f;
            RockThrowAttack();
        }
    }
     
}
