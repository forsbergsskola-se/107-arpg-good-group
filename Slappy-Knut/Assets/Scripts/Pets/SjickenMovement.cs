using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SjickenMovement : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody _rb;
    void Start()
    {
        _player = FindObjectOfType<PlayerAttack>().gameObject;
        _rb = GetComponent<Rigidbody>();
        //testing
        DontDestroyOnLoad(gameObject);
    }

    void LateUpdate()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        if (Vector3.Distance(_rb.position, _player.transform.position) < 4f)
        {
            //stop the chicken 4f away from player else follow him
        }
        else
        {
            transform.LookAt(_player.transform);
            Vector3 newPos = Vector3.MoveTowards(_rb.position, _player.transform.position, 5f * Time.deltaTime);
            _rb.MovePosition(newPos);    
        }
        
    }
}
