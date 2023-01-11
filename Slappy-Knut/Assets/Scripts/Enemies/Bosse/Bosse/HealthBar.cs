using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private GameObject _camera;
    private OgreBoss _ogre;
    
    private void Start()
    {
        _ogre = FindObjectOfType<OgreBoss>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    
    private void LateUpdate()
    {
        Quaternion cameraRotation = _camera.transform.rotation;
        transform.LookAt(transform.position + cameraRotation * Vector3.back,
            cameraRotation * Vector3.up); 
    }
}
