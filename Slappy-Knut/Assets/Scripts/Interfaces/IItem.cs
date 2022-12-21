using UnityEngine;

namespace Interfaces
{
    public interface IItem
    {
        string Name { get; set; }
        Sprite Icon { get; set; }
        float Power { get; set; }
        float Range { get; set; }
        string Description { get; set; }
        float Cooldown { get; set; }
    }
}