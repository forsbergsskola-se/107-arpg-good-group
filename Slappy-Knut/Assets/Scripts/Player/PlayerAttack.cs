using Interfaces;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange;
    public LayerMask enemyLayer;
    public Transform attackPoint;

    private Animator _animator;
    private PlayerMovement _playerMovement;
    private PlayerRage _playerRage;
    
    //TODO: attack with weapon
    // private Weapon _equippedWeapon;
        
    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        // _equippedWeapon = GetComponent<>()
        _playerRage = GetComponent<PlayerRage>();
    }

    void Update()
    { 
        if (Input.GetMouseButtonDown(0))
        {
             
              Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
              RaycastHit hitInfo;
              
              if (Physics.Raycast(rayOrigin, out hitInfo, _playerMovement.maxRayCastDistance, enemyLayer))
              {
                  Attack();
                  // Attack(_equippedWeapon);
              }
        }
    }
    public void Attack()
    { 
        //Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
        //Damage
        foreach (Collider enemy in hitEnemies)
        {
            _animator.SetTrigger("Attack");
            enemy.GetComponent<IDamagable>().TakeDamage(.1f, gameObject);
            _playerRage.TakeDamage(-1f, gameObject);
            Debug.Log($"{enemy.name} was hit");
        }
    }
    
    // public void Attack(Weapon weapon)
    // { 
    //     //Detect enemies in range of attack
    //     Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
    //     //Damage
    //     foreach (Collider enemy in hitEnemies)
    //     {
    //         _animator.SetTrigger("Attack");
    //         enemy.GetComponent<IDamagable>().TakeDamage(weapon.Power, gameObject);
    //         Debug.Log($"{enemy.name} was hit");
    //     }
    // }
}
