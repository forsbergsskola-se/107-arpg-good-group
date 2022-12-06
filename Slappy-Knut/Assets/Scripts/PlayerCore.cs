using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCore : MonoBehaviour
{
    public RageBar rageBar;
    public float rageDamage;
    public float maxRage;
    
    private float _minRage;
    private float _currentRage;
    private Animator _animator;
    private AudioSource _audioSource;
    private PlayerMovement _playerMovement;
    void Start()
    {
        _currentRage = _minRage;
        rageBar.SetRageBar(_currentRage);
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
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
        rageBar.SetRageBar(_currentRage);
    }
    void OnMaxRage()
    {
        // _playerMovement.enabled = false;
        _animator.SetBool("isRaging", true);
        Invoke("SetDeactive", 1f);
        Invoke("LoadScene", 2f);
    }
    void LoadScene()
    {
        SceneManager.LoadScene("TestPlayer");
    }
    void SetDeactive()
    {
        gameObject.SetActive(false);
    }
}
