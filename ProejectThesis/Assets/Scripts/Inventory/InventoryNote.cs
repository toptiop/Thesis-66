using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InventoryNote : MonoBehaviour
{
    [Header("Inventory")]
    public SO_Item EMPTY_ITEM;
    public Transform slotPrefab;
    public Transform inventoryPanel;
    protected GridLayoutGroup gridLayoutGroup;

    [Header("Mini Canvas")]
    public RectTransform miniCanvas;
    [SerializeField] protected InventorySlotNote rightClickSlot;

    [Space(5)]
    public int SlotAmount = 30;
    public int slotMat = 10;
    public InventorySlotNote[] inventorySlots;

    void Start()
    {
        gridLayoutGroup = inventoryPanel.GetComponent<GridLayoutGroup>();
        CreateInventorySlolt();
    }

    #region Inventory Methods
    public void AddItem(SO_Item item, int amount)
    {
        InventorySlotNote slot = IsEmptySlotLeft(item);
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
        rightClickSlot.UseItem();
        OnFinishMiniCanvas();
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
    public void RemoveItem(InventorySlotNote slot)
    {
        slot.SetThisSlot(EMPTY_ITEM, 0);
    }
    public void SortItem(bool Ascending = true)
    {
        //Sorting Item
        SetLayoutControlChild(true);
        List<InventorySlotNote> slotHaveItem = new List<InventorySlotNote>();
        foreach (InventorySlotNote slot in inventorySlots)
        {
            if (slot.item != EMPTY_ITEM)
                slotHaveItem.Add(slot);
        }

        var sortArray = Ascending ?
            slotHaveItem.OrderBy(Slot => Slot.item.id).ToArray() :
            slotHaveItem.OrderByDescending(Slot => Slot.item.id).ToArray();

        foreach (InventorySlotNote slot in inventorySlots)
        {
            Destroy(slot.gameObject);
        }

        CreateInventorySlolt();

        foreach (InventorySlotNote slot in sortArray)
        {
            AddItem(slot.item, slot.stack);
        }

    }
    public void CreateInventorySlolt()
    {
        inventorySlots = new InventorySlotNote[SlotAmount];
        for (int i = 0; i < SlotAmount; i++)
        {
            Transform slot = Instantiate(slotPrefab, inventoryPanel);
            InventorySlotNote invSlot = slot.GetComponent<InventorySlotNote>();

            inventorySlots[i] = invSlot;
            invSlot.inventory = this;
            invSlot.SetThisSlot(EMPTY_ITEM, 0);
        }
    }

    public InventorySlotNote IsEmptySlotLeft(SO_Item itemCheck = null, InventorySlotNote itemSlot = null)
    {
        InventorySlotNote firstEmptySlot = null;
        foreach (InventorySlotNote slot in inventorySlots)
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
    public void SetRightClickSlot(InventorySlotNote slot)
    {
        rightClickSlot = slot;
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

    #endregion

    public bool CheckInventoryForItems(SO_Item item, int requiredAmount)
    {
        int totalAmountInInventory = 0;

        foreach (InventorySlotNote slot in inventorySlots)
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
}
