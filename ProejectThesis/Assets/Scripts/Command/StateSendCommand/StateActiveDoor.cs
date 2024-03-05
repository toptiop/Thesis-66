using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateActiveDoor : ICommand
{
    private NavMeshAgent agent;
    private Command command;

    public StateActiveDoor(NavMeshAgent agent, Command command)
    {
        this.agent = agent;
        this.command = command;
    }

    public void Execute()
    {
        command.SetPosition(agent);
        command.ActiveDoor();
        Singleton.ai.Order(false);
    }
}
