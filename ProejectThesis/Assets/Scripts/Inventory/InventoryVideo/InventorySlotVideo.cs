using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlotVideo : MonoBehaviour, IDropHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler,IPointerEnterHandler, IPointerExitHandler
{
    [Header("Inventory Detail")]
    public InventoryVideo inventory;

    [Header("Slot Detail")]
    public SO_Item item;
    public int stack;

    [Header("UI")]
    public Color emptyColor;
    public Color itemColor;
    public Image icon;
    public TMP_Text stackText;

    [Header("Drag and Drop")]
    public int siblingIndex;
    public RectTransform draggable;
    Canvas canvas;
    CanvasGroup canvasGroup;

    Button button;
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        siblingIndex = transform.GetSiblingIndex();

        button = GetComponentInParent<Button>();

    }

    private void Update()
    {
        if (item != inventory.EMPTY_ITEM)
        {
            button.onClick.AddListener(() =>
            {
                inventory.ActivePanelNOte();
            });
        }
    }

    #region Drag and Drop Methods
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item == inventory.EMPTY_ITEM)
                return;
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            transform.SetAsLastSibling();
            inventory.SetLayoutControlChild(false);
            inventory.OnFinishMiniCanvas();
        }

    }
    public void OnDrag(PointerEventData eventData)
    {
        if (item == inventory.EMPTY_ITEM)
            return;
        draggable.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        draggable.anchoredPosition = Vector2.zero;
        transform.SetSiblingIndex(siblingIndex);
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            InventorySlotVideo slot = eventData.pointerDrag.GetComponent<InventorySlotVideo>();
            if (slot != null)
            {
                if (slot.item == item)
                {
                    MerageThisSlot(slot);
                }
                else
                {
                    SwapSlot(slot);
                }

            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item == inventory.EMPTY_ITEM)
                return;
            // inventory.OpenMiniCanvas(eventData.position);
            // inventory.SetRightClickSlot(this);
            inventory.OnFinishMiniCanvas();
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            inventory.SetLeftClickSlot(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != inventory.EMPTY_ITEM)
            inventory.openNote.gameObject.SetActive(true);

        InventoryInfo.instance.SetupInfoDisplay(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item != inventory.EMPTY_ITEM)
            inventory.openNote.gameObject.SetActive(false);

        InventoryInfo.instance.SetupNull();
    }
    #endregion

    public void UseItem()
    {
        stack = Mathf.Clamp(stack - 1, 0, item.maxStack);
        if (stack > 0)
            CheckShowText();
        inventory.ActivePanelNOte();
    }
    public void SwapSlot(InventorySlotVideo newSlot)
    {
        SO_Item keepItem;
        int keepStack;

        keepItem = item;
        keepStack = stack;

        SetSwap(newSlot.item, newSlot.stack);

        newSlot.SetSwap(keepItem, keepStack);
    }

    public void SetSwap(SO_Item swapItem, int amount)
    {
        item = swapItem;
        stack = amount;
        icon.sprite = swapItem.icon;

        CheckShowText();
    }

    public void MerageThisSlot(InventorySlotVideo mergeSlot)
    {
        if (stack == item.maxStack || mergeSlot.stack == mergeSlot.item.maxStack)
        {
            SwapSlot(mergeSlot);
            return;
        }
        int ItemAmount = stack + mergeSlot.stack;

        int intInthisSlot = Mathf.Clamp(ItemAmount, 0, item.maxStack);
        stack = intInthisSlot;

        CheckShowText();

        int amontLeft = ItemAmount - intInthisSlot;
        if (amontLeft > 0)
        {
            mergeSlot.SetThisSlot(mergeSlot.item, amontLeft);
        }
        else
        {
            inventory.RemoveItem(mergeSlot);
        }
    }
    public void MerageThisSlot(SO_Item mergeItem, int mergeAmout)
    {
        item = mergeItem;
        icon.sprite = mergeItem.icon;

        int ItemAmount = stack + mergeAmout;

        int intInthisSlot = Mathf.Clamp(ItemAmount, 0, item.maxStack);
        stack = intInthisSlot;

        CheckShowText();

        int amontLeft = ItemAmount - intInthisSlot;
        if (amontLeft > 0)
        {
            InventorySlotVideo slot = inventory.IsEmptySlotLeft(mergeItem, this);
            if (slot == null)
            {
                inventory.DropItem(mergeItem, amontLeft);
                return;
            }
            else
            {
                slot.MerageThisSlot(mergeItem, amontLeft);
            }
        }
    }

    public void SetThisSlot(SO_Item newItem, int amount)
    {
        item = newItem;
        icon.sprite = newItem.icon;

        int ItemAmount = amount;

        int intInthisSlot = Mathf.Clamp(ItemAmount, 0, newItem.maxStack);
        stack = intInthisSlot;

        CheckShowText();

        int amontLeft = ItemAmount - intInthisSlot;
        if (amontLeft > 0)
        {
            InventorySlotVideo slot = inventory.IsEmptySlotLeft(newItem, this);
            if (slot == null)
            {
                //Drop Item
                ItemSpawner.Instance.SpawnItem(newItem, amontLeft);
                return;
            }
            else
            {
                slot.SetThisSlot(newItem, amontLeft);
            }
        }
    }

    public void CheckShowText()
    {
        UpdateColorSlot();
        stackText.text = stack.ToString();
        if (item.maxStack < 2)
        {
            stackText.gameObject.SetActive(false);
        }
        else
        {
            if (stack > 1)
                stackText.gameObject.SetActive(true);
            else
                stackText.gameObject.SetActive(false);
        }
    }

    public void UpdateColorSlot()
    {
        if (item == inventory.EMPTY_ITEM)
            icon.color = emptyColor;
        else
            icon.color = itemColor;

    }
    public void RefreshIcon(SO_Item item)
    {
        icon.sprite = item.icon;
    }
}
