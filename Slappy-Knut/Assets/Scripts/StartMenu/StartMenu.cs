using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        _audioSource.Play();
        StartCoroutine(WaitForStartGame());
    }

    public void ExitGame()
    {
        _audioSource.Play();
        StartCoroutine(WaitForExitGame());
    }

    private IEnumerator WaitForStartGame()
    {
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene(1);
    }

    private IEnumerator WaitForExitGame()
    {
        yield return new WaitForSeconds(0.6f);
        //#if UNITY_EDITOR
        //UnitEditor.EditorApplication.isPlaying = false;
        //#endif
        Application.Quit();
    }
}
