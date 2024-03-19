using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using TMPro;
using UnityEngine.Video;

public class InventoryVideo : MonoBehaviour
{
    [Header("Inventory")]
    public SO_Item EMPTY_ITEM;
    public Transform slotPrefab;
    public Transform inventoryPanel;
    protected GridLayoutGroup gridLayoutGroup;

    [Header("Mini Canvas")]
    public RectTransform miniCanvas;
    [SerializeField] protected InventorySlotVideo rightClickSlot;
    [SerializeField] protected InventorySlotVideo leftClickSlot;

    [Space(5)]
    public int SlotAmount = 30;
    public InventorySlotVideo[] inventorySlots;

    [Header("InventoryUI")]
    public GameObject inventoryUI;
    public GameObject panelButton;
    public Button openNote;

    [Header("Video")]
    public GameObject panelNote;
    public TMP_Text title;
    public VideoPlayer videoPlayer;

    void Start()
    {
        gridLayoutGroup = inventoryPanel.GetComponent<GridLayoutGroup>();
        CreateInventorySlolt();
        //CreateInventorySloltMat();
    }

    #region Inventory Methods
    public void AddItem(SO_Item item, int amount)
    {
        InventorySlotVideo slot = IsEmptySlotLeft(item);
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
       /// rightClickSlot.UseItem();
       // OnFinishMiniCanvas();
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
    public void RemoveItem(InventorySlotVideo slot)
    {
        slot.SetThisSlot(EMPTY_ITEM, 0);
    }
    public void SortItem(bool Ascending = true)
    {
        //Sorting Item
        SetLayoutControlChild(true);
        List<InventorySlotVideo> slotHaveItem = new List<InventorySlotVideo>();
        foreach (InventorySlotVideo slot in inventorySlots)
        {
            if (slot.item != EMPTY_ITEM)
                slotHaveItem.Add(slot);
        }

        var sortArray = Ascending ?
            slotHaveItem.OrderBy(Slot => Slot.item.id).ToArray() :
            slotHaveItem.OrderByDescending(Slot => Slot.item.id).ToArray();

        foreach (InventorySlotVideo slot in inventorySlots)
        {
            Destroy(slot.gameObject);
        }

        CreateInventorySlolt();

        foreach (InventorySlotVideo slot in sortArray)
        {
            AddItem(slot.item, slot.stack);
        }

    }
    public void CreateInventorySlolt()
    {
        inventorySlots = new InventorySlotVideo[SlotAmount];
        for (int i = 0; i < SlotAmount; i++)
        {
            Transform slot = Instantiate(slotPrefab, inventoryPanel);
            InventorySlotVideo invSlot = slot.GetComponent<InventorySlotVideo>();

            inventorySlots[i] = invSlot;
            invSlot.inventory = this;
            invSlot.SetThisSlot(EMPTY_ITEM, 0);
        }
    }

    public InventorySlotVideo IsEmptySlotLeft(SO_Item itemCheck = null, InventorySlotVideo itemSlot = null)
    {
        InventorySlotVideo firstEmptySlot = null;
        foreach (InventorySlotVideo slot in inventorySlots)
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
    public void SetRightClickSlot(InventorySlotVideo slot)
    {
        rightClickSlot = slot;
    }
    public void SetLeftClickSlot(InventorySlotVideo slot)
    {
        leftClickSlot = slot;
    }

    public void OpenMiniCanvas(Vector2 clickPosition)
    {
        miniCanvas.position = clickPosition;
        miniCanvas.gameObject.SetActive(true);
    }

    public void OnFinishMiniCanvas()
    {
        rightClickSlot = null;
        miniCanvas.gameObject.SetActive(false);
    }

    public void ActivePanelNOte()
    {
        panelNote.SetActive(true);
        inventoryUI.SetActive(false);
        panelButton.SetActive(false);
        SetVideo(leftClickSlot.item);
    }

    void SetVideo(SO_Item item)
    {
        title.text = item.itemName;
        videoPlayer.clip = item.clip;
    }

    #endregion

    public bool CheckInventoryForItems(SO_Item item, int requiredAmount)
    {
        int totalAmountInInventory = 0;

        foreach (InventorySlotVideo slot in inventorySlots)
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

    #region Save

    public void SaveInventory()
    {
        SaveData saveData = new SaveData();
        saveData.inventorySlotsData = new List<InventorySlotData>();

        foreach (InventorySlotVideo slot in inventorySlots)
        {
            InventorySlotData slotData = new InventorySlotData();
            slotData.stack = slot.stack;
            slotData.item = slot.item;

            saveData.inventorySlotsData.Add(slotData);
        }

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/inventoryVideoSave.json", json);
    }

    public void LoadInventory()
    {
        string filePath = Application.persistentDataPath + "/inventoryVideoSave.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            SetLayoutControlChild(true);
            foreach (InventorySlotVideo slot in inventorySlots)
            {
                Destroy(slot.gameObject);
            }
            CreateInventorySlolt();

            for (int i = 0; i < inventorySlots.Length; i++)
            {
                inventorySlots[i].item = saveData.inventorySlotsData[i].item;
                inventorySlots[i].stack = saveData.inventorySlotsData[i].stack;
            }

            foreach (InventorySlotVideo slot in inventorySlots)
            {
                slot.CheckShowText();
                slot.RefreshIcon(slot.item);
            }
        }
    }

    #endregion
}