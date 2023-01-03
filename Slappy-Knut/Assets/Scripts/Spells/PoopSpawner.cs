using UnityEngine;
using UnityEngine.UI;

public class PoopSpawner : MonoBehaviour
{
    public GameObject poop;
    public float distance = 25;
    public float maxCooldown = 10f;
    public LayerMask walkableLayer;
    public Image cooldownImage;
    
    private int _maxRayCastDistance = 100;
    private GameObject _player;
    private float _cooldown;
    private bool _spellActive;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        cooldownImage.fillAmount = _cooldown / maxCooldown;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && _cooldown <= 0) 
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
                    _cooldown = maxCooldown;
                }
            }
        }
        if (_cooldown < 0) _cooldown = 0;
        else _cooldown -= Time.deltaTime;
        cooldownImage.fillAmount = _cooldown / maxCooldown;
    }
}
