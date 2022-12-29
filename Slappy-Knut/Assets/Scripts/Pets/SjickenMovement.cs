using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SjickenMovement : MonoBehaviour
{
    private GameObject _player;

    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerAttack>().gameObject;
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        if (Vector3.Distance(_rb.position, _player.transform.position) < 3f)
        {
            //stop the chicken 3f away from player else follow him
        }
        else
        {
            transform.LookAt(_player.transform);
            Vector3 newPos = Vector3.MoveTowards(_rb.position, _player.transform.position, 0.5f);
            _rb.MovePosition(newPos);    
        }
        
    }
}
