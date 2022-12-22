using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableInventory : MonoBehaviour
{
    private AntiAnxietyPotion _antiAnxietyPotion;
    private FishLandmine _fishLandmine;
    
    public GameObject antiAnxietySlot;
    public Image antiAnxietyIcon;
    public TextMeshProUGUI antiAnxietyCountText;
    public GameObject fishLandmineSlot;
    public Image fishLandmineIcon;
    public TextMeshProUGUI fishLandmineCountText;
    
    private void Start()
    {
        _antiAnxietyPotion = gameObject.AddComponent<AntiAnxietyPotion>();
        _fishLandmine = gameObject.AddComponent<FishLandmine>();
        SetColorDisabled(antiAnxietyIcon);
        SetColorDisabled(fishLandmineIcon);
    }

    private void Update()
    {
        if (AntiAnxietyPotion.Count > 0) SetColorEnabled(antiAnxietyIcon);
        if (AntiAnxietyPotion.Count == 0) SetColorDisabled(antiAnxietyIcon);
        if (FishLandmine.Count > 0) SetColorEnabled(fishLandmineIcon);
        if (FishLandmine.Count == 0) SetColorDisabled(fishLandmineIcon);
        fishLandmineCountText.text = $"{FishLandmine.Count}";
    }

    void SetColorDisabled(Image icon)
    {
        icon.color = Color.gray;
    }
    void SetColorEnabled(Image icon)
    {
        icon.color = Color.white;
    }
}
