using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public bool chargeable;
    public float power;
    public string description;
    public float cooldown;
    public float range;
    
    public void Use() {}
    public void Charge() {}
}