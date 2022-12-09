using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask walkableLayer;
    public int maxRayCastDistance = 100;
    private NavMeshAgent _meshAgent;
    private Animator _animator;
    private PlayerAudioManager _audioManager;
    private void Start()
    {
        _audioManager = GetComponent<PlayerAudioManager>();
        _animator = GetComponent<Animator>();
        _meshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) // left click
        {
            //the ray going from main camera
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            //moves the player to the clicked point if it's a walkablelayer
            if (Physics.Raycast(rayOrigin, out RaycastHit hitInfo, maxRayCastDistance, walkableLayer))
            {
                _meshAgent.SetDestination(hitInfo.point);
            }
        }
        //plays the running animation if the player is moving fast enough
        _animator.SetBool("isRunning", _meshAgent.velocity.magnitude >= .5);
    }

    public void PlayStepSound() //called as an event in the animator
    {
        _audioManager.AS_FootSteps.Play();
    }
}
