using System;
using UnityEngine;

public class MinimapCanvas : MonoBehaviour
{
    public Transform targetOverride;
    private bool _targetOverridden;
    private void Start()
    {
        if (targetOverride != null) _targetOverridden = true;
    }

    void LateUpdate()
    {
        Quaternion cameraRotation = CameraFollowMinimap.Minimap.transform.rotation;
        transform.LookAt(transform.position + cameraRotation * Vector3.back,
            cameraRotation * Vector3.up);
        if (_targetOverridden) transform.position = targetOverride.position;
    }
}
