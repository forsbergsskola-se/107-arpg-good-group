using Interfaces;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask enemyLayer;
    public GameObject attackPoint;
    
    private PlayerRage _playerRage;
    private PlayerSatisfaction _playerSatis;
    private PlayerAudioManager _audioManager;
    private PlayerController _playerMovement;
    private Animator _animator;

    private bool _mouseHeld;
    private float _timeHeld = 1;

    void Start()
    {
        //To equip glove at start
        _playerRage = GetComponent<PlayerRage>();
        _playerSatis = GetComponent<PlayerSatisfaction>();
        _audioManager = GetComponent<PlayerAudioManager>();
        _playerMovement = GetComponent<PlayerController>();
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
                transform.LookAt(hitInfo.transform);
                //distance check between target and player
                if (Vector3.Distance(hitInfo.transform.position, transform.position) < Weapon.CurrEquippedWeapon.Range)
                {
                    _timeHeld += Time.deltaTime;
                    _animator.Play("attack");
                } 
            }
        }
        if(_timeHeld -1 > Weapon.CurrEquippedWeapon.ChargeTime || !_mouseHeld) AttackRelease();
    }
    
    //tied to the animator as an event, only triggered when the slap lands
    public void Attack()
    {
        Weapon wpn = Weapon.CurrEquippedWeapon;
        //Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.transform.position, wpn.Range, enemyLayer);
        //Damage
        foreach (Collider enemy in hitEnemies)
        {
            _audioManager.AS_BasicSlap.Play();
            enemy.GetComponent<IDamagable>().TakeDamage(wpn.Power * _timeHeld, gameObject);
            Debug.Log(wpn.Power * _timeHeld);
            _playerRage.TakeDamage(-1f, gameObject);
            _playerSatis.AddSatisfaction(wpn.Power);
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
        _timeHeld = 1;
    }

    public void PlayAnimationOnAttack()
    {
        _animator.Play("attack");
    }
}
