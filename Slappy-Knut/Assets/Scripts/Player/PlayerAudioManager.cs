using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [HideInInspector] public AudioSource AS_FootSteps;
    [HideInInspector] public AudioSource AS_RageFart;
    [HideInInspector] public AudioSource AS_RageSound;
    [HideInInspector] public AudioSource AS_BasicSlap;
    
    [SerializeField] AudioClip footSteps;
    [SerializeField] AudioClip rageFart;
    [SerializeField] AudioClip rageScream;
    [SerializeField] private AudioClip basicSlap;
    private void Start()
    {
        AS_FootSteps = gameObject.AddComponent<AudioSource>();
        AS_RageFart = gameObject.AddComponent<AudioSource>();
        AS_RageSound = gameObject.AddComponent<AudioSource>();
        AS_BasicSlap = gameObject.AddComponent<AudioSource>();
        
        AS_FootSteps.clip = footSteps;
        AS_RageFart.clip = rageFart;
        AS_RageSound.clip = rageScream;
        AS_BasicSlap.clip = basicSlap;
    }
}
