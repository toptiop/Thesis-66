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
    public NavMeshAgent agent;
    public RobotController robotController;
    public Transform targetPos;
    public bool isOrder;
    private InputManager input;
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        robotController = GetComponent<RobotController>();

        input = FindAnyObjectByType<InputManager>();
    }

    void Update()
    {
        Auto();

        if (input.returnPlayer)
        {
            ReturnToPlayer();
            input.returnPlayer = false;
        }

        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    public void SetPositionRobot(Vector3 pos)
    {
        agent.SetDestination(pos);
    }
    public void SetRotationRobot(Quaternion rot)
    {
        transform.rotation = rot;
    }
    void Auto()
    {
        if (!isOrder)
        {
            if (Vector3.Distance(transform.position, player.position) >= followDistance)///&& robotController.state.isInteractingBox
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                    DelayMove();
            }
            else
            {
                timer = 1f;
                agent.SetDestination(transform.position);
                animator.SetFloat("Speed", 0f);
            }
        }
    }


    public void MoveToPosition(Vector3 pos)
    {
        if (Vector3.Distance(transform.position, pos) >= 0)
        {
            isOrder = true;
            agent.SetDestination(pos);
            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed);
        }
        else
        {
            timer = 1f;
            agent.SetDestination(transform.position);
            animator.SetFloat("Speed", 0f);
        }
    }
    public void ReturnToPlayer()
    {
        isOrder = false;
        if (Vector3.Distance(transform.position, player.position) >= followDistance)///&& robotController.state.isInteractingBox
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                DelayMove();
        }
        else
        {
            timer = 1f;
            agent.SetDestination(transform.position);
            animator.SetFloat("Speed", 0f);
        }
    }


    void DelayMove()
    {
        agent.SetDestination(player.position);
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    public void Order(bool order)
    {
        isOrder = order;
    }
}
