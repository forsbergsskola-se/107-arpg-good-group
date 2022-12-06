using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCore : MonoBehaviour
{
    public float rageDamage;
    public RageBar rageBar;
    public float maxRage;
    private float _minRage;
    private float _currentRage;
    void Start()
    {
        _currentRage = _minRage;
        rageBar.SetRageBar(_currentRage);
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
        gameObject.SetActive(false);
        Invoke("LoadScene", 2f);
    }
    void LoadScene()
    {
        SceneManager.LoadScene("TestPlayer");
    }
}
