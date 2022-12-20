using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private GameObject _player;
    private GameObject _inventoryUi;
    private GameObject _inventoryManager;
    public GameObject canvas;
    public void StartGame()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _inventoryUi = FindObjectOfType<InventoryUI>().gameObject;
        _inventoryManager = FindObjectOfType<Inventory>().gameObject;

        DontDestroyOnLoad(_player);
        DontDestroyOnLoad(_inventoryUi);
        DontDestroyOnLoad(_inventoryManager);
        DontDestroyOnLoad(canvas);
        
        SceneManager.LoadScene("The_Viking_Village");
        canvas.GetComponent<Canvas>().enabled = true;
        _player.transform.position = new Vector3(-85,5,12);
    }
}
