using System.Collections;
using UnityEngine;

public class PauseRagePotion : Consumable
{
    public int count;
    public float power;

    private float _timeSinceUse;
    private AudioSource _audioSource;
    private bool _used;
    private DummyPlayer _player;
    private float _rage;
    
    public override bool Chargeable { get; }
    public override float Power { get; }
    public override string Description { get; }
    public override float Cooldown { get; }
    public override float Range { get; }
    public override int Count { get; set; }

    public PauseRagePotion()
    {
        Chargeable = false;
        Power = power;
        Description = $"Potion that stops rage increase for {power} seconds";
        Cooldown = 30;
        Range = 0;
    }
    
    private void Awake()
    {
        Count = count;
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Use()
    {
        if (count < 1)
            return;
        _timeSinceUse = Time.deltaTime;
        _player = FindObjectOfType<DummyPlayer>();
        _rage = _player.currentRage;
        _used = true;
    }
    
    private void Update()
    {
        if (!_used) return;
        _audioSource.Play();
        if (_timeSinceUse < power)
        {
            StartCoroutine(Wait());
            _player.currentRage = _rage;
            Debug.Log(_timeSinceUse);
            _timeSinceUse += Time.deltaTime;
        }
        else
        {
            count--;
            _used = false;
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(power);
    }

    public override void Charge()
    {
        throw new System.NotImplementedException();
    }
}
