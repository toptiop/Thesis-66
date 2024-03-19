using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class AudioPickup : MonoBehaviour, IInteractable
{

    [Header("Interacton Text")]
    public string actionText;

    [Space]
    [Header("Item Detail")]
    public SO_Item item;
    public int amont = 1;

    public TMP_Text title;
    //public TMP_Text amountText;
    [SerializeField]
    private InventoryAudio inventory;
    ItemGO ItemGO;
    public GameObject canvasNote;
    public AudioSource video;
    public bool isOpen;

    public AudioSource audio;
    private void Awake()
    {
        ItemGO = GetComponent<ItemGO>();
        inventory = FindAnyObjectByType<InventoryAudio>();

        title.text = item.noteTitle;
        video.clip = item.audio;
    }


    public void OpenAudio()
    {
        isOpen = !isOpen;
        audio.PlayOneShot(item.pickupEFX);
        if (isOpen)
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
    }

    public string GetInteractionText()
    {
        return actionText;
    }

    public void Interact()
    {
        OpenAudio();
    }

}
