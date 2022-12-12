
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item Item;
    public GameObject player;

   //Pickup gets access to the inventory
   private void PickUp()
    {
        InventoryManagement.Instance.Add(Item);
        Destroy(gameObject);
    }

   //click with mouse to store the item to the inventory 
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

