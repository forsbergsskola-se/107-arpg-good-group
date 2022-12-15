using Interfaces;
using UnityEngine;

public class AntiAnxietyPotion : MonoBehaviour, IItem
{
    public int count = 5;
    [SerializeField] private float power = 10;
    private AudioSource _audioSource;
    
    
    public float Power { get; set; }
    public string Description { get; set; }
    public float Cooldown { get; set; }
    public float Range { get; set; }
    public bool Equipable { get; set; }
    public bool Chargable { get; set; }
    public int Count { get; set; }
    public AntiAnxietyPotion()
    {
        Chargable = false;
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


    public void Use()
    {
        if (count < 1)
            return;
        _audioSource.Play();
        // lowers player's current rage by power
        FindObjectOfType<DummyPlayer>().currentRage -= power;
        count--;
    }

    public void Charge()
    {
        // the item can not be charged
        throw new System.NotImplementedException();
    }
}
