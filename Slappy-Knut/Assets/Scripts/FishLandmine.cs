using System.Collections;
using UnityEngine;

public class FishLandmine : Consumable
{
    public float power;
    public GameObject explosion;
    public GameObject fishBody;

    private AudioSource _audioSource;

    public override bool Chargeable { get; }
    public override float Power { get; }
    public override string Description { get; }
    public override float Cooldown { get; }
    public override float Range { get; }
    public override int Count { get; set; }

    public FishLandmine()
    {
        Chargeable = false;
        Power = power;
        Description = "Fish that causes damage when you get too close.";
        Cooldown = 20;
        Range = 10;
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Use()
    {
        // is placed at player's position when placed
        gameObject.transform.position = FindObjectOfType<DummyPlayer>().transform.position;
        Count--;
    }

    private void OnTriggerEnter(Collider other)
    {
        // only IDamageable can set it off
        if (other is not IDamagable damageable) return; // only 
        _audioSource.time = 1f; // removes audio delay
        _audioSource.Play();
        damageable.TakeDamage(Power, gameObject);
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

    public override void Charge()
    {
        // the item can not be charged
        throw new System.NotImplementedException();
    }
}
