using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToBosse : MonoBehaviour
{
    public int levelRequirement = 3;
    
    
    private Scene _activeScene;
    private GameObject _player;
    private PlayerLevelLogic _levelLogic;
    private GameObject _glove;

    private AudioSource _audioSource;

    private void Start()
    {
        _activeScene = SceneManager.GetActiveScene();
        _player = GameObject.FindGameObjectWithTag("Player");
        _levelLogic = _player.GetComponent<PlayerLevelLogic>();
        _glove = FindObjectOfType<Glove>().gameObject;

        _audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(_glove);
    }

    
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Level: " + _levelLogic.level);

        if (IsEnterPortalPressed() && _levelLogic.level >= levelRequirement)
        {
            UsePortal();
        }
    }

    public void UsePortal()
    {
        _audioSource.Play();
        StartCoroutine(WaitForSceneLoad());
    }

    private bool IsEnterPortalPressed()
    {
        return Input.GetKey(KeyCode.E);
    }

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(1f);
        if (_activeScene.name == "The_Viking_Village")
        {
            SceneManager.LoadScene("BossScene");
        }
        else
        {
            SceneManager.LoadScene("The_Viking_Village");
        }
    }
}
