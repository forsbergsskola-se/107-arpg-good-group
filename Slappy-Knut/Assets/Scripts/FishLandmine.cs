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
        gameObject.transform.position = FindObjectOfType<DummyPlayer>().transform.position;
        Count--;
    }

    private void OnTriggerEnter(Collider other)
    {
        _audioSource.time = 1f;
        _audioSource.Play();
        if (other is IDamagable damageable)
        {
            damageable.TakeDamage(Power, gameObject);
        }
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
        throw new System.NotImplementedException();
    }
}
