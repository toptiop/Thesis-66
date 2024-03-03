using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadNote : MonoBehaviour, IInteractable
{
    [Header("Interacton Text")]
    public string actionText;

    [Space]
    [Header("Item Detail")]
    public SO_Item itemNote;
    public int amont = 1;
    //public TMP_Text amountText;
    [SerializeField]
    private InventoryNote inventory;

    public GameObject canvasNote;

    public bool isOpen;
    void Start()
    {
        actionText = "Read " + itemNote.itemName;
        inventory = FindObjectOfType<InventoryNote>();
    }

    void OpenNote()
    {
        isOpen = !isOpen;

        if(isOpen)
        {
            inventory.AddItem(itemNote, amont);
            canvasNote.SetActive(true);
            GameManager.Instance.InteractUI = true;
            Singleton.controller.canMove = true;
        }
        else
        {
            Singleton.controller.canMove = false;
            GameManager.Instance.InteractUI = false;
            canvasNote.SetActive(false);
            Destroy(gameObject);
        }
    }

    public string GetInteractionText()
    {
        return actionText;
    }

    public void Interact()
    {
        OpenNote();
    }
}
