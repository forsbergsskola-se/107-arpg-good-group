using System.Collections;
using Interfaces;
using UnityEngine;

public class FishLandmine : Interactable, IConsumable
{
    public float power;
    public GameObject explosion;
    public GameObject fishBody;
    public Sprite icon;
    public GameObject prefab;

    private PlayerLevelLogic _playerLevelLogic;
    private PlayerRage _playerRage;

    public string Type { get; }
    public string Name { get; set; }
    public Sprite Icon { get; set; }
    public float Power { get; set; }
    public string Description { get; set; }
    public float Cooldown { get; set; }
    public float Range { get; set; }
    public static int Count { get; set; }

    private AudioSource _audioSource;

    private void Start()
    {
        Icon = icon;
        _audioSource = GetComponent<AudioSource>();
        Power = power;
        Description = "Fish that causes damage when you get too close.";
        Cooldown = 20;
        Range = 10;

        GameObject player = GameObject.FindGameObjectWithTag("Player"); //THIS CODE IS FRAGILE
        _playerLevelLogic = player.GetComponent<PlayerLevelLogic>();//We should probaby come up with something better for this
        _playerRage = player.GetComponent<PlayerRage>();
    }
    public void Use()
    {
        if (Count < 1) return;
        
        Transform pTransform = FindObjectOfType<PlayerController>().transform;
        Vector3 p = pTransform.transform.position;
        Vector3 spawnOffset = new Vector3(p.x, p.y + 0.2f, p.z);
        Instantiate(prefab, spawnOffset, pTransform.rotation);
        Count--;
    }
    private void OnTriggerEnter(Collider other) // this is supposed to be DoT maybe?
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        // only non-player IDamagables should set it off
        if (damagable == null || other.gameObject.CompareTag("Player")) return;
        
        _audioSource.time = 1f; // removes audio delay
        _audioSource.Play();
        damagable.TakeDamage(Power, gameObject);
        _playerLevelLogic.IncreaseXP(power);
        _playerRage.TakeDamage(power * -1, gameObject);
        // coroutine is used to let the particle explosion finish before destroying the game object
        StartCoroutine(Explosion());
    }
    private IEnumerator Explosion()
    {
        explosion.SetActive(true);
        GetComponent<CapsuleCollider>().enabled = false;
        Destroy(fishBody);
        yield return new WaitForSecondsRealtime(1.5f);
        Destroy(gameObject);
    }
    public void IncreaseCount()
    {
        Count++;
    }
}
