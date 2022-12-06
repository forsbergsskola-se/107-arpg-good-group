using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            FindObjectOfType<ChickBoss>().StartBossFight();
    }
}
