using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCore : MonoBehaviour
{
    public Slider rageBar;
    public float rageDamage;
    public float maxRage;
    public SkinnedMeshRenderer bodyMesh;
    public SkinnedMeshRenderer hairMesh;
    public ParticleSystem fartDust;
    
    private float _minRage;
    private float _currentRage;
    private Scene _scene;
    private Animator _animator;
    private PlayerAudioManager _audioManager;
    private PlayerMovement _playerMovement;
    void Start()
    {
        _scene = SceneManager.GetActiveScene();
        _currentRage = _minRage;
        rageBar.value = _currentRage;
        
        _animator = GetComponent<Animator>();
        _audioManager = GetComponent<PlayerAudioManager>();
        _playerMovement = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        TakeDamage(rageDamage);
        if(Math.Abs(_currentRage - maxRage) < .01) OnMaxRage();
    }
    void TakeDamage(float rage)
    {
        _currentRage += rage * Time.deltaTime;
        rageBar.value = _currentRage;
    }
    void OnMaxRage()
    {
        _playerMovement.enabled = false;
        _audioManager.AS_RageSound.Play();
        Invoke("PlayRageAnimation",.5f);
        Invoke("SetDeactive",2f);
        Invoke("LoadScene", 5f);
    }

    void PlayRageAnimation()
    {
        _animator.SetBool("isRaging", true);
    }
    void LoadScene()
    {
        SceneManager.LoadScene(_scene.name);
    }
    void SetDeactive()
    {
        _audioManager.AS_RageFart.Play();
        fartDust.Play();
        bodyMesh.enabled = false;
        hairMesh.enabled = false;
    }
}
