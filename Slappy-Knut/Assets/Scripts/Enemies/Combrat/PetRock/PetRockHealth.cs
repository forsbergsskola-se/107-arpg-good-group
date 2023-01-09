
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
        //Disable the poop so it doesn't insta kill boss
        FindObjectOfType<PoopSpawner>().enabled = false;
    }

    public float DefenseRating { get; set; }
    public void TakeDamage(float damage, GameObject attacker) => Health -= 1; //<--- testing 1 damage for maxHealth 5
    public float Health
    {
        get => _health;
        set
        {
            _health = value;
            healthBar.fillAmount = value / maxHealth;
            
            if(_health >= 0)
            {
               FindObjectOfType<Combrat>().StartScream();
            }
            if(_health <= 0)
                OnDeath();
        }
    }

    public void OnDeath()
    {
        //Disables and destroys things after death (gets called once)
        ChangeToDeathLook();
        FindObjectOfType<GateDown>().canGateGoDown = true;
        GetComponent<PetRockMovement>().PetIsDead(); //Stops pet from calling movements 
        Destroy(_rb.GetComponent<SphereCollider>());
        Destroy(_rb);
        healthBar.GetComponentInParent<Canvas>().enabled = false;
        //enable the poop spell
        FindObjectOfType<PoopSpawner>().enabled = true;
        FindObjectOfType<Combrat>().StartDeath();
    }

    private void ChangeToDeathLook()
    {
        //Left eye change to X
        transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
        //Right eye Change to X
        transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(true);
        //Disable eyebrows
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        //Enable Dead twigs n stuff
        transform.GetChild(7).gameObject.SetActive(true);
    }
}


