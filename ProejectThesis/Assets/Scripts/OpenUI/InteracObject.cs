using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracObject : MonoBehaviour, IInteractable, OpenUI
{
    public GameObject interactable;
    
    public bool isOpen;

    [SerializeField] private GetInterface setinterface;

    public bool IsOpen
    {
        get { return isOpen; }
        set { isOpen = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        setinterface.Interface = this;
        interactable.SetActive(false);
    }

    // Update is called once per frame

    public string GetInteractionText()
    {
        return "Open Pad";
    }

    public void Interact()
    {
        Use();
    }

    void Use()
    {
        isOpen = !isOpen;

        if(isOpen)
        {
            UIManager.instance.AddUI(interactable);
            interactable.SetActive(true);
            GameManager.Instance.ChangeStateInteractUI(true);
            Singleton.controller.SignalCanMoveEnabled();
        }
        else
        {
            interactable.SetActive(false);
            GameManager.Instance.ChangeStateInteractUI(false);
            Singleton.controller.SignalCanMoveDisabled();
        }
    }

    public void CloseUI()
    {
        interactable.SetActive(false);
        isOpen = false;
    }

    public void Close()
    {
        isOpen = false;
    }
}
