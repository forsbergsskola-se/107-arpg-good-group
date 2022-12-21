public class ConsumablePickup : Interactable
{
    public IConsumable consumable;

    private void Start()
    {
        consumable = GetComponent<IConsumable>();
    }

    protected override void Interact()
    {
        consumable.IncreaseCount();
        Destroy(gameObject);
    }
}