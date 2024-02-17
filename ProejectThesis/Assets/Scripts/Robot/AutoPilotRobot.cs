using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoPilotRobot : MonoBehaviour
{
    public float speed = 2f;
    public Transform player; 
    public float followDistance = 5f;
    [SerializeField] private float timer = 1;
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public RobotController robotController;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        robotController = GetComponent<RobotController>();        
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) >= followDistance && robotController.state.isInteractingBox)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                DelayMove();
        }
        else
        {
            timer = 1f;
            navMeshAgent.SetDestination(transform.position);
            animator.SetFloat("Speed", 0f);
        }


    }

    void DelayMove()
    {        
        navMeshAgent.SetDestination(player.position);
        float speed = navMeshAgent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }
}
