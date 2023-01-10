using System;
using UnityEngine;

public class MinimapCanvas : MonoBehaviour
{
    private Transform _minimap;
    public Transform targetOverride;
    private bool _targetOverridden;
    private void Start()
    {
        if (targetOverride != null) _targetOverridden = true; 
        _minimap = GameObject.FindGameObjectWithTag("MinimapCamera").transform;
    }

    void LateUpdate()
    {
        Quaternion cameraRotation = _minimap.rotation;
        transform.LookAt(transform.position + cameraRotation * Vector3.back,
            cameraRotation * Vector3.up);
        if (_targetOverridden) transform.position = targetOverride.position;
    }
}
