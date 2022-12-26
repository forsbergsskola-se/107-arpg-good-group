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
        float weaponPower = Weapon.CurrEquippedWeapon.Power;
        IDamagable enemi = PlayerController.LastClickedTarget.collider.GetComponent<IDamagable>();
        enemi.TakeDamage(weaponPower * PlayerController.TimeHeld);
        _audioManager.AS_BasicSlap.Play();
        _playerRage.TakeDamage(-1f, gameObject);
        _playerSatis.IncreaseXP(weaponPower * 1.1f);
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
