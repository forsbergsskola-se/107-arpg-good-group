
using UnityEngine;

public class Combrat : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private State state;
    private enum State
    {
        Idle,
        Attack,
    }

    void Start()
    {
        
        state = State.Idle;
    }
    
    void Update()
    {
        
    }
    
    public void StartBossFight() => state = State.Attack;
}
