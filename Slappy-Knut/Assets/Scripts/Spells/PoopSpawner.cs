using UnityEngine;

public class PoopSpawner : MonoBehaviour
{
    public GameObject poop;
    public float distance = 25;
    public LayerMask walkableLayer;
    
    private int _maxRayCastDistance = 100;
    private GameObject _player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3)) 
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            
            if (Physics.Raycast(rayOrigin, out hitInfo, _maxRayCastDistance, walkableLayer))
            {
                _player = GameObject.FindGameObjectWithTag("Player");
                if (Vector3.Distance(_player.transform.position, hitInfo.point) > distance) return;
                Instantiate(poop, hitInfo.point, Quaternion.Euler(0,0,0));
            }
        }
    }
}
