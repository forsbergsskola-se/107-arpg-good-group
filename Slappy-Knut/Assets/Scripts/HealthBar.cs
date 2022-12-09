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
        //Todo: Needs to look at the camera but not turn around when the camera runs past the boss
        //gameObject.transform.LookAt(_camera.transform.position);
        //this working better
        transform.LookAt(_ogre.transform.position + _camera.transform.position);
    }
}
