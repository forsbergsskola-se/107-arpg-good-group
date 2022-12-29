using System.Collections;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject canvas;
    public static bool IsPaused;
    private GameObject _inventory;

    private void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Inventory");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && IsPaused == false)
        {
            canvas.SetActive(true);
            IsPaused = true;
            _inventory.SetActive(false);
            Pause();
        }
        
        else if (Input.GetKeyDown(KeyCode.Escape) && IsPaused)
        {
            Time.timeScale = 1;
            IsPaused = false;
            canvas.SetActive(false);
            _inventory.SetActive(false);
            Inventory.DescriptionBox.SetActive(false);
        }

        if (IsPaused == false)
        {
            _inventory.SetActive(true);
        }
    }

    private void Pause ()
    {
        Time.timeScale = 0;
    }
    
    public void ResumeGame()
    {
        StartCoroutine(WaitForResumeGame());
        IsPaused = false;
    }
    
    IEnumerator WaitForResumeGame()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        Time.timeScale = 1;
        canvas.SetActive(false);
    }
}
