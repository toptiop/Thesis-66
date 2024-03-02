using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveOutline : MonoBehaviour
{
    Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            outline.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            outline.enabled = false;
        }
    }


}
