using System;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    public GameObject knutPrefab;
    public GameObject canvasPrefab;

    private GameObject _player;
    private GameObject _canvas;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        //_canvas = FindObjectOfType<Canvas>().gameObject;
        if (_player == null)
        {
            Instantiate(knutPrefab, new Vector3(-85,5,12), Quaternion.Euler(0,0,0));
        }
        else
        {
            _player.transform.position = new Vector3(-85, 5, 12);
        }
        if (_canvas == null)
        {
            Instantiate(canvasPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
