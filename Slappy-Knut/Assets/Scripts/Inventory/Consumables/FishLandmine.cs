using System.Collections;
using Interfaces;
using UnityEngine;

public class FishLandmine : MonoBehaviour, IItem
{
    public float power;
    public GameObject explosion;
    public GameObject fishBody;

    public float Power { get; set; }
    public string Description { get; set; }
    public float Cooldown { get; set; }
    public float Range { get; set; }
    public bool Equipable { get; set; }
    public bool Chargable { get; set; }
    
    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Chargable = false;
        Power = power;
        Description = "Fish that causes damage when you get too close.";
        Cooldown = 20;
        Range = 10;
    }
    public void Use() {}
    private void OnTriggerEnter(Collider other) // this is supposed to be DoT maybe?
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        // only non-player IDamagables should set it off
        if (damagable == null || other.gameObject.CompareTag("Player")) return;
        
        _audioSource.time = 1f; // removes audio delay
        _audioSource.Play();
        damagable.TakeDamage(Power, gameObject);
        // coroutine is used to let the particle explosion finish before destroying the game object
        StartCoroutine(Explosion());
    }
    private IEnumerator Explosion()
    {
        explosion.SetActive(true);
        Destroy(fishBody);
        yield return new WaitForSecondsRealtime(1.5f);
        Destroy(gameObject);
    }
    public void Charge(){}
}
