using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBox : MonoBehaviour
{
    public Animator anim;
    public AudioClip open, close;
    public AudioSource source;

    public CheckBox correctBox;
    public bool isOpen;
    public Renderer doorMat;
    public Material matClose;
    public Material matUnlock;
    private void Update()
    {
        if (correctBox != null)
        {
            isOpen = correctBox.correct;
            if (doorMat != null && doorMat.materials.Length >= 2)
            {
                if (correctBox.correct)
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

/*
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
*/

    public IEnumerator DelayDoor(bool newOpen)
    {
        source.PlayOneShot(open);
        yield return new WaitForSeconds(1);
        anim.SetBool("isOpen", newOpen);
    }
}