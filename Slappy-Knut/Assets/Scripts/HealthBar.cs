using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private GameObject _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Todo: Needs to look at the camera but not turn around when the camera runs past the boss
        gameObject.transform.LookAt(_camera.transform.position);
    }
}
