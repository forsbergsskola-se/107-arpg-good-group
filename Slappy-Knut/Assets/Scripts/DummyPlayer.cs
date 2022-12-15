using System;
using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
    public float currentRage;
    private AntiAnxietyPotion antiAnxietyPotion;
    private PauseRagePotion pauseRagePotion;
    public FishLandmine fishLandmine;

    private void Start()
    {
        antiAnxietyPotion = gameObject.AddComponent<AntiAnxietyPotion>();
        pauseRagePotion = gameObject.AddComponent<PauseRagePotion>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            antiAnxietyPotion.Use();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            pauseRagePotion.Use();
        }
        
        if (Input.GetKeyDown(KeyCode.H))
        {
            Transform t = transform;
            Vector3 p = t.transform.position;
            Vector3 spawnOffset = new Vector3(p.x, p.y + 0.2f, p.z);
            Instantiate(fishLandmine, spawnOffset, t.rotation);
        }
    }

    private void FixedUpdate()
    {
        currentRage += 0.001f;
    }
}
