using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    public Animator anim;
    public AudioClip open, close;
    public AudioSource source;

    public KeyPad keyPad;
    public bool isOpen;
    bool isPlay;
    public Renderer doorMat;
    public Material matClose;
    public Material matUnlock;
    private void Update()
    {
        if (keyPad != null)
        {
            if (keyPad.isUnlock)
            {
                if (open != null && source != null)
                {
                    if (!isPlay)
                    {
                        isPlay = true;
                        source.PlayOneShot(open);
                    }

                }
                StartCoroutine(DelayDoor(true));
            }
        }


        if (keyPad != null)
        {
            isOpen = keyPad.isUnlock;
            if (doorMat != null && doorMat.materials.Length >= 2)
            {
                if (keyPad.isUnlock)
                {
                    Material[] materials = doorMat.materials;
                    materials[1] = matClose;
                    doorMat.materials = materials;
                }
                else
                {
                    Material[] materials = doorMat.materials;
                    materials[1] = matUnlock;
                    doorMat.materials = materials;
                }
            }
            else
            {
                //Debug.LogError("Door material or materials array not properly set.");
            }
        }
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }

   
    IEnumerator DelayDoor(bool newOpen)
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("isOpen", newOpen);
        Destroy(this);
    }
}
