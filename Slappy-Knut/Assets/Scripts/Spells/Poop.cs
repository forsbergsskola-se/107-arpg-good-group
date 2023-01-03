using Interfaces;
using UnityEngine;

public class Poop : MonoBehaviour
{
    public int power;

    private GameObject _player;
    private PlayerRage _playerRage;
    private PlayerLevelLogic _playerSatis; 
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRage = _player.GetComponent<PlayerRage>();
        _playerSatis = _player.GetComponent<PlayerLevelLogic>();
        Invoke("Destroy", 2f);
    }
    
    private void OnTriggerStay(Collider other)
    {
        IDamagable target = other.GetComponent<IDamagable>();
        
        if (target != null && !other.gameObject.CompareTag("Player"))
        {
            target.TakeDamage(power, _player);
            _playerRage.TakeDamage(-1f, _player);
            _playerSatis.IncreaseXP(power);
        }
    }

    void Destroy() => Destroy(gameObject);
}
