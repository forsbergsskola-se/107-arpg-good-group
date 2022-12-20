using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl1AndLvl2Portal : MonoBehaviour
{
    private float _distance;
    private Scene _activeScene;
    private GameObject _player;
    private GameObject _glove;

    private void Start()
    {
        _activeScene = SceneManager.GetActiveScene();
        _player = GameObject.FindGameObjectWithTag("Player");
        _glove = FindObjectOfType<Glove>().gameObject;

        DontDestroyOnLoad(_glove);
    }

    private void Update()
    {
        _distance = Vector3.Distance(_player.transform.position, transform.position);

        if (_distance < 3 && IsEnterPortalPressed())
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
