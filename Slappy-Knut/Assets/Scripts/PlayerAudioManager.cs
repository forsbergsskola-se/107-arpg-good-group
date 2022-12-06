using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [HideInInspector] public AudioSource AS_FootSteps;
    // public AudioSource rageSounds;
    [HideInInspector] public AudioSource AS_RageFart;
    [SerializeField] AudioClip footSteps;
    [SerializeField] AudioClip rageFart;
    
    private void Start()
    {
        AS_FootSteps = gameObject.AddComponent<AudioSource>();
        AS_RageFart = gameObject.AddComponent<AudioSource>();
        AS_FootSteps.clip = footSteps;
        AS_RageFart.clip = rageFart;
    }
}
