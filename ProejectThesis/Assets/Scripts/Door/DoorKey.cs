using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    public Animator anim;
    public AudioClip open, close;
    public AudioSource source;

    public KeyPad keyPad;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(keyPad != null)
            {
                if (keyPad.isUnlock)
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
            if (keyPad != null)
            {
                if (keyPad.isUnlock)
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
