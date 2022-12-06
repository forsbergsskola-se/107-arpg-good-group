using UnityEngine;
using UnityEngine.UI;

public class RageBar : MonoBehaviour
{
    public Slider slider;
    public void SetRageBar(float rage)
    {
        slider.value = rage;
    }
}
