
using UnityEngine;
using UnityEngine.AI;

public class RockBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float knockBackForce;
    public static bool _hasBeenKnockedUp;
    
    private PlayerRage _playerRage;
    private GameObject _player;
    private NavMeshAgent _navPlayer;
    private Rigidbody _playerRb;
    private void Start()
    {
        _player = FindObjectOfType<PlayerAttack>().gameObject;
        _navPlayer = _player.GetComponent<NavMeshAgent>();
        _playerRb = _player.GetComponent<Rigidbody>();
        _playerRage = FindObjectOfType<PlayerRage>();
        
        Physics.IgnoreCollision(FindObjectOfType<Combrat>().GetComponent<CapsuleCollider>(), GetComponent<Collider>());
    }

    void Update()
    {
        transform.Translate(0, 0, -speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            _playerRage.TakeDamage(damage,gameObject);
                    
            KnockBack();
        }
        Destroy(gameObject);
    }

    void KnockBack()
    {
        //knock backs the player when hit
        Vector3 difference = (_player.transform.position-transform.position).normalized;
        difference.y = 1f;
        Vector3 force = difference * (knockBackForce);
        _playerRb.AddForce(force, ForceMode.Impulse);
        
        //turns the navmesh off to get the physics on player
        _navPlayer.updatePosition = false;
       
        //resets the path to nothing
        _navPlayer.ResetPath();
        _hasBeenKnockedUp = true;
    }
}
