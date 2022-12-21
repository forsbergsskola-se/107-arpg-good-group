using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject knutPrefab;
    public GameObject canvasPrefab;
    public GameObject inventoryUiPrefab;
    public GameObject glovePrefab;

    private GameObject _player;
    private static GameObject _canvas;
    private static GameObject _inventoryUi;
    private static GameObject _glove;
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
        if (_canvas == null)
        {
            Instantiate(canvasPrefab);
            _canvas = FindObjectOfType<Canvas>().gameObject;
            DontDestroyOnLoad(_canvas);
        }
        if (_inventoryUi == null)
        {
            Instantiate(inventoryUiPrefab);
            _inventoryUi = FindObjectOfType<InventoryUI>().gameObject;
            DontDestroyOnLoad(_inventoryUi);
        }
        if (_glove == null)
        {
            Instantiate(glovePrefab, new Vector3(12,4,15), Quaternion.Euler(0,0,0));
            _glove = FindObjectOfType<Glove>().gameObject;
            DontDestroyOnLoad(_glove);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
