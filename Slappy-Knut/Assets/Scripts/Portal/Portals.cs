using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portals : MonoBehaviour
{
    public int levelRequirement = 1;
    public string nextScene;

    private bool _portalStay;
    private GameObject _player;
    private PlayerLevelLogic _levelLogic;
    private GameObject _backgroundMusic;

    private AudioSource _audioSource;
    
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _levelLogic = _player.GetComponent<PlayerLevelLogic>();
        _backgroundMusic = GameObject.FindGameObjectWithTag("BackgroundMusic");

        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_portalStay && _levelLogic.level >= levelRequirement && Input.GetKey(KeyCode.E)) UsePortal();
    }

    private void OnTriggerEnter(Collider other)
    {
        _portalStay = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _portalStay = false;
    }

    private void UsePortal()
    {
        _audioSource.Play();
        StartCoroutine(WaitForSceneLoad());
    }
    
    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(1f);
        if(nextScene != null) SceneManager.LoadScene(nextScene);
        Destroy(_backgroundMusic);
    }
    
}
