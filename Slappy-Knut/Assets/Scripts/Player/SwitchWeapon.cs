using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    private void Start()
    {
        Weapon.AllWeapons.AddRange(GetComponentsInChildren<Weapon>());
        foreach (Weapon weapon in Weapon.AllWeapons)
        {
            if (weapon.name != "Hand") weapon.gameObject.SetActive(false);
            else
            {
                Weapon.DefaultWeapon = weapon;
                Weapon.CurrEquippedWeapon = Weapon.DefaultWeapon;
            }
        }
    }
}
