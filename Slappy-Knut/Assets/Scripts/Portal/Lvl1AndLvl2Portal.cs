using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl1AndLvl2Portal : MonoBehaviour
{
    public int levelRequirement = 1;
    
    
    private Scene _activeScene;
    private GameObject _player;
    private PlayerLevelLogic _levelLogic;
    private GameObject _glove;

    private void Start()
    {
        _activeScene = SceneManager.GetActiveScene();
        _player = GameObject.FindGameObjectWithTag("Player");
        _levelLogic = _player.GetComponent<PlayerLevelLogic>();
        _glove = FindObjectOfType<Glove>().gameObject;

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
        if (_activeScene.name == "The_Viking_Village")
        {
            SceneManager.LoadScene("Midnight_Viking_Village_Level");
        }
        else
        {
            SceneManager.LoadScene("The_Viking_Village");
        }
    }

    private bool IsEnterPortalPressed()
    {
        return Input.GetKey(KeyCode.E);
    }
}
