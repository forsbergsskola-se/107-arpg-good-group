using UnityEngine;

public class NPCAudioManager : MonoBehaviour
{
    [HideInInspector] public AudioSource AS_FootSteps;
    [HideInInspector] public AudioSource AS_Damage;
    [HideInInspector] public AudioSource AS_Hit;
    
    [SerializeField] AudioClip footSteps;
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip damage;
    private void Start()
    {
        AS_FootSteps = gameObject.AddComponent<AudioSource>();
        AS_FootSteps.clip = footSteps;
        AS_FootSteps.spatialBlend = 1;
        AS_FootSteps.rolloffMode = AudioRolloffMode.Linear;
        AS_FootSteps.maxDistance = 35;
        AS_FootSteps.volume = .5f;
        

        AS_Damage = gameObject.AddComponent<AudioSource>();
        AS_Damage.clip = damage;

        AS_Hit = gameObject.AddComponent<AudioSource>();
        AS_Hit.clip = hit;
    }
    
}
