using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("The_Viking_Village");

    }

    public void ExitGame()
    {
        //#if UNITY_EDITOR
        //UnitEditor.EditorApplication.isPlaying = false;
        //#endif
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
