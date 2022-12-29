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
        if (Input.GetKeyDown(KeyCode.Q) && PauseGame.IsPaused == false)
        {
            antiAnxietyPotion.Use();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && PauseGame.IsPaused == false)
        {
            fishLandmine.Use();
        }
    }
}
