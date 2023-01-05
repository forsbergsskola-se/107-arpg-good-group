using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(FindObjectOfType<ChickBoss>() != null)
                FindObjectOfType<ChickBoss>().StartBossFight();
            if(FindObjectOfType<Combrat>() != null)
                FindObjectOfType<Combrat>().StartBossFight();
            Destroy(gameObject);
        }
    }
}
