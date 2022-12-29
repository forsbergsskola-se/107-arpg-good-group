using System;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerRage : MonoBehaviour, IDamagable
{
    public Slider rageBar;
    public float maxRage = 1;
    public float rageDOT;
    public SkinnedMeshRenderer bodyMesh;
    public SkinnedMeshRenderer hairMesh;
    public ParticleSystem fartCloud;
    public float DefenseRating { get; set; }

    public static float CurrentRage;
   
    
    private Scene _scene;
    private Animator _animator;
    private PlayerAudioManager _audioManager;
    private PlayerController _playerMovement;
    
    void Start()
    {
        CurrentRage = 0;
        GameObject rageSlider = GameObject.Find("RageBar");
        rageBar = rageSlider.GetComponent<Slider>();
        _animator = GetComponent<Animator>();
        _audioManager = GetComponent<PlayerAudioManager>();
        _playerMovement = GetComponent<PlayerController>();
    }
    void Update()
    { 
        CurrentRage += rageDOT * Time.deltaTime;
        rageBar.value = CurrentRage;
        if(CurrentRage > maxRage) OnDeath(); //checks if maxRage reached 
                                        //it's actually not expensive (not called every frame)
    }
    public void OnDeath()
    {
        _playerMovement.enabled = false;
        _animator.Play("die");
        Invoke("LoadScene", 3f);
    }
    void LoadScene() //invoke requires a parameterless function
    {
        Weapon.AllWeapons.Clear();
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void SetInactive() //called on the animator
    {
        _audioManager.AS_RageFart.Play();
        fartCloud.Play();
        bodyMesh.enabled = false;
        hairMesh.enabled = false;
    }
    public void TakeDamage(float damage, GameObject attacker)
    {
        CurrentRage += damage;
        if (CurrentRage < 0) CurrentRage = 0; //clamps the rage bar
        rageBar.value = CurrentRage;
    }

    public void IncreaseStats(float rage, float defenceRating)
    {
        maxRage *= rage;
        DefenseRating *= defenceRating;
    }
    
}
