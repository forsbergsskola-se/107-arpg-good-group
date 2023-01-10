using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private Transform _playerTransform;
    private Transform _cameraTransform;
    public float horizontalAngle = 45f;
    public float verticalAngle = 45f;
    public float zoomLevel = -10f;
    void Start()
    {
        _cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // value clamps
        horizontalAngle %= 180;
        zoomLevel = Math.Clamp(zoomLevel, -20, -3);
        verticalAngle = Math.Clamp(verticalAngle, 10, 90);
        
        // prevent repeated calls
        var thisTransform = transform;
        
        // put rig inside of player
        thisTransform.position = _playerTransform.position;
        
        // camera rotation
        thisTransform.localEulerAngles = new Vector3(verticalAngle, horizontalAngle, 0f);
        
        // camera zoom
        _cameraTransform.localPosition = new Vector3(0, 0, zoomLevel);
    }
}
