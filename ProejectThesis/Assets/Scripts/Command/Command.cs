using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Command : MonoBehaviour
{
    public enum Commander { OpenDoor,HackDoor,ActiveSwitch, SharePower}

    public Commander typeCommand;

    public Vector3 setPos;
    public Quaternion setRotation;

    [Header("Check Robot")]
    public Vector3 GroundedOffset;
    public Vector3 GroundedRadius;     
    public bool isRobot;
    [Tooltip("Select Layer Robot")]
    public LayerMask layerRobot;
    bool isPlaySound;
    

    [Space]
    [Header("Robot Componenet")]
    [Tooltip("Don't add Robot components.")]
    [SerializeField] private AutoPilotRobot robot;

    #region Setting
    [Space]
    [Header("OpenDoor")]//OpenDoor
    [Tooltip("Add Component DoorActive")]
    [SerializeField] DoorActive door;

    [Space]
    [Header("HackDoor")]
    [SerializeField] DoorHack doorHacking;
    [SerializeField] float timeToHacking = 2f;
    [SerializeField] bool isHacking = false;
    

    [Space]
    [Header("ActiveSwitch")]
    [SerializeField] GameObject activeObject;
    [SerializeField] bool isActiveObj;

    [Space]
    [Header("SharePower")]
    [SerializeField] GameObject sharePowerToObj;
    [SerializeField] bool isShare;
    #endregion

    [Header("Time Active")]
    [SerializeField] float timeToActive;
    [SerializeField] Image imgActive;


    [Space]
    [Header("Colliders")]
    [SerializeField] Collider[] col;
    Outline outline;
    private void Awake()
    {
        outline = GetComponent<Outline>();
    }
    private void Update()
    {
        CheckRobot();

        if(col.Length > 0)
        {
           foreach(Collider coll in col)
            {
                if (coll.gameObject.CompareTag("Robot"))
                {
                    Debug.Log("GG");
                    ReceiveCommand(typeCommand);
                }
            }
        }

        switch (typeCommand)
        {
            case Commander.OpenDoor:
                if(imgActive != null)
                    imgActive.fillAmount = timeToActive / 0f;
                break;
            case Commander.HackDoor:
                if (imgActive != null)
                    imgActive.fillAmount = timeToActive / timeToHacking;
                break;
            case Commander.ActiveSwitch:
                break;
            case Commander.SharePower:
                break;
        }
    }

    public void ReceiveCommand(Commander command)
    {
        switch (command)
        {
            case Commander.OpenDoor:
                if (robot != null)
                {
                    robot.navMeshAgent.SetDestination(setPos);
                    robot.SetPositionRobot(setRotation);
                }
                Invoke("OpenDoor", 2f);
                break;

            case Commander.HackDoor:
                if (robot != null)
                {
                    robot.navMeshAgent.SetDestination(setPos);
                    robot.SetPositionRobot(setRotation);
                }
                if (!isHacking)
                {
                    timeToActive += Time.deltaTime;
                    isHacking = true;
                    Invoke("HackingDoor", timeToHacking);
                }
                break;

            case Commander.ActiveSwitch:
                //
                break;

            case Commander.SharePower:
                SharePower(); // เรียกใช้เมทอด ShaerPower เมื่อได้รับคำสั่ง ShaerPower
                break;

            default:
                Debug.LogWarning("Unknown command received."); // กรณีไม่รู้จักคำสั่งที่ถูกส่งมา
                break;
        }
    }
    public void OpenDoor()
    {
       if(isRobot)
        {
            Debug.Log("Open Door");
            door.isDelaying = true;
        
            if(!isPlaySound)
            {
                isPlaySound = true;
                StartCoroutine(door.DelayDoor(true, door.open));
            }
            this.enabled = false;
            Destroy(outline);
        }
    }
    public void HackingDoor()
    {
        if (isRobot)
        {
            
            doorHacking.StartHacking();
            doorHacking.SetHacked(true);
            this.enabled = false;
            Destroy(outline,1f);
            timeToActive = 0f;
        }
    }

    public void SharePower()
    {

    }

    void CheckRobot()
    {
        Vector3 boxCenter = transform.position -  GroundedOffset;
        isRobot = Physics.CheckBox(boxCenter, new Vector3(GroundedRadius.x, GroundedRadius.y, GroundedRadius.z), Quaternion.identity, layerRobot, QueryTriggerInteraction.Ignore);
        Collider[] colliders = Physics.OverlapBox(boxCenter, GroundedRadius / 2, Quaternion.identity, layerRobot, QueryTriggerInteraction.Ignore);
        col = colliders;
       
        if (isRobot)
        {
            foreach (Collider col in colliders) 
            {
                robot = col.GetComponent<AutoPilotRobot>();
            }
        }
        else
        {
            robot = null;          
        }
    }

    void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (isRobot) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        Vector3 position = transform.position - new Vector3(GroundedOffset.x, GroundedOffset.y, GroundedOffset.z);

        Gizmos.DrawCube(position, GroundedRadius);
    }



}
