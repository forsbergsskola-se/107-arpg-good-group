using UnityEngine;
using UnityEngine.UI;

public class PoopSpawner : MonoBehaviour
{
    public GameObject poop;
    public float distance = 25;
    public float maxCoolDown = 10f;
    public LayerMask walkableLayer;
    public Image coolDownImage;
    
    private int _maxRayCastDistance = 100;
    private GameObject _player;
    private float _coolDown;
    private bool _spellActive;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        coolDownImage.fillAmount = _coolDown / maxCoolDown;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && _coolDown <= 0) 
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            
            if (Physics.Raycast(rayOrigin, out hitInfo, _maxRayCastDistance, walkableLayer))
            {
                _player = GameObject.FindGameObjectWithTag("Player");
                if (Vector3.Distance(_player.transform.position, hitInfo.point) > distance) _audioSource.Play();
                else
                {
                    Instantiate(poop, hitInfo.point, Quaternion.Euler(0,0,0));
                    _coolDown = maxCoolDown;
                }
            }
        }
        if (_coolDown < 0) _coolDown = 0;
        else _coolDown -= Time.deltaTime;
        coolDownImage.fillAmount = _coolDown / maxCoolDown;
    }
}
