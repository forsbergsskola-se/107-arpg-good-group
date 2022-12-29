
using Interfaces;
using UnityEngine;

public abstract class Pet : Interactable, IItem
{
    
    public abstract string Name { get; set; }
    public abstract Sprite Icon { get; set; }
    public abstract float Power { get; set; }
    public abstract float Range { get; set; }
    public abstract string Description { get; set; }
    public abstract float Cooldown { get; set; }
    
    public abstract bool IsEquipped { get; set; }

    protected abstract void Start();
    
}
