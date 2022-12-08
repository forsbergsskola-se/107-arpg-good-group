using UnityEngine;

public class ChickAudioManager : MonoBehaviour
{
    [HideInInspector] public AudioSource AS_FootSteps;
    [HideInInspector] public AudioSource AS_RageChirp;
    [HideInInspector] public AudioSource AS_AttackChirp;
    
    [SerializeField] AudioClip footSteps;
    [SerializeField] AudioClip rageChirp;
    [SerializeField] AudioClip attackChirp;
    private void Start()
    {
        AS_FootSteps   = gameObject.AddComponent<AudioSource>();
        AS_RageChirp   = gameObject.AddComponent<AudioSource>();
        AS_AttackChirp = gameObject.AddComponent<AudioSource>();
        AS_FootSteps.clip   = footSteps;
        AS_RageChirp.clip   = rageChirp;
        AS_AttackChirp.clip = attackChirp;
    }
}