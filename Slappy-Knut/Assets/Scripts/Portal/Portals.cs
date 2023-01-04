using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portals : MonoBehaviour
{
    public int levelRequirement = 1;
    public string nextScene;
    public TextMeshProUGUI hotkeyText;

    private bool _portalStay;
    private GameObject _player;
    private PlayerLevelLogic _levelLogic;
    private AudioSource _audioSource;
    
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _levelLogic = _player.GetComponent<PlayerLevelLogic>();

        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = .3f;
    }

    private void Update()
    {
        if (_portalStay && _levelLogic.level >= levelRequirement && Input.GetKey(KeyCode.E))
        {
            
            UsePortal();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _portalStay = true;
        if (_levelLogic.level >= levelRequirement) hotkeyText.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        _portalStay = false;
        hotkeyText.gameObject.SetActive(false);
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
    }
    
}
