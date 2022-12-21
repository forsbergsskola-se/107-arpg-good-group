using UnityEngine.UI;

public class AntiAnxietyPotion : Interactable, IConsumable
{
    public float power = 10;
    public Image icon;
    
    public string Name { get; set; }
    public Image Icon { get; set; }
    public float Power { get; set; }
    public string Description { get; set; }
    public float Cooldown { get; set; }
    public float Range { get; set; }
    public bool Equipable { get; set; }
    public bool Chargable { get; set; }
    public static int Count { get; set; }
    
    private PlayerAudioManager _audioManager;
    // public IConsumable consumable;

    private void Start()
    {
        Icon = icon;
        Chargable = false;
        Power = power;
        Description = $"Potion that lowers rage by {power}";
        Cooldown = 10;
        Range = 0;
        _audioManager = GetComponent<PlayerAudioManager>();
        // consumable = GetComponent<IConsumable>();
    }
    protected override void Interact()
    {
        IncreaseCount();
        Destroy(gameObject);
    }
    public void Use()
    {
        if (Count < 1) return;
        _audioManager.AS_DrinkPotion.Play();
        // lowers player's current rage by power
        GetComponent<PlayerRage>().TakeDamage(-power, null);
        Count--;
    }

    public void IncreaseCount()
    {
        Count++;
    }
}
