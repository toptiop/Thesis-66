using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDetection : MonoBehaviour
{
    public LayerMask detectionLayer;
    public float radiusInteract = 2;
    public float detectionItemRadius = 7f;
    public Vector3 detectionSize = new Vector3(2f, 2f, 2f);
    public bool showIcon;
    ShowIcon[] icon;
    public InputRobotManager _input;
    public ActionUI actionUI;
    void Start()
    {

    }


    void Update()
    {
        CheckObject();
        Interact();
    }
    void CheckItemOnGround()
    {
        // Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);
        Collider[] hitColliders = Physics.OverlapBox(transform.position, detectionSize / 2f, transform.rotation, detectionLayer);

        bool interactableFound = false;

        foreach (Collider col in hitColliders)
        {
            //Debug.Log("Detected: " + col.gameObject.name);//Debug

            IInteractable interactableObject = col.gameObject.GetComponent<IInteractable>();
            if (interactableObject != null)
            {
                string interactionText = interactableObject.GetInteractionText();

                if (actionUI != null)
                {
                    actionUI.ShowInteractionUI(interactionText);
                    Debug.Log("Action != Null");
                }

                if (_input.interaction)
                {
                    _input.interaction = false;
                    interactableObject.Interact();
                }
                interactableFound = true;

            }
            else
            {
                StartCoroutine(DelayFalse());
                //Debug.Log("Not Detected");
            }
        }

        if (!interactableFound && actionUI != null)
        {
            actionUI.HideCanvas();
        }
    }

    void CheckObject()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionItemRadius, detectionLayer);

        bool isAnyIconShown = false;

        foreach (Collider col in hitColliders)
        {
            icon = col.gameObject.GetComponents<ShowIcon>();
            foreach (ShowIcon ic in icon)
            {
                if (ic != null)
                {
                    // Only show the icon if it's within the detection radius
                    if (Vector3.Distance(transform.position, col.transform.position) <= detectionItemRadius && showIcon)
                    {
                        ic.ChangeCantIcon();
                        ic.ActiveIcon();
                        isAnyIconShown = true;
                    }
                    else
                    {
                        ic.HideIcon();
                    }
                }
            }
        }

        // If no icon is shown, hide all icons
        if (!isAnyIconShown)
        {
            foreach (Collider col in hitColliders)
            {
                icon = col.gameObject.GetComponents<ShowIcon>();
                foreach (ShowIcon ic in icon)
                {
                    if (ic != null)
                    {
                        ic.HideIcon();
                    }
                }
            }
        }
    }


    void Interact()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radiusInteract, detectionLayer);

        bool isAnyIconShown = false;

        foreach (Collider col in hitColliders)
        {
            icon = col.gameObject.GetComponents<ShowIcon>();

            IInteractable interactableObject = col.gameObject.GetComponent<IInteractable>();
            foreach (ShowIcon ic in icon)
            {
                if (interactableObject != null)
                {
                    string interactionText = interactableObject.GetInteractionText();

                    if (actionUI != null)
                    {
                        // actionUI.ShowInteractionUI(interactionText);

                    }

                    if (_input.interaction)
                    {
                        _input.interaction = false;
                        interactableObject.Interact();
                    }
                }
                else
                {
                    StartCoroutine(DelayFalse());
                }

                if (ic != null)
                {
                    // Only show the icon if it's within the detection radius
                    if (Vector3.Distance(transform.position, col.transform.position) <= detectionItemRadius && showIcon)
                    {
                        ic.ChangeIcon();
                        isAnyIconShown = true;
                    }
                    else
                    {
                        ic.ChangeCantIcon();
                    }
                }

                else
                {
                    StartCoroutine(DelayFalse());
                }
            }

            StartCoroutine(DelayFalse());
        }

        // If no icon is shown, hide all icons
        if (!isAnyIconShown)
        {
            actionUI.HideCanvas();
            foreach (Collider col in hitColliders)
            {
                icon = col.gameObject.GetComponents<ShowIcon>();
                foreach (ShowIcon ic in icon)
                {
                    if (ic != null)
                    {
                        ic.ChangeCantIcon();
                    }
                }
            }
        }
    }

    IEnumerator DelayFalse()
    {
        yield return new WaitForSeconds(.1f);
        _input.interaction = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionItemRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, detectionSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusInteract);
    }
}
