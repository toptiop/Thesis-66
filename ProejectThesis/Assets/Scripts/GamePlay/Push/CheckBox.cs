using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    public int passBox;
    public bool correct;
    public AudioClip unlock;
    public DoorBox door;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            IDBox pbox = other.GetComponent<IDBox>();
            if (pbox != null)
            {
                if(pbox.pass == passBox)
                {
                    correct = true;

                    if (door != null && unlock != null)
                        door.source.PlayOneShot(unlock);
                    else
                        Debug.LogWarning("Null references in" + this);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            correct = false;
        }
    }
}
