using System.Collections;
using Interfaces;
using UnityEngine;

public class FishLandmine : MonoBehaviour, IItem
{
    public float power;
    public GameObject explosion;
    public GameObject fishBody;

    private AudioSource _audioSource;
    
    public float Power { get; set; }
    public string Description { get; set; }
    public float Cooldown { get; set; }
    public float Range { get; set; }
    public bool Equipable { get; set; }
    public bool Chargable { get; set; }
    public int Count { get; set; }

    public FishLandmine()
    {
        Chargable = false;
        Power = power;
        Description = "Fish that causes damage when you get too close.";
        Cooldown = 20;
        Range = 10;
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    public void Use()
    {
        // is placed at player's position when placed
        gameObject.transform.position = FindObjectOfType<DummyPlayer>().transform.position;
        Count--;
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     // only IDamageable can set it off
    //     // if (other is not IDamagable damageable) return; // only 
    //     _audioSource.time = 1f; // removes audio delay
    //     _audioSource.Play();
    //     damageable.TakeDamage(Power, gameObject);
    //     // coroutine is used to let the particle explosion finish before destroying the game object
    //     StartCoroutine(Explosion());
    // }
    
    private IEnumerator Explosion()
    {
        explosion.SetActive(true);
        Destroy(fishBody);
        yield return new WaitForSecondsRealtime(1.5f);
        Destroy(gameObject);
    }

    public void Charge()
    {
        // the item can not be charged
        throw new System.NotImplementedException();
    }
}
