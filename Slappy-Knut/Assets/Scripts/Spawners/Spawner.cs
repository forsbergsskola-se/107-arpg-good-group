using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject knutPrefab;
    public GameObject uiPrefab;
    public GameObject npcPrefab;
    public Transform _playerSpawnPoint;
    public int maxNPCs = 20;
    public static int CurrentNpcCount;

    private GameObject[] _npcSpawnPoints;
    private GameObject _player;
    private static GameObject _ui;
    private Scene _activeScene;
    private float spawnTime = 5;
    private void Awake()
    {
        _npcSpawnPoints = GameObject.FindGameObjectsWithTag("NPCSpawnPoint");
        _player = GameObject.FindGameObjectWithTag("Player");
        CurrentNpcCount = FindObjectsOfType<NPC>().Length;
        if (_player == null)
        {
            Instantiate(knutPrefab, _playerSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            _player.GetComponent<NavMeshAgent>().Warp(_playerSpawnPoint.position);
        }
        if (_ui == null)
        {
            Instantiate(uiPrefab);
            _ui = GameObject.FindGameObjectWithTag("UI");
            if(_ui != null)
                DontDestroyOnLoad(_ui);
        }

        if (Pet.CurrEquippedPet != null && _player != null) // sjicken has to wait for player to spawn
            Pet.CurrEquippedPet.transform.position = _player.transform.position;

    }

    private void Update()
    {
        if (spawnTime > 0) spawnTime -= Time.deltaTime;
        else if (spawnTime == 0 &&_npcSpawnPoints.Length > 0)
        {
           SpawnNPCs();
        }
    }

    void SpawnNPCs()
    {
        if (CurrentNpcCount < maxNPCs)
        {
            Vector3 randomSpawnPoint = _npcSpawnPoints[Random.Range(0, _npcSpawnPoints.Length)].transform.position;
            Instantiate(npcPrefab, randomSpawnPoint, Quaternion.identity);
            CurrentNpcCount++;
        }
        spawnTime = 5;
    }
}
