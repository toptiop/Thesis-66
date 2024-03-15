using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour, IInteractable
{

    public Vector3 setRotation;
    public GameObject box;
    public bool setParent;
    public Rigidbody rb;

    public PlayerController player;
    public RobotController robot;
    string retureString;

    private void Update()
    {
        if (setParent)
        {
            retureString = "Push Box";
        }

    }
    public string GetInteractionText()
    {
        return retureString;
    }

    public void Interact()
    {
        // ตรวจสอบว่ามีผู้เล่นหรือหุ่นยนต์อยู่ใกล้กล่องหรือไม่
        if (player != null)
        {
            MoveBox(player.transform);
        }
        else if (robot != null)
        {
            MoveBox(robot.transform);
        }
    }

    // ย้ายกล่องตามตำแหน่งที่กำหนด
    private void MoveBox(Transform mover)
    {
        setParent = !setParent;
        if (!setParent)
        {
            mover.transform.position = transform.position;
            mover.transform.rotation = Quaternion.Euler(setRotation);

            rb.isKinematic = true;
            if (player != null)
            {
                player.state.AnimationInteractingBox(true);
                box.transform.position = player.boxPos.position;
                box.transform.parent = player.boxPos;
            }
            else if (robot != null)
            {
                robot.state.AnimationInteractingBox(true);
                box.transform.parent = mover;
            }
            retureString = "Cancel Push Pox";
        }
        else
        {
            box.transform.parent = null;
            rb.isKinematic = false;
            if (player != null)
            {
                player.state.AnimationInteractingBox(false);
            }
            else if (robot != null)
            {
                robot.state.AnimationInteractingBox(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerController>();
        }
        //else if (other.gameObject.CompareTag("Robot"))
        //{
        //    robot = other.GetComponent<RobotController>();
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerController>();
        }
        //else if (other.gameObject.CompareTag("Robot"))
        //{
        //    robot = other.GetComponent<RobotController>();
        //}
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
}
