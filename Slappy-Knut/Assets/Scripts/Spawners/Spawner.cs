using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public GameObject knutPrefab;
    public GameObject uiPrefab;

    private GameObject _player;
    private static GameObject _ui;
    private Scene _activeScene;
    private Transform _playerSpawnPoint;
    private GameObject _backgroundMusic;
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
        if (_ui == null)
        {
            Instantiate(uiPrefab);
            _ui = GameObject.FindGameObjectWithTag("UI");
            DontDestroyOnLoad(_ui);
        }
        _backgroundMusic = GameObject.FindGameObjectWithTag("BackgroundMusic");
        if (_backgroundMusic != null)
        {
            DontDestroyOnLoad(_backgroundMusic);
        }
    }
}
