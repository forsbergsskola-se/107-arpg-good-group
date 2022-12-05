using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask walkableLayer;
    public int maxRayCastDistance = 100;
    private NavMeshAgent _meshAgent;
    private Animator _animator;


    private void Start()
    {
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
                _animator.SetBool("isRunning", true);
                _meshAgent.SetDestination(hitInfo.point);
            }
        }
        if(_meshAgent.velocity.magnitude <= .5) _animator.SetBool("isRunning", false);
    }
}
