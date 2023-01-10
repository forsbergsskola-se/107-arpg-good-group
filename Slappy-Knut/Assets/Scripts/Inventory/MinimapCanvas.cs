using System;
using UnityEngine;

public class MinimapCanvas : MonoBehaviour
{
    private Transform _minimap;
    private void Start()
    {
        _minimap = GameObject.FindGameObjectWithTag("MinimapCamera").transform;
    }

    void LateUpdate()
    {
        Quaternion cameraRotation = _minimap.rotation;
        transform.LookAt(transform.position + cameraRotation * Vector3.back,
            cameraRotation * Vector3.up); 
    }
}
