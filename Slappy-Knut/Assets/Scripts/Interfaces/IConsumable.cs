using Interfaces;

public interface IConsumable : IItem
{
    public static int Count { get; set; }

    public void Use();
    public void IncreaseCount();
}