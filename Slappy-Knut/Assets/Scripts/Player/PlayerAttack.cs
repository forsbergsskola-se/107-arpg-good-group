using Interfaces;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask interactableLayer;
    public GameObject attackPoint;
    
    private PlayerRage _playerRage;
    private PlayerSatisfaction _playerSatis;
    private PlayerAudioManager _audioManager;
    [HideInInspector] public Animator _animator;

    void Start()
    {
        _playerRage = GetComponent<PlayerRage>();
        _playerSatis = GetComponent<PlayerSatisfaction>();
        _audioManager = GetComponent<PlayerAudioManager>();
        _animator = GetComponent<Animator>();
    }
    //tied to the animator as an event, only triggered when the slap lands
    public void Attack()
    {
        Weapon wpn = Weapon.CurrEquippedWeapon;
        //Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.transform.position, wpn.Range, interactableLayer);
        //Damage
        
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<IDamagable>().TakeDamage(wpn.Power *  PlayerController.TimeHeld, gameObject);
            _audioManager.AS_BasicSlap.Play();
            _playerRage.TakeDamage(-1f, gameObject);
            _playerSatis.AddSatisfaction(wpn.Power);
        }
    }
    public void AttackHold() //Holds the animation for charge attack
    {
        if (Weapon.CurrEquippedWeapon.Chargable && PlayerController.MouseHeld)
        {
            _animator.speed = 0;
        }
    }
}
