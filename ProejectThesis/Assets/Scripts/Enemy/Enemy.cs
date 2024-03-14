using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public State currentState;

    [Header("Movement")]
    public float walkSpeed = 1;
    public float sprintSpeed = 5.5f;

    [Header("Waypoint")]
    public List<StrogeWaypoint> waypointsPattern = new List<StrogeWaypoint>();
    [SerializeField] private int pattern;
    [SerializeField]private int _currentWaypoint;
    [SerializeField] private Transform currentWaypoint;
    [SerializeField] private float timeRemaining = 5;
    float timer;

    [Header("Alert")]
    public float alertTime;
    [SerializeField] private float _currentOutOfSightDuration;
    public float OutOfSightDuration = 3;
    [SerializeField] 
    private float waitReturnState = 5;

    [Header("Attack")]
    public bool showGUI = true;
    public float range = 2;
    public float GUIOffset;

    
    [Header("Head")]
    public bool isRotate = false;
    public GameObject refHead;
    public float moveSpeed = 2f; // ความเร็วในการเคลื่อนที่

    private Vector3 startPos; // ตำแหน่งเริ่มต้น
    public Vector3 minPos; // ตำแหน่งขอบเขตต่ำสุด
    public Vector3 maxPos; // ตำแหน่งขอบเขตสูงสุด

    private float direction = 1f;

    [Header("Animator"),SerializeField]
    private Animator animator;
    public float rotateSpeed = 2f;

    [SerializeField]private EnemyFieldOfView vision;
    [SerializeField] private RigBuilder rig;
    public MultiAimConstraint multiAimConstraint;
    private NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        vision = GetComponentInChildren<EnemyFieldOfView>();
        rig = GetComponentInChildren<RigBuilder>();

        animator.applyRootMotion = true;
        agent.updateRotation = true;

        startPos = refHead.transform.localPosition;
        timer = timeRemaining;
        pattern = Random.Range(0, waypointsPattern.Count);

        EnterState(State.Search);
    }

    // Update is called once per frame
    void Update()
    {

        currentWaypoint = waypointsPattern[pattern].waypoint[_currentWaypoint];

        if(timeRemaining <= 4.5f)
        {
            rig.enabled = true;
            isRotate = true;
        }
        else
        {
            rig.enabled = false;
            isRotate = false;
        }
            

       // MoveToWayPoint();
        RotateHead();
        //RotateTowardsWaypoint();

        UpdateState();

        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);

      
    }

    void EnterState(State newState)
    {
        ExitState();
        currentState = newState;

        switch (currentState)
        {
            case State.Idle:
                agent.speed = 0;
                break;
            case State.Search:
                agent.speed = walkSpeed;
                break;
            case State.CanSee:
                agent.speed = 0;
                break;
            case State.Chase:
                agent.speed = sprintSpeed;
                break;
        }
    }

    void UpdateState()
    {
        switch (currentState)
        {
            case State.Idle:

                if (vision.canSeePlayer)
                    EnterState(State.CanSee);

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(currentWaypoint.transform.forward), Time.deltaTime * rotateSpeed);

                timeRemaining -= Time.deltaTime;
                if ((timeRemaining <= 0))
                {

                    _currentWaypoint++;
                    if (_currentWaypoint >= waypointsPattern[pattern].waypoint.Count)
                    {
                        pattern = Random.Range(0, waypointsPattern.Count);
                        _currentWaypoint = 0;
                    }


                    timeRemaining = timer;
                    EnterState(State.Search);
                }
               

                break;
            case State.Search:

                RotateTowardsWaypoint();

                if (vision.canSeePlayer)
                    EnterState(State.CanSee);

                Vector3 waypointVector = transform.position - agent.destination;
                if(Vector3.Distance(transform.position, currentWaypoint.position) >= 1)
                {
                    agent.SetDestination(currentWaypoint.position);
                }
                else if(Vector3.Distance(transform.position,currentWaypoint.position) <= 0.1)
                {
                    EnterState(State.Idle);
                }

                break;
            case State.CanSee:

                agent.SetDestination(transform.position);

                if (vision.canSeePlayer)
                {
                    alertTime += Time.deltaTime;
                    if (alertTime > 5)
                        EnterState(State.Chase);
                }
                else
                {
                    alertTime = 0;
                }

                if(!vision.canSeePlayer)
                    waitReturnState -= Time.deltaTime;
                if (waitReturnState <= 0)
                {
                    if (timeRemaining < 4.8f)
                    {                      
                        EnterState(State.Idle);
                    }
                    else
                    {
                        
                        agent.SetDestination(currentWaypoint.position);
                        if (Vector3.Distance(transform.position, currentWaypoint.position) >= 1)
                        {
                            agent.speed = sprintSpeed;
                        }
                        else if (Vector3.Distance(transform.position, currentWaypoint.position) <= 0.1)
                            EnterState(State.Search);
                    }
                }

                break;
            case State.Chase:

                if (!vision.canSeePlayer)
                {
                    _currentOutOfSightDuration += Time.deltaTime;
                    if (_currentOutOfSightDuration >= OutOfSightDuration)
                    {
                        agent.SetDestination(transform.position);
                        EnterState(State.CanSee);
                    }
                }
                else
                {
                    agent.SetDestination(vision.playerRef.transform.position);
                    _currentOutOfSightDuration = 0;
                }

                if(Vector3.Distance(transform.position, vision.playerRef.transform.position) < range)
                {
                    agent.SetDestination(transform.position);
                    Debug.Log("You Die");
                }

                break;
        }
    }

    void ExitState()
    {
        switch (currentState)
        {
            case State.Idle:
                break;
            case State.Search:
                break;
            case State.CanSee:
                waitReturnState = 5;
                break;
            case State.Chase:
                break;
        }
    }

    void RotateHead()
    {
        if (isRotate && !vision.canSeePlayer)
        {
            float movement = moveSpeed * Time.deltaTime * direction;
            Vector3 newOffset = multiAimConstraint.data.offset + new Vector3(movement, 0, 0);

            // ตรวจสอบการชนขอบเขต
            if (newOffset.x <= minPos.x || newOffset.x >= maxPos.x)
            {
                direction *= -1f; // กลับทิศทางเมื่อชนขอบเขต
            }
            else
            {
                multiAimConstraint.data.offset = newOffset;
            }
        }
        else
        {
            multiAimConstraint.data.offset = startPos;
        }
    }

    void RotateTowardsWaypoint()
    {
        if (!isRotate && currentWaypoint != null) 
        {
            Vector3 directionToWaypoint = currentWaypoint.position - transform.position;
            directionToWaypoint.y = 0f;

            Quaternion targetRotation = Quaternion.LookRotation(directionToWaypoint);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(showGUI)
        {
            Handles.color = Color.red;
            Handles.DrawWireArc(transform.position + Vector3.up * GUIOffset, Vector3.up, Vector3.forward, 360, range);
        }
    }
#endif
}

public enum State
{
    Idle,
    Search,
    CanSee,
    Chase
}

[System.Serializable]
public class StrogeWaypoint
{
    public List<Transform> waypoint;
}
