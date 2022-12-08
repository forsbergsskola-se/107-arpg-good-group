using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract bool Chargeable { get; }
    public abstract float Power { get; }
    public abstract string Description { get; }
    public abstract float Cooldown { get; }
    public abstract float Range { get; }

    public abstract void Use(GameObject player);
    public abstract void Charge();
}