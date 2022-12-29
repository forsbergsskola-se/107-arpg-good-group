using System.Collections;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            PauseGame.isPaused = false;
            GetComponentInParent<Canvas>().gameObject.SetActive(false);
        }
    }
    public void ResumeGame ()
    {
        StartCoroutine(WaitForResumeGame());
        PauseGame.isPaused = false;
    }

    public void ExitGame()
    {
        StartCoroutine(WaitForExitGame());
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }

    IEnumerator WaitForResumeGame()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        Time.timeScale = 1;
        GetComponentInParent<Canvas>().gameObject.SetActive(false);
    }
    
    IEnumerator WaitForExitGame()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        Application.Quit();
        Debug.Log("Exiting game.");
    }
}
