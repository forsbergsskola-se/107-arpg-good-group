using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private GameObject _camera;
    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void LateUpdate()
    {
        //Todo: Needs to look at the camera but not turn around when the camera runs past the boss
        gameObject.transform.LookAt(_camera.transform.position);
    }
}
