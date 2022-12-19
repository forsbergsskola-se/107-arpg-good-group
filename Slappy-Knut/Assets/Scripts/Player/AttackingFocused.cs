using System;
using UnityEngine;

public class AttackingFocused : Interactable
{
    private PlayerAttack pAttack;

    private void Start()
    {
        pAttack = FindObjectOfType<PlayerAttack>();
    }

    protected override void Interact()
    {
        AttackAfterWalkingToFocusedTarget();
    }

    void AttackAfterWalkingToFocusedTarget()
    {
        //Walks towards the enemy and calls this when close enough
        pAttack.PlayAnimationOnAttack();
    }
}