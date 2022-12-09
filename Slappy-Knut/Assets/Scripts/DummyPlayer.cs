using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
    public float currentRage;
    public AntiAnxietyPotion antiAnxietyPotion;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            antiAnxietyPotion.Use(gameObject);
        }
    }
}
