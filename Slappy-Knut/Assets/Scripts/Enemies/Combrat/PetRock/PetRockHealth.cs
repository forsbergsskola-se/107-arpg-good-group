
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class PetRockHealth : Interactable, IDamagable
{

    private Rigidbody _rb;
    private float _health;
    [SerializeField] private float maxHealth = 5;
    public Image healthBar;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _health = maxHealth;
    }

    public float DefenseRating { get; set; }
    public void TakeDamage(float damage, GameObject attacker) => Health -= 1; //<--- testing 1 damage for maxHealth 5
    private float Health
    {
        get => _health;
        set
        {
            _health = value;
            healthBar.fillAmount = value / maxHealth;
            
            if(_health <= 0)
                OnDeath();
        }
    }

    public void OnDeath()
    {
        //Disables and destroys things after death
        //Todo: Makes eyes X when dead
        FindObjectOfType<GateDown>().canGateGoDown = true;
        GetComponent<PetRockMovement>().PetIsDead(); //Stops pet from calling movements 
        Destroy(_rb.GetComponent<SphereCollider>());
        Destroy(_rb);
        healthBar.GetComponentInParent<Canvas>().enabled = false;
    }
}
