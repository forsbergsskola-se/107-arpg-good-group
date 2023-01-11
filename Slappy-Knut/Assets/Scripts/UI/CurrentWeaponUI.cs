using UnityEngine;
using UnityEngine.UI;

public class CurrentWeaponUI : MonoBehaviour
{
    public Image _weaponIcon;

    public void UpdateIcon(Sprite icon)
    {
        _weaponIcon.sprite = icon;
    }
}
