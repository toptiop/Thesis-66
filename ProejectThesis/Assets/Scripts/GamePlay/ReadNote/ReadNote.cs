using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadNote : MonoBehaviour, IInteractable
{
    [Header("Interacton Text")]
    public string actionText;

    [Space]
    [Header("Item Detail")]
    public SO_Item item;
    public int amont = 1;
    //public TMP_Text amountText;
    [SerializeField]
    private InventoryNote inventory;
    ItemGO ItemGO;
    public GameObject canvasNote;

    public bool isOpen;

    public AudioSource audio;

    private void Awake()
    {
        ItemGO = GetComponent<ItemGO>();
    }
    void Start()
    {
        actionText = "Read " + item.itemName;
        inventory = FindObjectOfType<InventoryNote>();
    }

    void OpenNote()
    {
        isOpen = !isOpen;
        audio.PlayOneShot(item.pickupEFX);
        if(isOpen)
        {
            inventory.AddItem(item, amont);
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

        if(ItemGO != null)
        {
            ItemGO.UpdateQuestProgress(item);
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
