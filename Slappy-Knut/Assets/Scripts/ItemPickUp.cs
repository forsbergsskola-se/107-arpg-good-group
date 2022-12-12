
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item Item;
    public GameObject player;

    public void PickUp()
    {
        InventoryManagement.Instance.Add(Item);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        PickUp();
    }

    /*private void Update()
    {
        if (Vector3.Distance(player.transform.position, item.transform.position)<1.5f)
        {
           PickUp(); 
        }
        
    }*/
}

