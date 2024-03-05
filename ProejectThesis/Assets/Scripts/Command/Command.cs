using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Command : MonoBehaviour
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
    

    [Space]
    [Header("Robot Componenet")]
    [Tooltip("Don't add Robot components.")]
    [SerializeField] private AutoPilotRobot robot;

    #region Setting

    [Space]
    [Header("HackDoor")]
    [SerializeField] DoorHack doorHacking;
    [SerializeField] float timeToHacking = 2f;
    [SerializeField] bool isHacking = false;
    

    [Space]
    [Header("ActiveSwitch")]
    [SerializeField] DoorActive door;
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
            timeToActive = 0f;
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

    void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (isRobot) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        Vector3 position = transform.localPosition - new Vector3(GroundedOffset.x, GroundedOffset.y, GroundedOffset.z);

        Gizmos.DrawCube(position, GroundedRadius);
    }



}
