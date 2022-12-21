using Interfaces;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask interactableLayer;
    public GameObject attackPoint;
    
    private PlayerRage _playerRage;
    private PlayerLevelLogic _playerSatis;
    private PlayerAudioManager _audioManager;
    private PlayerController _playerMovement;
    [HideInInspector] public Animator _animator;
    private float damageModifier;

    void Start()
    {
        _playerRage = GetComponent<PlayerRage>();
        _playerSatis = GetComponent<PlayerLevelLogic>();
        _audioManager = GetComponent<PlayerAudioManager>();
        _animator = GetComponent<Animator>();
        damageModifier = 1;
    }

    public void AttackAnimation()
    {
        _animator.Play("attack");
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
            IDamagable damagable = enemy.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(wpn.Power *  PlayerController.TimeHeld, gameObject);
                _audioManager.AS_BasicSlap.Play();

                _playerRage.TakeDamage(-1f, gameObject);
                _playerSatis.IncreaseXP(wpn.Power);    
            }
        }
    }
    public void AttackHold() //Holds the animation for charge attack
    {
        if (Weapon.CurrEquippedWeapon.Chargable && PlayerController.MouseHeld)
        {
            _animator.speed = 0;
        }
    }
    public void IncreaseAttackPower(float powerIncreaseMultiplier)
    {
        damageModifier *= powerIncreaseMultiplier;
    }
}
