public class AttackingFocused : Interactable
{
    private PlayerAttack pAttack;

    private void Start()
    {
        pAttack = FindObjectOfType<PlayerAttack>();
    }

    protected override void Interact()
    {
        pAttack._animator.Play("attack");
    }
}