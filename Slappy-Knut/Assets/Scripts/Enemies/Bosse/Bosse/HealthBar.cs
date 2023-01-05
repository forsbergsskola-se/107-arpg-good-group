using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Camera _camera;
    //private OgreBoss _ogre;
    private void Start()
    {
      //  _ogre = FindObjectOfType<OgreBoss>();
        _camera = FindObjectOfType<Camera>();
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.back,
            _camera.transform.rotation * Vector3.up); 
    }
}
