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
    private Animator _animator;
    private PlayerAudioManager _audioManager;
    private PlayerMovement _playerMovement;
    void Start()
    {
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
        _animator.SetBool("isRaging", true);
        Invoke("SetDeactive",2f);
        Invoke("LoadScene", 4f);
    }
    void LoadScene()
    {
        SceneManager.LoadScene("TestPlayer");
    }
    void SetDeactive()
    {
        _animator.SetBool("isRaging", false);
        _audioManager.AS_RageFart.Play();
        fartDust.Play();
        bodyMesh.enabled = false;
        hairMesh.enabled = false;
    }
}
