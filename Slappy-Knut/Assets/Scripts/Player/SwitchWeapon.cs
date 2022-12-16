using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    private void Awake()
    {
        Weapon.AllWeapons.AddRange(GetComponentsInChildren<Weapon>());
        foreach (var weapon in Weapon.AllWeapons)
        {
            if (weapon.name != "Hand") weapon.gameObject.SetActive(false);
            else Weapon.CurrEquippedWeapon = weapon;
        }
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) Weapon.Switch(Weapon.AllWeapons[0]);
        else if (Input.GetKeyDown(KeyCode.T)) Weapon.Switch(Weapon.AllWeapons[1]);
    }
}
