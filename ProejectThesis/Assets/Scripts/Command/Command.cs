using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Command : MonoBehaviour, IInteractable
{
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
    [SerializeField] private ShowIcon icon;
    [SerializeField] private GameObject canvasIcon;

    [Space]
    [Header("Robot Componenet")]
    [Tooltip("Don't add Robot components.")]
    [SerializeField] private AutoPilotRobot robot;

    #region Setting

    [Space]
    [Header("HackDoor")]
    [SerializeField] DoorHack doorHacking;
    [SerializeField] private bool isHacking = false;
    

    [Space]
    [Header("ActiveSwitch")]
    [SerializeField] private DoorActive door;
    [SerializeField] private bool isActiveObj;

    [Space]
    [Header("SharePower")]
    [SerializeField] private GameObject sharePowerToObj;
    [SerializeField] private bool isShare;
    #endregion


    [Space]
    [Header("Colliders")]
    [SerializeField] private Collider[] col;
    

    Outline outline;
  
    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void Start()
    {
        icon = GetComponent<ShowIcon>();      
    }
   
    private void Update()
    {
        CheckRobot();
    }

    public void SetPosition(NavMeshAgent agent)
    {
        agent.SetDestination(setPos);
        agent.transform.position = setPos;
        agent. transform.rotation = setRotation;
    }

    public void ActiveDoor()
    {
       if(isRobot)
        {
           // Debug.Log("Open Door");
            door.isDelaying = true;
        
            if(!isPlaySound)
            {
                isPlaySound = true;
                StartCoroutine(door.DelayDoor(true, door.open));
            }
            this.enabled = false;
            Destroy(outline);
            Destroy(icon);
            Destroy(canvasIcon);
            robot.Order(false);
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
            Destroy(icon);
            Destroy(canvasIcon); 
            robot.Order(false);
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

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (isRobot) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        Vector3 position = transform.position - new Vector3(GroundedOffset.x, GroundedOffset.y, GroundedOffset.z);

        Gizmos.DrawCube(position, GroundedRadius);
    }

    public string GetInteractionText()
    {
        return "";
    }

    public void Interact()
    {
        switch (typeCommand)
        {
            case Commander.HackDoor:
                HackingDoor();
                break;
            case Commander.ActiveSwitch:
                ActiveDoor();
                break;
            case Commander.SharePower:
                break;
            case Commander.None:
                break;
        }


    }
}
