using UnityEngine;

public class CombratAudioManager : MonoBehaviour
{
    [HideInInspector] public AudioSource AS_Throw;
    [HideInInspector] public AudioSource AS_Cry;
    [HideInInspector] public AudioSource AS_Scream;
    
    [SerializeField] AudioClip throwing;
    [SerializeField] AudioClip crying;
    [SerializeField] AudioClip screaming;
    private void Start()
    {
        AS_Throw = gameObject.AddComponent<AudioSource>();
        AS_Throw.volume = 0.05f;
        AS_Cry  = gameObject.AddComponent<AudioSource>();
        AS_Cry.volume = 0.2f;
        AS_Scream = gameObject.AddComponent<AudioSource>();
        AS_Scream.volume = 0.2f;
        AS_Throw.clip = throwing;
        AS_Cry.clip = crying;
        AS_Scream.clip = screaming;
    }
}