using Interfaces;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange;
    public LayerMask enemyLayer;
    public Transform attackPoint;
    
    private Animator _animator;
        
    void Start()
    {
        _animator = GetComponent<Animator>();
        
    }

    void Update()
    { 
        if (Input.GetMouseButtonDown(0))
        {
             
              Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
              RaycastHit hitInfo;
              
              if (Physics.Raycast(rayOrigin, out hitInfo, 100, enemyLayer))
              {
                  Attack();
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
            Debug.Log($"{enemy.name} was hit");
        }
    }
}
