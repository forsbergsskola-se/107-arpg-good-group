using System;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerRage : MonoBehaviour, IDamagable
{
    public Slider rageBar;
    public float rageDOT;
    public SkinnedMeshRenderer bodyMesh;
    public SkinnedMeshRenderer hairMesh;
    public ParticleSystem fartDust;
    public float DefenseRating { get; set; }
    
    private float _maxRage;
    private float _currentRage;
    private Scene _scene;
    private Animator _animator;
    private PlayerAudioManager _audioManager;
    private PlayerMovement _playerMovement;
    
    void Start()
    {
        _scene = SceneManager.GetActiveScene();
        _animator = GetComponent<Animator>();
        _audioManager = GetComponent<PlayerAudioManager>();
        _playerMovement = GetComponent<PlayerMovement>();
    }
    void Update()
    { 
        _currentRage += rageDOT * Time.deltaTime;
        rageBar.value = _currentRage;
        if(Math.Abs(_currentRage - _maxRage) < .01) OnDeath(); //checks if maxRage reached 
                                                                //it's actually not expensive (not called every frame)
    }

    public void OnDeath()
    {
        _playerMovement.enabled = false;
        _audioManager.AS_RageSound.Play();
        Invoke("PlayRageAnimation",.5f);
        Invoke("SetInactive",2f);
        Invoke("LoadScene", 5f);
    }

    //methods to use with invoke (methods to be used with invoke can not have any parameters)
    void PlayRageAnimation()
    {
        _animator.SetBool("isRaging", true);
    }
    void LoadScene()
    {
        SceneManager.LoadScene(_scene.name);
    }
    void SetInactive()
    {
        _audioManager.AS_RageFart.Play();
        fartDust.Play();
        bodyMesh.enabled = false;
        hairMesh.enabled = false;
    }
    public void TakeDamage(float damage, GameObject attacker)
    {
        _currentRage += damage;
        rageBar.value = _currentRage;
    }
}
