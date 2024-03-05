using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMoveToPosition : ICommand
{

    private NavMeshAgent agent;
    private Vector3 destination;

    public StateMoveToPosition(NavMeshAgent agent, Vector3 destination)
    {
        this.agent = agent;
        this.destination = destination;
    }

    public void Execute()
    {
        agent.SetDestination(destination);
    }
}
