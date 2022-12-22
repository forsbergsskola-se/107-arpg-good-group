using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerToBosse : MonoBehaviour
{
    public GameObject knutPrefab;
    public GameObject canvasPrefab;
    public GameObject inventoryUiPrefab;
    
    private GameObject _player;
    private static GameObject _canvas;
    private static GameObject _inventoryUi;
    private static GameObject _glove;
    private Scene _activeScene;
    void Awake()
    {
        _activeScene = SceneManager.GetActiveScene();
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player == null)
        {
            Instantiate(knutPrefab, new Vector3(-85,5,12), Quaternion.Euler(0,0,0));
        }
        else
        {
            if (_activeScene.name == "Boss_Scene_Portal_Level")
            {
                _player.transform.position = new Vector3(0, 0, 12);
            }
            else
            {
                _player.transform.position = new Vector3(-85, 5, 12);
            }
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
            //Instantiate(glovePrefab, new Vector3(12,4,15), Quaternion.Euler(0,0,0));
            _glove = FindObjectOfType<Glove>().gameObject;
            DontDestroyOnLoad(_glove);
        }
        
        if (_activeScene.name != "Day_Viking_Village_Level")
        {
            _glove.transform.position = new Vector3(0, 100, 0);
        }
    }
}
