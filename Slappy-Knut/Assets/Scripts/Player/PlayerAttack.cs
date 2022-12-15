using System;
using Interfaces;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask enemyLayer;
    public GameObject attackPoint;
    // public Weapon equippedWeapon;
    
    private PlayerRage _playerRage;
    private PlayerSatisfaction _playerSatis;
    private PlayerAudioManager _audioManager;
    private PlayerMovement _playerMovement;
    private Animator _animator;
    
    void Start()
    {
        Weapon.CurrEquippedWeapon = attackPoint.AddComponent<Hand>();
        // equippedWeapon = Weapon.CurrEquippedWeapon;
        _playerRage = GetComponent<PlayerRage>();
        _playerSatis = GetComponent<PlayerSatisfaction>();
        _audioManager = GetComponent<PlayerAudioManager>();
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo, _playerMovement.maxRayCastDistance, enemyLayer))
            {
                //distance check between target and player
                if (Vector3.Distance(hitInfo.transform.position, transform.position) < Weapon.CurrEquippedWeapon.Range)
                {
                    _animator.SetTrigger("Attack");
                } 
            }
        }
    }

    //tied to the animator as an event, only triggered when the slap lands
    public void Attack() 
    {
        if (Weapon.CurrEquippedWeapon.Chargable)
        {
            float chargeTime = Weapon.CurrEquippedWeapon.ChargeTime;
            if(chargeTime > 0) chargeTime -= Time.deltaTime;
            if(chargeTime < 0) BaseAttack();
        }
        else BaseAttack();
    }

    public void BaseAttack()
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
}
