using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f; //How close do we need to be to interact
    public Transform interactionTransform; //The transform where we interact

    private bool isFocus = false; //Is this interactable currently being focused?
    private Transform player;//Reference to the player transform

    private bool hasInteracted = false; //Have we already interacted with the object?

    public virtual void Interact()
    {
        //This mehtod is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }

    private void Update()
    {
        //If we are currently being focused
        //And we haven't already interacted with the object
        if (isFocus && !hasInteracted)
        {
            //If we are close enough
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
