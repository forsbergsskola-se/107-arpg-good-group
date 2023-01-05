using System;
using System.Collections;
using System.Collections.Generic;
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

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartBossFight() => state = State.Attack;
}
