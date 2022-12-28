using UnityEngine;

public class NPCAudioManager : MonoBehaviour
{
    [HideInInspector] public AudioSource AS_FootSteps;
    
    [SerializeField] AudioClip footSteps;
    [HideInInspector] public AudioSource AS_Damage;
    [HideInInspector] public AudioSource AS_Hit;
    [SerializeField] AudioClip hit;
    
    [SerializeField] AudioClip damage;
    private void Start()
    {
        AS_FootSteps = gameObject.AddComponent<AudioSource>();
        AS_FootSteps.clip = footSteps;
        AS_FootSteps.spatialBlend = 1;

        AS_Damage = gameObject.AddComponent<AudioSource>();
        AS_Damage.clip = damage;
        AS_Damage.spatialBlend = 1;

        AS_Hit = gameObject.AddComponent<AudioSource>();
        AS_Hit.clip = hit;
        AS_Hit.spatialBlend = 1;
    }
    
}
