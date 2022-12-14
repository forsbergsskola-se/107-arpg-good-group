using UnityEngine;
using UnityEngine.UI;

public class PlayerSatisfaction : MonoBehaviour
{
    //satis = Satisfaction
    public Slider satisfactionBar;
    public float requiredSatis = 100;

    private float _currentSatis;
    void Update()
    {
        // Debug.Log(_currentSatis);
        satisfactionBar.value = _currentSatis / requiredSatis;
    }
    public void AddSatisfaction(float damage)
    {
        _currentSatis += damage;
        if (_currentSatis >= requiredSatis)
        {
            //TODO: level up, set requiredSatis to a higher value, reset the _currentSatis
        }
    }
}
