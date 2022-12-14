using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAudioManager : MonoBehaviour
{
    [HideInInspector] public AudioSource AS_FootSteps;
    
    [SerializeField] AudioClip footSteps;
    [HideInInspector] public AudioSource AS_Damage;
    
    [SerializeField] AudioClip damage;
    private void Start()
    {
        AS_FootSteps = gameObject.AddComponent<AudioSource>();
        AS_FootSteps.clip = footSteps;
    }
    
}
