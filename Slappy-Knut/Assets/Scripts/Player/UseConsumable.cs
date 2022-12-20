using UnityEngine;

public class UseConsumable : MonoBehaviour
{
    private AntiAnxietyPotion antiAnxietyPotion;
    public FishLandmine fishLandmine;
    private PlayerAudioManager _audioManager;
    private void Start()
    {
        antiAnxietyPotion = gameObject.AddComponent<AntiAnxietyPotion>();
        _audioManager = GetComponent<PlayerAudioManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            antiAnxietyPotion.Use();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            fishLandmine.Use();
        }
    }
}
