using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCommand : MonoBehaviour
{
    public LayerMask layerMask;
    public LayerMask obstructionMask;
    public Transform cameraPos;
    public float distance = 5;
    public Vector3 targetPosition;

    [Header("AI")]
    public AutoPilotRobot robot;

    [Header("Out Component")]
    [SerializeField] private Outline outlineScript;
    [SerializeField] private Command sendCommand;
    InputManager input;
    RaycastHit hit;
    void Start()
    {
        input = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, distance) && !IsInLayerMask(hit.collider.gameObject.layer, obstructionMask))
        {
            Debug.Log("Hit Name: " + hit.collider.name);
            if (hit.collider != null)
            {
                this.targetPosition = hit.point;
                outlineScript = hit.collider.GetComponent<Outline>();
                if (outlineScript != null)
                {
                    outlineScript.enabled = true;
                }

                sendCommand = hit.collider.GetComponent<Command>();

            }

            //Debug.DrawLine(ray.origin, hit.point, Color.red, distance);
        }
        else
        {
            //Debug.DrawRay(ray.origin, ray.direction * distance, Color.green, distance);
            if (outlineScript != null )
            {
                outlineScript.enabled = false;
                outlineScript = null;                
            }
            if(sendCommand != null)
            {
                //sendCommand = null;
            }
        }
        if (input.swap)
        {
           robot.MoveToPosition(hit.point);
            Debug.DrawLine(ray.origin, hit.point, Color.red, distance);
            input.swap = false;
        }


        if (Vector3.Distance(robot.transform.position, hit.point) <= 1) ;
        {
            if (sendCommand != null)
            {

            }
               // sendCommand.ReceiveCommand(sendCommand.typeCommand);
        }
    }
    

    // Check if a layer is in a LayerMask
    bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}


