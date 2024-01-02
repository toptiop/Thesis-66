using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemObject : MonoBehaviour, IInteractable
{
    [Header("Interacton Text")]
    public string actionText;

    [Space]
    [Header("Item Detail")]
    public SO_Item item;
    public int amont = 1;
    public TMP_Text amountText;
    [SerializeField]
    private Inventory inventory;

    public void SetAmout(int newAmont)
    {
        amont = newAmont;
        amountText.text = amont.ToString();
    }    
    public void RandomAmout()
    {
        amont = Random.Range(1, item.maxStack + 1);
        amountText.text = amont.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inventory = other.GetComponent<ItemPicker>().inventory;
            //Destroy(gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inventory = null;
            
        }
    }

    void PickupItem()
    {
        inventory.AddItem(item, amont);
        Destroy(gameObject);
    }

    public string GetInteractionText()
    {
        return actionText;
    }

    public void Interact()
    {
        if(inventory != null)
        {
            PickupItem();
        }
    }
}
