using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [HideInInspector] public AudioSource AS_FootSteps;
    [HideInInspector] public AudioSource AS_RageFart;
    [HideInInspector] public AudioSource AS_RageSound;
    
    [SerializeField] AudioClip footSteps;
    [SerializeField] AudioClip rageFart;
    [SerializeField] AudioClip rageScream;
    private void Start()
    {
        AS_FootSteps = gameObject.AddComponent<AudioSource>();
        AS_RageFart = gameObject.AddComponent<AudioSource>();
        AS_RageSound = gameObject.AddComponent<AudioSource>();
        AS_FootSteps.clip = footSteps;
        AS_RageFart.clip = rageFart;
        AS_RageSound.clip = rageScream;
    }
}
