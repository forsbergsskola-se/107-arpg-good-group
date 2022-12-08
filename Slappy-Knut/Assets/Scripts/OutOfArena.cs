using UnityEngine;

public class OutOfArena : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        player.transform.position = new Vector3(0, 0, 0);
    }
}
