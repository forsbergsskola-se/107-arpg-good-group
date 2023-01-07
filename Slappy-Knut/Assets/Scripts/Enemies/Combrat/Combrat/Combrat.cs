
using System.Collections;
using UnityEngine;

public class Combrat : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private State state;

    public GameObject rockPrefab;
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
        Instantiate(rockPrefab, transform.position+new Vector3(0,1,0), Quaternion.identity);
    }

   void TimerToShoot()
    {
        _timeLeft -= Time.deltaTime;

        if (_timeLeft < 0 && state == State.RockThrowAttack)
        {
            _timeLeft = 1f;
            RockThrowAttack();
        }
    }
     
}
