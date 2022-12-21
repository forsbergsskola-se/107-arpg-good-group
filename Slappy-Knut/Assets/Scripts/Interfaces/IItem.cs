using UnityEngine.UI;

namespace Interfaces
{
    public interface IItem
    {
        string Name { get; set; }
        Image Icon { get; set; }
        float Power { get; set; }
        float Range { get; set; }
        string Description { get; set; }
        float Cooldown { get; set; }
    }
}