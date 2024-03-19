using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System.IO;

public class InventoryAudio : MonoBehaviour
{
    [Header("Inventory")]
    public SO_Item EMPTY_ITEM;
    public Transform slotPrefab;
    public Transform inventoryPanel;
    protected GridLayoutGroup gridLayoutGroup;

    [Header("Mini Canvas")]
    public RectTransform miniCanvas;
    [SerializeField] protected InventorySlotAudio rightClickSlot;
    [SerializeField] protected InventorySlotAudio leftClickSlot;

    [Space(5)]
    public int SlotAmount = 30;
    public InventorySlotAudio[] inventorySlots;

    [Header("InventoryUI")]
    public GameObject inventoryUI;
    public GameObject panelButton;
    public Button openNote;

    [Header("Note")]
    public GameObject panelNote;
    public TMP_Text title;
    public AudioSource audioSource;


    void Start()
    {
        gridLayoutGroup = inventoryPanel.GetComponent<GridLayoutGroup>();
        CreateInventorySlolt();

    }

    #region Inventory Methods
    public void AddItem(SO_Item item, int amount)
    {
        InventorySlotAudio slot = IsEmptySlotLeft(item);
        if (slot == null)
        {
            //Full
            DropItem(item, amount);
            return;
        }

        slot.MerageThisSlot(item, amount);
    }
    public void UseItem() //OnClick Event
    {
        //rightClickSlot.UseItem();
        //OnFinishMiniCanvas();
    }
    public void DropItem() //OnClick Event
    {
        ItemSpawner.Instance.SpawnItem(rightClickSlot.item, rightClickSlot.stack);
        DestroyItem();
    }
    public void DropItem(SO_Item item, int amount)
    {
        ItemSpawner.Instance.SpawnItem(item, amount);
    }
    public void DestroyItem()
    {
        rightClickSlot.SetThisSlot(EMPTY_ITEM, 0);
        OnFinishMiniCanvas();
    }//OnClick Event
    public void RemoveItem(InventorySlotAudio slot)
    {
        slot.SetThisSlot(EMPTY_ITEM, 0);
    }
    public void SortItem(bool Ascending = true)
    {
        //Sorting Item
        SetLayoutControlChild(true);
        List<InventorySlotAudio> slotHaveItem = new List<InventorySlotAudio>();
        foreach (InventorySlotAudio slot in inventorySlots)
        {
            if (slot.item != EMPTY_ITEM)
                slotHaveItem.Add(slot);
        }

        var sortArray = Ascending ?
            slotHaveItem.OrderBy(Slot => Slot.item.id).ToArray() :
            slotHaveItem.OrderByDescending(Slot => Slot.item.id).ToArray();

        foreach (InventorySlotAudio slot in inventorySlots)
        {
            Destroy(slot.gameObject);
        }

        CreateInventorySlolt();

        foreach (InventorySlotAudio slot in sortArray)
        {
            AddItem(slot.item, slot.stack);
        }

    }
    public void CreateInventorySlolt()
    {
        inventorySlots = new InventorySlotAudio[SlotAmount];
        for (int i = 0; i < SlotAmount; i++)
        {
            Transform slot = Instantiate(slotPrefab, inventoryPanel);
            InventorySlotAudio invSlot = slot.GetComponent<InventorySlotAudio>();

            inventorySlots[i] = invSlot;
            invSlot.inventory = this;
            invSlot.SetThisSlot(EMPTY_ITEM, 0);
        }
    }



    public InventorySlotAudio IsEmptySlotLeft(SO_Item itemCheck = null, InventorySlotAudio itemSlot = null)
    {
        InventorySlotAudio firstEmptySlot = null;
        foreach (InventorySlotAudio slot in inventorySlots)
        {
            if (slot == itemSlot)
            {
                continue;
            }
            if (slot.item == itemCheck)
            {
                if (slot.stack < slot.item.maxStack)
                {
                    return slot;
                }
            }
            else if (slot.item == EMPTY_ITEM && firstEmptySlot == null)
                firstEmptySlot = slot;
        }
        return firstEmptySlot;
    }
    public void SetLayoutControlChild(bool isControlled)
    {
        gridLayoutGroup.enabled = isControlled;
    }
    #endregion

    #region Mini Canvas
    public void SetRightClickSlot(InventorySlotAudio slot)
    {
        rightClickSlot = slot;
    }
    public void OpenMiniCanvas(Vector2 clickPosition)
    {
        miniCanvas.position = clickPosition;
        miniCanvas.gameObject.SetActive(true);
    }
    public void SetLeftClickSlot(InventorySlotAudio slot)
    {
        leftClickSlot = slot;
    }
    public void OnFinishMiniCanvas()
    {
        rightClickSlot = null;
        leftClickSlot = null;
        miniCanvas.gameObject.SetActive(false);
    }

    #endregion

    public bool CheckInventoryForItems(SO_Item item, int requiredAmount)
    {
        int totalAmountInInventory = 0;

        foreach (InventorySlotAudio slot in inventorySlots)
        {
            if (slot.item == item)
            {
                totalAmountInInventory += slot.stack;

                // If you find enough items in the inventory, break out of the loop
                if (totalAmountInInventory >= requiredAmount)
                {
                    return true;
                }
            }
        }

        // If you reach this point, required items were not found in the inventory
        return false;
    }

    #region Read Note

    public void ActivePanelNOte()
    {
        panelNote.SetActive(true);
        inventoryUI.SetActive(false);
        panelButton.SetActive(false);
        SetTextNote(leftClickSlot.item);
    }

    public void SetTextNote(SO_Item item)
    {
        title.text = item.noteTitle;
        audioSource.clip = item.audio;
    }

    #endregion


    #region Save

    public void SaveInventory()
    {
        SaveData saveData = new SaveData();
        saveData.inventorySlotsData = new List<InventorySlotData>();

        foreach (InventorySlotAudio slot in inventorySlots)
        {
            InventorySlotData slotData = new InventorySlotData();
            slotData.stack = slot.stack;
            slotData.item = slot.item;

            saveData.inventorySlotsData.Add(slotData);
        }

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/inventoryAudioSave.json", json);
    }

    public void LoadInventory()
    {
        string filePath = Application.persistentDataPath + "/inventoryAudioSave.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            SetLayoutControlChild(true);
            foreach (InventorySlotAudio slot in inventorySlots)
            {
                Destroy(slot.gameObject);
            }
            CreateInventorySlolt();

            for (int i = 0; i < inventorySlots.Length; i++)
            {
                inventorySlots[i].item = saveData.inventorySlotsData[i].item;
                inventorySlots[i].stack = saveData.inventorySlotsData[i].stack;
            }

            foreach (InventorySlotAudio slot in inventorySlots)
            {
                slot.CheckShowText();
                slot.RefreshIcon(slot.item);
            }
        }
    }

    #endregion
}

