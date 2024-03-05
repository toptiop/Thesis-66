using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class DoorHack : MonoBehaviour
{
    public Animator anim;
    public AudioClip open;
    public AudioSource source;

    public bool activeDoor;
    private bool isHacked = false;
    public float hackingTime = 1;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void StartHacking()
    {
        if (!isHacked)
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        if(!activeDoor)
        {
            source.PlayOneShot(open);
            anim.SetBool("isOpen", true);
            activeDoor = true;
        }
    }
        
    public void SetHacked(bool hacked)
    {
        isHacked = hacked;
    }
}
