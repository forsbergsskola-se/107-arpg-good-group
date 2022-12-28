using System.Collections.Generic;
using Unity.Mathematics;
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
    public int minNPC = 20;

    private GameObject[] _npcSpawnPoints;
    private GameObject _player;
    private List<NPC> _npcs = new();
    private static GameObject _ui;
    private Scene _activeScene;
    private float spawnTime = 5;
    private void Awake()
    {
        _npcSpawnPoints = GameObject.FindGameObjectsWithTag("NPCSpawnPoint");
        _player = GameObject.FindGameObjectWithTag("Player");
        _npcs.AddRange(FindObjectsOfType<NPC>());
        if (_player == null)
        {
            Instantiate(knutPrefab, _playerSpawnPoint.position, Quaternion.Euler(0,0,0));
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
    }

    private void Update()
    {
        spawnTime -= Time.deltaTime;
        if (spawnTime < 0)
        {
            if (_npcs.Count < minNPC)
            {
                Vector3 randomSpawnPoint = _npcSpawnPoints[Random.Range(0, _npcSpawnPoints.Length)].transform.position;
                Instantiate(npcPrefab, randomSpawnPoint, Quaternion.Euler(0, 0, 0));
            }
            spawnTime = 5;
        }
    }
}
