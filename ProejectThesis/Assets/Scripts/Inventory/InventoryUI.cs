using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour, OpenUI
{
    [SerializeField]private GameObject inventoryUI;
    public bool activeInventory;
    [SerializeField]private bool isOpen;
    private InputManager _input;

    public List<GameObject> tapUI = new List<GameObject>();

    [SerializeField] private GetInterface setInterface;

    public bool IsOpen
    {
        get { return isOpen; }
        set { isOpen = value; }
    }

    private void Awake()
    {
        _input = FindAnyObjectByType<InputManager>();

        inventoryUI.SetActive(false);
    }

    private void Start()
    {
        setInterface.Interface = this;
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
            UIManager.instance.AddUI(inventoryUI);
            GameManager.Instance.ChangeStateInteractUI(true);
            Singleton.controller.SignalCanMoveEnabled();
            inventoryUI.SetActive(true);
            tapUI[0].SetActive(true);
        }
        else 
        {
            GameManager.Instance.ChangeStateInteractUI(false);
            Singleton.controller.SignalCanMoveDisabled();
            inventoryUI.SetActive(false); 

            foreach(GameObject tap in tapUI)
            {
                tap.SetActive(false);
            }
        }
    }

    public void CloseUI() // Fuction On UI
    {
       isOpen = ! isOpen;
        inventoryUI.SetActive(false);
        GameManager.Instance.ChangeStateInteractUI(false);
        Singleton.controller.SignalCanMoveDisabled();
    }

    public void Close()
    {
        isOpen = false;
    }
}
