using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [HideInInspector] public AudioSource AS_FootSteps;
    [HideInInspector] public AudioSource AS_BasicSlap;
    
    [SerializeField] AudioClip footSteps;
    [SerializeField] AudioClip basicSlap;
    private void Start()
    {
        AS_FootSteps = gameObject.AddComponent<AudioSource>();
        AS_BasicSlap = gameObject.AddComponent<AudioSource>();
        
        AS_FootSteps.clip = footSteps;
        AS_BasicSlap.clip = basicSlap;
    }
}
