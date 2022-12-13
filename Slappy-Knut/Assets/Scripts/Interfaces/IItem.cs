namespace Interfaces
{
    public interface IItem
    {
        float Power { get; set; }
        string Description { get; set; }
        float Cooldown { get; set; }
        float Range { get; set; }
        
        bool Equipable { get; }
        bool Chargable { get; }

        void Use() {}
        void Charge() {}
    }
}