using UnityEngine;
using UnityEngine.UI;

public class RageBar : MonoBehaviour
{
    public Slider slider;

    // public void Start()
    // {
    //     _slider = GetComponent<Slider>();
    // }
    public void SetRageBar(float rage)
    {
        slider.value = rage;
    }
}
