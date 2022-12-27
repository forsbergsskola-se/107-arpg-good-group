using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public GameObject knutPrefab;
    public GameObject inventoryUiPrefab;

    private GameObject _player;
    private static GameObject _inventoryUi;
    private Scene _activeScene;
    private Transform _playerSpawnPoint;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerSpawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawnPoint").transform;
        if (_player == null)
        {
            Instantiate(knutPrefab, _playerSpawnPoint.position, Quaternion.Euler(0,0,0));
        }
        else
        {
            _player.transform.position = _playerSpawnPoint.position;
        }
        if (_inventoryUi == null)
        {
            Instantiate(inventoryUiPrefab);
            _inventoryUi = FindObjectOfType<Inventory>().gameObject;
            DontDestroyOnLoad(_inventoryUi);
        }
    }
}
