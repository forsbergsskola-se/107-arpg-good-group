using UnityEngine;

public class AntiAnxietyPotion : Interactable, IConsumable
{
    public float power = 10;
    public Sprite icon;
    
    public string Name { get; set; }
    public Sprite Icon { get; set; }
    public float Power { get; set; }
    public string Description { get; set; }
    public float Cooldown { get; set; }
    public float Range { get; set; }
    public static int Count { get; set; }
    
    private PlayerAudioManager _audioManager;

    private void Start()
    {
        Icon = icon;
        Power = power;
        Description = $"Potion that lowers rage by {power}";
        Cooldown = 10;
        Range = 0;
        _audioManager = GetComponent<PlayerAudioManager>();
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
