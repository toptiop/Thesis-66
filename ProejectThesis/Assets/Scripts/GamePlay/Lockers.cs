using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockers : MonoBehaviour,IInteractable
{
    public bool isOpen;
    public Animator anim;
    public BoxCollider door;

    public string GetInteractionText()
    {
        return "Open Locker";
    }

    public void Interact()
    {
        Open();
    }

    void Open()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            StartCoroutine(DisableCollider());
            anim.SetBool("isOpen", isOpen);
        }
        else
        {
            StartCoroutine(DisableCollider());
            anim.SetBool("isOpen", isOpen);
        }
    }

    IEnumerator DisableCollider()
    {
        door.enabled = false;

        yield return new WaitForSeconds(3.5f);

        door.enabled = true;
    }
}
