using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private GameObject _player;
    public float offsetX = 6.5f;
    public float offsetY = 9f;
    public float offsetZ = 7.5f;
    private Vector3 _cameraOffset;
    void Start()
    {
        _cameraOffset = new Vector3(offsetX, offsetY, offsetZ);
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 playerPosition = _player.transform.position;
        transform.position = new Vector3(playerPosition.x + offsetX, playerPosition.y + offsetY, playerPosition.z + offsetZ);
    }
}
