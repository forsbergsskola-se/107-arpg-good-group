using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [HideInInspector] public AudioSource AS_FootSteps;
    [HideInInspector] public AudioSource AS_RageFart;
    [HideInInspector] public AudioSource AS_BasicSlap;
    [HideInInspector] public AudioSource AS_DrinkPotion;
    
    [SerializeField] AudioClip footSteps;
    [SerializeField] AudioClip rageFart;
    [SerializeField] AudioClip basicSlap;
    [SerializeField] AudioClip drinkPotion;
    private void Start()
    {
        AS_FootSteps = gameObject.AddComponent<AudioSource>();
        AS_RageFart = gameObject.AddComponent<AudioSource>();
        AS_BasicSlap = gameObject.AddComponent<AudioSource>();
        AS_DrinkPotion = gameObject.AddComponent<AudioSource>();
        
        AS_FootSteps.clip = footSteps;
        AS_RageFart.clip = rageFart;
        AS_BasicSlap.clip = basicSlap;
        AS_DrinkPotion.clip = drinkPotion;
    }
}
