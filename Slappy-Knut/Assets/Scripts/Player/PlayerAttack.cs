using Interfaces;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask enemyLayer;
    public Transform attackPoint;

    private PlayerMovement _playerMovement;
    private PlayerRage _playerRage;
    private PlayerSatisfaction _playerSatis;
    private PlayerAudioManager _audioManager;
    private Animator _animator;
    
    //TODO: attack with weapon
    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerRage = GetComponent<PlayerRage>();
        _playerSatis = GetComponent<PlayerSatisfaction>();
        _audioManager = GetComponent<PlayerAudioManager>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    { 
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
              RaycastHit hitInfo;

              if (Physics.Raycast(rayOrigin, out hitInfo, _playerMovement.maxRayCastDistance, enemyLayer))
              {
                  //distance check between target and player
                  if (Vector3.Distance(hitInfo.transform.position, transform.position) < 2)
                  {
                        _animator.SetTrigger("Attack");
                  } 
              }
        }
    }
    //tied to the animator as an event, only triggered when the slap lands
    public void Attack() 
    {
        //Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, Weapon.CurrEquippedWeapon.Range, enemyLayer);
        //Damage
        foreach (Collider enemy in hitEnemies)
        {
            _audioManager.AS_BasicSlap.Play();
            //values are temporary until we have a weapon
            enemy.GetComponent<IDamagable>().TakeDamage(Weapon.CurrEquippedWeapon.Power, gameObject);
            _playerRage.TakeDamage(-1f, gameObject);
            _playerSatis.AddSatisfaction(Weapon.CurrEquippedWeapon.Power);
            Debug.Log($"{enemy.name} was hit");
        }
    }
}
