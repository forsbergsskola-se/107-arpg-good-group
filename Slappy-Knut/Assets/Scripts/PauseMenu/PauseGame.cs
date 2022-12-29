using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject canvas;
    public static bool isPaused;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.SetActive(true);
            isPaused = true;
            Pause();
        }
    }
    
    void Pause ()
    {
        Time.timeScale = 0;
    }
}
