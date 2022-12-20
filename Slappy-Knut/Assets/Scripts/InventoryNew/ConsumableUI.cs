using UnityEngine;

public class ConsumableUI : MonoBehaviour
{
    private IConsumable[] _consumables;
    private AntiAnxietyPotion antiAnxiety;
    private FishLandmine fishLandmine;
    
    enum Consumable {
        AntiAnxietyPotion = 0,
        FishLandmine = 1
    }
    private void Start()
    {
        antiAnxiety = GetComponent<AntiAnxietyPotion>();
        _consumables[(int)Consumable.AntiAnxietyPotion] = antiAnxiety;
        
        fishLandmine = GetComponent<FishLandmine>();
        _consumables[(int)Consumable.FishLandmine] = fishLandmine;
    }

    public void Add(IConsumable consumable)
    {
        consumable.Count += 1;
        if (consumable.Count == 1) consumable.Icon.color = Color.white;
    }

    public void Use(IConsumable consumable)
    {
        consumable.Count -= 1;
        if (consumable.Count == 0) consumable.Icon.color = Color.white;
    }
    

    
}