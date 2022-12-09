using System;
using UnityEngine;

public class OutOfArena : MonoBehaviour
{
    private GameObject _player;
    private ChickBoss _chickBoss;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _chickBoss = FindObjectOfType<ChickBoss>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if either chicken and player get out of the arena resets them in the center
        if(other.GetComponent<ChickBoss>())
            _chickBoss.transform.position = new Vector3(0, 0, 0);
        if(other.CompareTag("Player"))
            _player.transform.position = new Vector3(0, 0, 0);
    }
}
