using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracObject : MonoBehaviour, IInteractable
{
    public GameObject interactable;

    [SerializeField]
    private bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        interactable.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
            interactable.SetActive(true);
            GameManager.Instance.InteractUI = true;
        }
        else
        {
            interactable.SetActive(false);
            GameManager.Instance.InteractUI = false;
        }
    }

    public void CloseUI()
    {
        interactable.SetActive(false);
        isOpen = false;
    }
}
