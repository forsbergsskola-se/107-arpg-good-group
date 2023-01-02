using System.Collections;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject canvas;
    public static bool IsPaused;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsPaused) Pause();
        else if (Input.GetKeyDown(KeyCode.Escape) && IsPaused) ResumeGame();
    }

    private void Pause ()
    {
        canvas.SetActive(true);
        IsPaused = true;
        Inventory.inventoryUI.SetActive(false);
        Time.timeScale = 0;
    }
    
    public void ResumeGame()
    {
        StartCoroutine(WaitForSound());
    }

    IEnumerator WaitForSound()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1;
        IsPaused = false;
        canvas.SetActive(false);
        Inventory.DescriptionBox.SetActive(false);
        Inventory.inventoryUI.SetActive(false);
    }
}
