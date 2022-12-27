using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public void ResumeGame ()
    {
        Time.timeScale = 1;
        GetComponentInParent<Canvas>().gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exiting game.");
    }
}
