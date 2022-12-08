using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
  private Item item;

  public Button RemoveButton;

  public void RemoveItem()
  {
    InventoryManagement.Instance.Remove(item);
    
    Destroy(gameObject);
  }

  public void AddItem(Item newItem)
  {
    item = newItem;
  }
}
