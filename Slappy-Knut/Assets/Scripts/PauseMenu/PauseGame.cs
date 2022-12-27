using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject canvas;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.SetActive(true);
            Pause();
        }
    }
    
    void Pause ()
    {
        Time.timeScale = 0;
    }
}
