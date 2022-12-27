using Interfaces;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f; //How close do we need to be to interact

    private bool _isFocus = false; //Is this interactable currently being focused?
    private Transform _player;//Reference to the player transform
    private bool _hasInteracted = false; //Have we already interacted with the object?

    private void Interact()
    {
        FindObjectOfType<PlayerInteract>().Interact(this);
        FindObjectOfType<PlayerController>().RemoveFocus();
        _hasInteracted = true;
    }

    protected virtual void Update()
    {
        //If we are currently being focused
        //And we haven't already interacted with the object
        if (_isFocus && !_hasInteracted)
        {
            //If we are close enough
            float distance = Vector3.Distance(_player.position, transform.position);
            if (distance <= radius)
            {
                Interact();
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        IDamagable damagable = GetComponent<IDamagable>();
        if (damagable != null) radius = Weapon.CurrEquippedWeapon.Range;
        _isFocus = true;
        _player = playerTransform;
        _hasInteracted = false;
    }

    public void OnDefocused()
    {
        _isFocus = false;
        _player = null;
        _hasInteracted = false;
    }

    // private void OnDrawGizmosSelected()//Creates distance checking between player and object 
    // {
    //     if (interactionTransform == null)
    //     {
    //         interactionTransform = transform;
    //     }
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireSphere(transform.position, radius);
    // }
}