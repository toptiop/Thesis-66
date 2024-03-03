using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]private GameObject inventoryUI;
    public bool activeInventory;
    [SerializeField]private bool isOpen;
    private InputManager _input;
    private void Awake()
    {
        _input = FindAnyObjectByType<InputManager>();
    }

    private void Update()
    {
        if (inventoryUI != null)
            activeInventory = inventoryUI.gameObject.activeSelf;
            isOpen = inventoryUI.gameObject.activeSelf;

        if (_input.inventory)
        {
            SetActive();
            _input.inventory = false;
        }
        #region SetPlayer


        
        #endregion
    }


    void SetActive()
    {
        isOpen = !isOpen;
        if(isOpen)
        {
            GameManager.Instance.ChangeStateInteractUI(true);
            Singleton.controller.SignalCanMoveEnabled();
            inventoryUI.SetActive(true);
        }
        else 
        {
            GameManager.Instance.ChangeStateInteractUI(false);
            Singleton.controller.SignalCanMoveDisabled();
            inventoryUI.SetActive(false); 
        }
    }

    public void CloseUI()
    {
        isOpen = ! isOpen;
        inventoryUI.SetActive(false);
        GameManager.Instance.ChangeStateInteractUI(false);
        Singleton.controller.SignalCanMoveDisabled();
    }
}
