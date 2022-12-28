using Interfaces;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttack : MonoBehaviour
{
    private PlayerRage _playerRage;
    private PlayerLevelLogic _playerSatis;
    private PlayerAudioManager _audioManager;
    [HideInInspector] public static Animator _animator;
    private float damageModifier;
    
    public static float TimeHeld = 1;
    public static float CurrCooldown;

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
        if (CurrCooldown < 0) _animator.Play("attack");
        
    }
    //tied to the animator as an event, only triggered when the slap lands
    public void Attack()
    {
        Transform targetTransform = PlayerController.LastClickedTarget.collider.transform;
            transform.LookAt(targetTransform);
            float weaponPower = Weapon.CurrEquippedWeapon.Power;
            IDamagable enemy = PlayerController.LastClickedTarget.collider.GetComponent<IDamagable>();
            if (Vector3.Distance(transform.position, targetTransform.position) <= Weapon.CurrEquippedWeapon.Range)
            {
                enemy.TakeDamage(weaponPower * TimeHeld);
                _audioManager.AS_BasicSlap.Play();
                _playerRage.TakeDamage(-1f, gameObject);
                _playerSatis.IncreaseXP(weaponPower * 1.1f);
                GetComponent<NavMeshAgent>().SetDestination(transform.position);
                CurrCooldown = Weapon.CurrEquippedWeapon.Cooldown;
            }
    }
    public void AttackHold() //Holds the animation for charge attack
    {
        if (Weapon.CurrEquippedWeapon.Chargable && PlayerController.MouseHeld)
        {
            _animator.speed = 0;
        }
    }
    public static void AttackRelease()
    {
        _animator.speed = 1;
        TimeHeld = 1;
    }
    public void IncreaseAttackPower(float powerIncreaseMultiplier)
    {
        damageModifier *= powerIncreaseMultiplier;
    }
}
