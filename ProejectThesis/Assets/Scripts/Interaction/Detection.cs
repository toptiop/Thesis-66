using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public LayerMask detectionLayer;
    public float detectionRadius = 2f;
    public Vector3 detectionSize = new Vector3(2f, 2f, 2f);


    public InputManager _input;
    void Start()
    {
        
    }


    void Update()
    {
        CheckItemOnGround();
    }
    void CheckItemOnGround()
    {
        // Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);
        Collider[] hitColliders = Physics.OverlapBox(transform.position, detectionSize / 2f, transform.rotation, detectionLayer);

        bool interactableFound = false;

        foreach(Collider col in hitColliders)
        {
            Debug.Log("Detected: " + col.gameObject.name);//Debug
            IInteractable interactableObject = col.gameObject.GetComponent<IInteractable>();
            if(interactableObject != null)
            {
                string interactionText = interactableObject.GetInteractionText();

                // ActionText

                if (_input.interaction)
                {
                    _input.interaction = false;
                    interactableObject.Interact();
                }
            }
            else
            {
                StartCoroutine(DelayFalse());
                Debug.Log("Not Detected");
            }
        }

        // Hide ActionUI
    }

    IEnumerator DelayFalse()
    {
        yield return new WaitForSeconds(.1f);
        _input.interaction = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, detectionSize);
    }
}
