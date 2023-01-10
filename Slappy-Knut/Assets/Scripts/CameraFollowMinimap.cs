using UnityEngine;

public class CameraFollowMinimap : MonoBehaviour 
{
    private Transform _playerTransform;
    private Transform _mainCameraTransform;
    private readonly int _lockedHeight = 80;

    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _mainCameraTransform = FindObjectOfType<CameraFollow>().transform;
    }
    
    void Update()
    {
        
        // prevent repeated calls
        Transform thisTransform = transform;
        Vector3 playerPos = _playerTransform.position;

        thisTransform.position = new(playerPos.x, _lockedHeight, playerPos.z);
        
        // set camera rotation to 
        thisTransform.localEulerAngles = new Vector3(90f, _mainCameraTransform.eulerAngles.y, 0f);
    }
}
