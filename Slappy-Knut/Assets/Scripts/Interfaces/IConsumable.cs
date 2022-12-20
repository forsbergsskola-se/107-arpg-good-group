using Interfaces;

public interface IConsumable : IItem
{
    public int Count { get; set; }
    public void Use(){}
}
