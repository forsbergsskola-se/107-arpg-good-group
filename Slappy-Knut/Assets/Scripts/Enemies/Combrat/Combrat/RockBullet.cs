using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private PlayerRage _playerRage;
    private void Start()
    {
        _playerRage = FindObjectOfType<PlayerRage>();
    }

    void Update()
    {
        transform.Translate(0, 0, -speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            //Todo: Hurt player, knock back.
            _playerRage.TakeDamage(damage,gameObject);
            Debug.Log("Player got hit!");
        }
        Destroy(gameObject);
    }
}
