using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour, IInteractable
{
    public bool isPickup;
    public BoxCollider col;
    private PlayerController player;
    private RobotController robot;
    private Rigidbody rb;
    string returnString;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void PickUp(Transform pos)
    {
        isPickup = !isPickup;
        if (!isPickup)
        {
            if (player != null)
            {
                player.pickupOnHand = true;

            }
            else if (robot != null)
            {
                //
            }

            transform.position = pos.position;
            transform.parent = pos;
            if (col != null)
            {
                col.enabled = false;
            }
            rb.useGravity = false;
            
            returnString = "Drop";
        }
        else
        {
            if (player != null)
            {
                player.pickupOnHand = false;
            }
            else if (robot != null)
            {
                
            }
            transform.parent = null;
            if (col != null)
            {
                col.enabled = true;
            }
            rb.useGravity = true;
            returnString = "Pickup";
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerController>();
        }
        else if (other.gameObject.CompareTag("Robot"))
        {
            robot = other.GetComponent<RobotController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
        }
        else if (other.gameObject.CompareTag("Robot"))
        {
            robot = null;
        }
    }

    public string GetInteractionText()
    {
        return returnString;
    }

    public void Interact()
    {
        if(player != null)
        {
            PickUp(player.posHand);
        }
        else if(robot != null)
        {
            // robot pickup
        }
    }
}
