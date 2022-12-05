using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickBoss : MonoBehaviour
{
    public Animator anim;
    [Header("State")]
    private State _state;
    private enum State
    {
        Idle,
        Normal,
        Attack,
        Eating,
    }

    
    void Start()
    {
        _state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        //anim.SetBool("Run", true);
    }

    void Animations()
    {
        
    }
}
