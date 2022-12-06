using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    public float rageDamage;
    public RageBar rageBar;
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
    }
    void TakeDamage(float rage)
    {
        _currentRage += rage * Time.deltaTime;
        rageBar.SetRageBar(_currentRage);
    }
}
