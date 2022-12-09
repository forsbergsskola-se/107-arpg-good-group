using System;
using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
    public float currentRage;
    public AntiAnxietyPotion antiAnxietyPotion;
    public PauseRagePotion pauseRagePotion;
    
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
    }
}
