using Interfaces;
using UnityEngine;

public class AntiAnxietyPotion : MonoBehaviour, IItem
{
    public int count = 5;
    public float power = 10;
    
    
    public float Power { get; set; }
    public string Description { get; set; }
    public float Cooldown { get; set; }
    public float Range { get; set; }
    public bool Equipable { get; set; }
    public bool Chargable { get; set; }
    public int Count { get; set; }
    
    private PlayerAudioManager _audioManager;
    private void Start()
    {
        Count = count;
        Chargable = false;
        Power = power;
        Description = $"Potion that lowers rage by {power}";
        Cooldown = 10;
        Range = 0;
        _audioManager = GetComponent<PlayerAudioManager>();
    }
    
    public void Use()
    {
        if (count < 1) return;
        _audioManager.AS_DrinkPotion.Play();
        // lowers player's current rage by power
        GetComponent<PlayerRage>().TakeDamage(-power, null);
        count--;
    }

    public void Charge() {}
}
