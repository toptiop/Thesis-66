using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActive : MonoBehaviour
{
    public Animator anim;
    public AudioClip open, close;
    public AudioSource source;
    public bool isDelaying = false;
    void Start()
    {
        if(anim == null)
            anim = GetComponent<Animator>();
    }

    
    public IEnumerator DelayDoor(bool newOpen, AudioClip clip)
    {
        isDelaying = false;
        source.PlayOneShot(clip);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("isOpen", newOpen);
    }

    public void Open()
    {
        anim.SetBool("isOpen", true);
    }
}
