using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SjickenMovement : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody _rb;
    private Animator _anim;
    void Start()
    {
        _player = FindObjectOfType<PlayerAttack>().gameObject;
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
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
            _anim.SetBool("Run", false);
            //_anim.SetBool("Eat", true); // dno if we want eat idle
        }
        else
        {
            _anim.SetBool("Run", true);
            transform.LookAt(_player.transform);
            Vector3 newPos = Vector3.MoveTowards(_rb.position, _player.transform.position, 5f * Time.deltaTime);
            _rb.MovePosition(newPos);    
        }
        
    }
}
