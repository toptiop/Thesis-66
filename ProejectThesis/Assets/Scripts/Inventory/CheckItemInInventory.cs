using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItemInInventory : MonoBehaviour, IInteractable
{
    public SO_Item item;
    public int itemAmont = 1;
    public GameObject activeObject;

    public Inventory inventory;

    public string GetInteractionText()
    {
        return "Disable";
    }

    public void Interact()
    {
        CheckItem();
    }

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    void CheckItem()
    {
        Debug.Log("0");
        if(inventory != null) 
        {
            Debug.Log("1");
            if (inventory.CheckInventoryForItems(item, itemAmont))
            {
                activeObject.SetActive(false);
                Debug.Log("2");
                Destroy(this);
            }
        }
    }
}
