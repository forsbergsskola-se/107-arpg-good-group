using UnityEngine;

public class AntiAnxietyPotion : Consumable
{
    public int count;
    [SerializeField] private float power;
    private AudioSource _audioSource;
    
    public override bool Chargeable { get; }
    public override float Power { get; }
    public override string Description { get; }
    public override float Cooldown { get; }
    public override float Range { get; }
    public override int Count { get; set; }

    public AntiAnxietyPotion()
    {
        Chargeable = false;
        Power = power;
        Description = $"Potion that lowers rage by {power}";
        Cooldown = 10;
        Range = 0;
    }

    private void Awake()
    {
        Count = count;
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Use(GameObject player)
    {
        _audioSource.Play();
        FindObjectOfType<DummyPlayer>().currentRage -= power;
        count--;
    }

    public override void Charge()
    {
        throw new System.NotImplementedException();
    }
}
