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
            Transform t = transform;
            Vector3 p = t.transform.position;
            Vector3 spawnOffset = new Vector3(p.x, p.y + 0.2f, p.z);
            Instantiate(fishLandmine, spawnOffset, t.rotation);
        }
    }
}
