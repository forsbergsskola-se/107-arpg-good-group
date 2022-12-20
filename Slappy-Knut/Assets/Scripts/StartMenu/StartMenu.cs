using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private GameObject _player;
    private GameObject _inventoryUi;
    private GameObject _inventoryManager;
    private GameObject _canvas;
    public void StartGame()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _inventoryUi = FindObjectOfType<InventoryUI>().gameObject;
        _inventoryManager = FindObjectOfType<Inventory>().gameObject;
        _canvas = FindObjectOfType<Canvas>().gameObject;
        
        DontDestroyOnLoad(_player);
        DontDestroyOnLoad(_inventoryUi);
        DontDestroyOnLoad(_inventoryManager);
        DontDestroyOnLoad(_canvas);
        
        SceneManager.LoadScene("The_Viking_Village");
        _player.SetActive(true);
        _inventoryUi.SetActive(true);
        _inventoryManager.SetActive(true);
        _canvas.SetActive(true);
        _player.transform.position = new Vector3(-85,5,12);
    }
}
