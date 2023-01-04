using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    private bool diedWithWeapon;
    private void Start()
    {
        diedWithWeapon = Inventory.EquippedSlot != null;
        Weapon.AllWeapons.Clear();
        Weapon.AllWeapons.AddRange(GetComponentsInChildren<Weapon>());
        
        foreach (Weapon weapon in Weapon.AllWeapons)
        {
            weapon.gameObject.SetActive(false);
            if (weapon.Name == "Hand") Weapon.DefaultWeapon = weapon;
            if (diedWithWeapon && Inventory.EquippedSlot.itemName == weapon.Name) Weapon.Switch(weapon.Name);
        }
        if (!diedWithWeapon) Weapon.Switch(Weapon.DefaultWeapon.Name);
    }
}
