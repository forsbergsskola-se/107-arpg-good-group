using System.Collections;
using Interfaces;
using Unity.Mathematics;
using UnityEngine;

public class FishLandmine : Interactable, IConsumable
{
    public float power;
    public ParticleSystem explosion;
    public GameObject fishBody;

    private PlayerLevelLogic _playerLevelLogic;
    private PlayerRage _playerRage;

    public string Type { get; }
    public string Name { get; set; }
    public Sprite Icon { get; set; }
    public float Power { get; set; }
    public string Description { get; set; }
    public float Cooldown { get; set; }
    public GameObject Prefab { get; set; }
    public float Range { get; set; }


    private void Start()
    {
        Power = power;
        Description = "Fish that causes damage when you get too close.";
        Cooldown = 20;
        Range = 10;

        GameObject player = GameObject.FindGameObjectWithTag("Player"); //THIS CODE IS FRAGILE
        _playerLevelLogic = player.GetComponent<PlayerLevelLogic>();//We should probaby come up with something better for this
        _playerRage = player.GetComponent<PlayerRage>();
    }
    private void OnTriggerEnter(Collider other) // this is supposed to be DoT maybe?
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        // only non-player IDamagables should set it off
        if (damagable == null || other.gameObject.CompareTag("Player")) return;
        
        damagable.TakeDamage(Power, gameObject);
        _playerLevelLogic.IncreaseXP(power);
        _playerRage.TakeDamage(power * -1, gameObject);
        // coroutine is used to let the particle explosion finish before destroying the game object
        StartCoroutine(Explosion());
    }
    private IEnumerator Explosion()
    {
        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
        // explosion.SetActive(true);
        GetComponent<CapsuleCollider>().enabled = false;
        Destroy(fishBody);
        yield return new WaitForSecondsRealtime(1.5f);
        Destroy(gameObject);
    }
    public void IncreaseCount()
    {
        FindObjectOfType<FishLandmineSpawner>().Add();
    }
}
