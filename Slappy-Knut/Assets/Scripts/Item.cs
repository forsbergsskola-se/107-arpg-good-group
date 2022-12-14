using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract bool Chargeable { get; set; }
    public abstract float Power { get; set; }
    public abstract string Description { get; set; }
    public abstract float Cooldown { get; set; }
    public abstract float Range { get; set; }

    public abstract void Use();
    public abstract void Charge();
}