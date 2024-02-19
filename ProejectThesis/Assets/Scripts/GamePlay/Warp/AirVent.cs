using System.Collections;
using UnityEngine;

public class AirVent : MonoBehaviour, IInteractable
{
    public Transform warpPoint;
    public Transform playerTransform;
    public RobotController controller;
    public string GetInteractionText()
    {
        return "Enter the air vent.";
    }

    public void Interact()
    {
        Warp();
    }

    void Warp()
    {
        if (warpPoint != null && playerTransform != null)
        {
            Debug.Log("warp");
            StartCoroutine(DelayEnable());
        }
        else
        {
            Debug.LogWarning("Warp point or player transform is not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Robot"))
        {
            playerTransform = other.transform;
            controller = other.GetComponent<RobotController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Robot"))
        {
            StartCoroutine(DelayNull());
        }
    }

    IEnumerator DelayEnable()
    {
        controller.enabled = false;
        playerTransform.position = warpPoint.position;
        yield return new WaitForSeconds(0.1f);
        controller.enabled = true;
    }

    IEnumerator DelayNull()
    {
        yield return new WaitForSeconds(0.5f);
        playerTransform = null;
        controller = null;
    }
}
