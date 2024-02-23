using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour, IInteractable
{
    public bool isOpen;
    public Animator anim;
    public BoxCollider door;

    public string GetInteractionText()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    void Open()
    {
        isOpen = !isOpen;

        if(isOpen)
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

        yield return new WaitForSeconds(1);

        door.enabled = true;
    }
}
