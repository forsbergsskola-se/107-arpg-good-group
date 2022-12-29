using System.Collections;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ExitGame()
    {
        StartCoroutine(WaitForExitGame());
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }

    IEnumerator WaitForExitGame()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        Application.Quit();
        Debug.Log("Exiting game.");
    }
}
