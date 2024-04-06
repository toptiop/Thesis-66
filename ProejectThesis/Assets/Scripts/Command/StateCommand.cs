using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCommand : MonoBehaviour
{
    public Commander typeCommander;

    public LayerMask layerMask;
    public LayerMask obstructionMask;
    public Transform cameraPos;
    public float distance = 17;
    public Vector3 targetPosition;

    [Header("AI")]
    public AutoPilotRobot robot;

    [Header("Out Component")]
    [SerializeField] private Outline outlineScript;
    [SerializeField] private Command setType;
    public CommandInvoker invoker;
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

        if (Physics.Raycast(ray, out hit, distance) && !IsInLayerMask(hit.collider.gameObject.layer, obstructionMask))
        {
            targetPosition = hit.point;

            outlineScript = hit.collider.GetComponent<Outline>();
            if (outlineScript != null)
                outlineScript.enabled = true;

            setType = hit.collider.GetComponent<Command>();
            if (setType != null)
                typeCommander = setType.typeCommand;

            Debug.DrawLine(ray.origin, hit.point, Color.red, distance);
        }
        else
        {
            if (outlineScript != null)
                outlineScript.enabled = false;

            setType = null;
            typeCommander = Commander.None;
        }

        if (input.command)
        {
            if (invoker != null)
            {
                ReceiveCommand(typeCommander);
            }
            else
            {
                Debug.LogError("Invoker or setType has not been assigned in the inspector!");
            }
            input.command = false;
        }

    }
    void ReceiveCommand(Commander command) 
    {
        switch (command)
        {
            case Commander.HackDoor:
                MoveToPositionAndHackDoor(hit.collider.GetComponent<Command>().setPos, hit.collider.GetComponent<Command>());
                break;
            case Commander.ActiveSwitch:
                MoveToPositionAndOpenDoor(hit.collider.GetComponent<Command>().setPos, hit.collider.GetComponent<Command>());
                break;
            case Commander.SharePower:
                break;
            case Commander.None:
                MoveToPosition(hit.point);
                break;
        }
    }

    #region Command

    void MoveToPosition(Vector3 destination)
    {
        Debug.Log("Move To" + hit.point);
        robot.Order(true);
        ICommand moveCommand = new StateMoveToPosition(robot.agent, destination);
        invoker.ExecuteCommand(moveCommand);
    }

    void MoveToPositionAndOpenDoor(Vector3 destination, Command command = null)
    {
        robot.Order(true);
        ICommand moveCommand = new StateMoveToPosition(robot.agent, destination);
        invoker.ExecuteCommand(moveCommand);
        StartCoroutine(WaitForMoveCompletionAndOpenDoor(command));
    }

    private IEnumerator WaitForMoveCompletionAndOpenDoor(Command command)
    {
        while (robot.agent.pathPending || robot.agent.remainingDistance > 0.1f)
        {
            yield return null;
        }
        robot.agent.SetDestination(command.setPos);
        ICommand openDoorCommand = new StateActiveDoor(robot.agent, command);
        invoker.ExecuteCommand(openDoorCommand);
    }

    void MoveToPositionAndHackDoor(Vector3 destination, Command command = null)
    {
        robot.Order(true);
        ICommand moveCommand = new StateMoveToPosition(robot.agent, destination);
        invoker.ExecuteCommand(moveCommand);
        StartCoroutine(WaitForMoveCompletionAndHackDoor(command));
    }

    private IEnumerator WaitForMoveCompletionAndHackDoor(Command command)
    {
        while (robot.agent.pathPending || robot.agent.remainingDistance > 0.1f)
        {
            yield return null;
        }
        robot.agent.SetDestination(command.setPos);
        ICommand openDoorCommand = new StateDoorHack(robot.agent, command);
        invoker.ExecuteCommand(openDoorCommand);
    }

    void MoveToPositionAndSharePower(Vector3 destination, Command command = null)
    {
        robot.Order(true);
        ICommand moveCommand = new StateMoveToPosition(robot.agent, destination);
        invoker.ExecuteCommand(moveCommand);
        StartCoroutine(WaitForMoveCompletionAndSharePower(command));
    }

    private IEnumerator WaitForMoveCompletionAndSharePower(Command command)
    {
        while (robot.agent.pathPending || robot.agent.remainingDistance > 0.1f)
        {
            yield return null;
        }
        robot.agent.SetDestination(command.setPos);
        ICommand openDoorCommand = new StateDoorHack(robot.agent, command);
        invoker.ExecuteCommand(openDoorCommand);
    }
    #endregion

    // Check if a layer is in a LayerMask
    bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}

public enum Commander {HackDoor, ActiveSwitch, SharePower, None }
