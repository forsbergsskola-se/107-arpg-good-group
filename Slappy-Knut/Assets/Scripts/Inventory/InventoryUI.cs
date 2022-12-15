using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent; //Reference the InventoryParent in Unity
    public GameObject inventoryUI;
    
    private Inventory _inventory;
    private InventorySlot[] _slots;
   
    // Start is called before the first frame update
    void Start()
    {
        _inventory = Inventory.Instance;
        _inventory.OnItemChangedCallback += UpdateUI;

        _slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {
        Debug.Log("UPDATING UI");

        for (int i = 0; i < _slots.Length; i++)
        {
            if (i < _inventory.items.Count)
            {
                _slots[i].AddItem(_inventory.items[i]);
            }
            else
            {
                _slots[i].ClearSlot();
            }
        }
    }
}