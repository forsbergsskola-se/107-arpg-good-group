using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private GameObject _camera;
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    void LateUpdate()
    {
        //Todo: Needs to look at the camera but not turn around when the camera runs past the boss
        gameObject.transform.LookAt(_camera.transform.position);
    }
}
