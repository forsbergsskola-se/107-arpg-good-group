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
        // saves player's current rage at the moment they use the potion
        _rage = _player.currentRage;
        // the logic only works when it's in update, so _used makes sure the logic is
        // only run when the potion has been used
        _used = true;
        _audioSource.Play();
    }
    
    private void Update()
    {
        if (!_used) return; // returns when potion has not been used
        if (_timeSinceUse < power) // power is the amount of seconds rage increase pauses for
        {
            // this could probably be made better by moving more logic into the coroutine, but this works
            StartCoroutine(Wait());
            _player.currentRage = _rage;
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
        // the item can not be charged
        throw new System.NotImplementedException();
    }
}
