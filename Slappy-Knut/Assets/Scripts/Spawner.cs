using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public GameObject knutPrefab;
    public GameObject inventoryUiPrefab;
    public GameObject glovePrefab;

    private GameObject _player;
    private static GameObject _inventoryUi;
    private static GameObject _glove;
    private Scene _activeScene;
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
        

        _activeScene = SceneManager.GetActiveScene();
        if (_activeScene.name != "VillageDay")
        {
            _glove.transform.position = new Vector3(0,100,0);
        }
    }
}
