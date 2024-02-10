using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindRobot : MonoBehaviour, IInteractable
{
    public string GetInteractionText()
    {
        return "Active Robot";
    }

    public void Interact()
    {
        ActiveRobot();
    }

    void ActiveRobot()
    {
        Singleton.Instance.switchcharacters.activeSwitch = true;
        Destroy(gameObject);
    }
}
