using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator anim;
    public AudioClip open,close;
    public AudioSource source;
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(open != null && source != null)
            {
                source.PlayOneShot(open);
            }
            StartCoroutine(DelayDoor(true));

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (close != null && source != null)
            {
                source.PlayOneShot(close);
            }
             StartCoroutine(DelayDoor(false));
             
        }
    }

    IEnumerator DelayDoor(bool newOpen)
    {
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("isOpen", newOpen);
    }
}
