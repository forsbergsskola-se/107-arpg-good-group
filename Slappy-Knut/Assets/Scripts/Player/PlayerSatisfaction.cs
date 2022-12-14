using UnityEngine;
using UnityEngine.UI;

public class PlayerSatisfaction : MonoBehaviour
{
    //satis = Satisfaction
    public Slider satisfactionBar;
    public float requiredSatis = 1;

    private float _currentSatis;
    void Update()
    {
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
