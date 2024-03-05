using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateDoorHack : ICommand
{
    private NavMeshAgent agent;
    private Command command;

    public StateDoorHack(NavMeshAgent agent, Command command)
    {
        this.command = command;
        this.agent = agent;
    }
    public void Execute()
    {
        command.SetPosition(agent);
        command.HackingDoor();
        Singleton.ai.Order(false);
    }
}
