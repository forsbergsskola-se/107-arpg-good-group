using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask walkableLayer;
    public int maxRayCastDistance = 100;
    private NavMeshAgent _meshAgent;
    private Animator _animator;
    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _meshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) // left click
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo, maxRayCastDistance, walkableLayer))
            {
                _meshAgent.SetDestination(hitInfo.point);
            }
        }
        _animator.SetBool("isRunning", _meshAgent.velocity.magnitude >= .5);
    }

    public void PlayStepSound()
    {
        _audioSource.Play();
    }
}
