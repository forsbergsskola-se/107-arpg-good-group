using System;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    public GameObject knutPrefab;

    private GameObject _player;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player == null)
        {
            Instantiate(knutPrefab, new Vector3(-85,5,12), Quaternion.Euler(0,0,0));
        }
        else
        {
            _player.transform.position = new Vector3(-85, 5, 12);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
