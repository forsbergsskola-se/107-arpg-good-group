using UnityEngine;

public class OgreAudioManager : MonoBehaviour
{
    [HideInInspector] public AudioSource AS_FootSteps;
    [HideInInspector] public AudioSource AS_Death;
    [HideInInspector] public AudioSource AS_GetHit;
    
    [SerializeField] AudioClip footSteps;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip getHit;
    private void Start()
    {
        AS_FootSteps = gameObject.AddComponent<AudioSource>();
        AS_FootSteps.volume = 0.01f;
        AS_Death  = gameObject.AddComponent<AudioSource>();
        AS_Death.volume = 0.4f;
        AS_GetHit = gameObject.AddComponent<AudioSource>();
        AS_GetHit.volume = 0.2f;
        AS_FootSteps.clip = footSteps;
        AS_Death.clip = death;
        AS_GetHit.clip = getHit;
    }
}