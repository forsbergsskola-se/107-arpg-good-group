using UnityEngine;
using UnityEngine.UI;

public class PlayerSatisfaction : MonoBehaviour
{
    //satis = Satisfaction
    public Slider satisfactionBar;

    private float maxSatis;
    private float _currentSatis;
    void Update()
    {
        satisfactionBar.value = _currentSatis;
    }
    public void AddSatisfaction(float damage)
    {
        _currentSatis += damage;
    }
}
