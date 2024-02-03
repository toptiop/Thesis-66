using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBox : MonoBehaviour
{
    public Animator anim;
    public AudioClip open, close;
    public AudioSource source;

    public CheckBox correctBox;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(correctBox != null)
            {
                if (correctBox.correct)
                {
                    if (open != null && source != null)
                    {
                        source.PlayOneShot(open);
                    }
                    StartCoroutine(DelayDoor(true));
                }
            }         
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (correctBox != null)
            {
                if (correctBox.correct)
                {
                    if (close != null && source != null)
                    {
                        source.PlayOneShot(close);
                    }
                    StartCoroutine(DelayDoor(false));
                }
            }           
        }
    }

    IEnumerator DelayDoor(bool newOpen)
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("isOpen", newOpen);
    }
}