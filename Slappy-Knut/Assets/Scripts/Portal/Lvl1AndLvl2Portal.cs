using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl1AndLvl2Portal : MonoBehaviour
{
    private float _distance;
    private Scene _activeScene;
    private GameObject _player;
    private GameObject _inventoryUi;
    private GameObject _glove;
    private GameObject _canvas;

    private void Start()
    {
        _activeScene = SceneManager.GetActiveScene();
        _player = GameObject.FindGameObjectWithTag("Player");
        _inventoryUi = FindObjectOfType<InventoryUI>().gameObject; 
        _glove = FindObjectOfType<Glove>().gameObject;
        _canvas = FindObjectOfType<Canvas>().gameObject;
        
        DontDestroyOnLoad(_player);
        DontDestroyOnLoad(_inventoryUi);
        DontDestroyOnLoad(_glove);
        DontDestroyOnLoad(_canvas);
    }

    private void Update()
    {
        _distance = Vector3.Distance(_player.transform.position, transform.position);

        if (_distance < 3 && IsEnterPortalPressed())
        {
            UsePortal();
        }
    }

    public void UsePortal()
    {
        if (_activeScene.name == "The_Viking_Village")
        {
            SceneManager.LoadScene("Midnight_Viking_Village_Level");
        }
        else
        {
            SceneManager.LoadScene("The_Viking_Village");
        }
    }

    private bool IsEnterPortalPressed()
    {
        return Input.GetKey(KeyCode.E);
    }
}
