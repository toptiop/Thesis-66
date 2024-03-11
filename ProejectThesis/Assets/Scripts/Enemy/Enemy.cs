using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] State currentState = State.Search;
    #region CanSee
    [Tooltip("Setting Range")]
    [Header("Angle Can See Player")]
    public float radius;
    [Range(0, 360)]
    public float angle;
    #endregion
    [Space,Header("Ref Player")]
    public GameObject playerRef;

    [Space,Header("Layer")]
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    [Header("Waypoint")]
    public List<Transform> SearchWaypoints;
    int _currentWaypoint;

    [Header("Animator"),SerializeField]
    private Animator animator;

   
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerRef = GameObject.Find("TargetHit");
        StartCoroutine(FOVRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if(SearchWaypoints.Count > 0)
        {
            MoveToWayPoint();
        }

        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    void UpdateState()
    {
        switch (currentState)
        {
            case State.Idle:
                break;
            case State.Search:
                break;
            case State.Chase:
                break;
        }
    }
    void MoveToWayPoint()
    {
        Vector3 waypointVector = transform.position - agent.destination;

        if(waypointVector.magnitude <= 1)
        {
            //_currentWaypoint = Random.Range(0,SearchWaypoints.Count);

            StartCoroutine(DelayToMove());

            agent.SetDestination(SearchWaypoints[_currentWaypoint].position);
        }
    }

    IEnumerator DelayToMove()
    {
        yield return new WaitForSeconds(2);

        _currentWaypoint = Random.Range(0, SearchWaypoints.Count);
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
}

public enum State
{
    Idle,
    Search,
    Chase
}
