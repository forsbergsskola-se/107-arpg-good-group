using Interfaces;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask enemyLayer;
    public GameObject attackPoint;
    
    private PlayerRage _playerRage;
    private PlayerSatisfaction _playerSatis;
    private PlayerAudioManager _audioManager;
    private PlayerMovement _playerMovement;
    private Animator _animator;

    private bool _mouseHeld;
    
    void Start()
    {
        Weapon.CurrEquippedWeapon = attackPoint.AddComponent<Hand>();
        _playerRage = GetComponent<PlayerRage>();
        _playerSatis = GetComponent<PlayerSatisfaction>();
        _audioManager = GetComponent<PlayerAudioManager>();
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _mouseHeld = Input.GetMouseButton(1);
        if (_mouseHeld)
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo, _playerMovement.maxRayCastDistance, enemyLayer))
            {
                //distance check between target and player
                if (Vector3.Distance(hitInfo.transform.position, transform.position) < Weapon.CurrEquippedWeapon.Range)
                {
                    _animator.Play("attack");
                } 
            }
        }
        else AttackRelease();
    }
    
    //tied to the animator as an event, only triggered when the slap lands
    public void Attack() 
    {
        //Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.transform.position, Weapon.CurrEquippedWeapon.Range, enemyLayer);
        //Damage
        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log(Weapon.CurrEquippedWeapon.Power);
            _audioManager.AS_BasicSlap.Play();
            enemy.GetComponent<IDamagable>().TakeDamage(Weapon.CurrEquippedWeapon.Power, gameObject);
            _playerRage.TakeDamage(-1f, gameObject);
            _playerSatis.AddSatisfaction(Weapon.CurrEquippedWeapon.Power);
            Debug.Log($"{enemy.name} was hit");
        }
    }
    public void AttackHold() //Holds the animation for charge attack
    {
        if (Weapon.CurrEquippedWeapon.Chargable && _mouseHeld)
        {
            _animator.speed = 0;
        }
    }
    public void AttackRelease()
    {
        _animator.speed = 1;
    }
    
}
