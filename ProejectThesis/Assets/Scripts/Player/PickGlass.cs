using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickGlass : MonoBehaviour, IInteractable
{
    public equipment equipment;

    private void Start()
    {
        equipment = FindObjectOfType<equipment>();
    }
    public string GetInteractionText()
    {
        return "";
    }

    public void Interact()
    {
        equipment.SwapGlass();
        Singleton.Instance.switchcharacters.canSwitch = true;
        Destroy(gameObject);
    }
}