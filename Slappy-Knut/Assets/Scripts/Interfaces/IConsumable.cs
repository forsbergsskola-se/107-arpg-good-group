using Interfaces;
using UnityEngine;

public interface IConsumable : IItem
{
    public static int Count { get; set; }

    public void IncreaseCount(){}

    public void Use(){}
}
