using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    //[SerializeField] State currentState = State.Search;
   
    [Header("Waypoint")]
    public List<StrogeWaypoint> waypointsPattern = new List<StrogeWaypoint>();
    public List<Transform> SearchWaypoints;
    [SerializeField] private int pattern;
    [SerializeField]private int _currentWaypoint;
    [SerializeField] private Transform currentWaypoint;
    [SerializeField] private float timeRemaining = 5;
    float timer;

    [Header("Head")]
    public bool isRotate = false;
    public GameObject head;
    public float moveSpeed = 2f; // ความเร็วในการเคลื่อนที่

    private Vector3 startPos; // ตำแหน่งเริ่มต้น
    public Vector3 minPos; // ตำแหน่งขอบเขตต่ำสุด
    public Vector3 maxPos; // ตำแหน่งขอบเขตสูงสุด

    private float direction = 1f;

    [Header("Animator"),SerializeField]
    private Animator animator;

    [SerializeField]private EnemyFieldOfView enemy;
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponentInChildren<EnemyFieldOfView>();

        startPos = head.transform.localPosition;
        timer = timeRemaining;
        pattern = Random.Range(0, waypointsPattern.Count);
    }

    // Update is called once per frame
    void Update()
    {
        currentWaypoint = waypointsPattern[pattern].waypoint[_currentWaypoint];

        if(timeRemaining <= 4.5f)
            isRotate = true;
        else
            isRotate = false;

        MoveToWayPoint();
        RotateHead();

        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }


    void ChasePlayer()
    {
        if (!enemy.canSeePlayer)
        {

        }
    }

    void MoveToWayPoint()
    {
        Vector3 waypointVector = transform.position - agent.destination;

        if(waypointVector.magnitude <= 1)
        {
            //_currentWaypoint = Random.Range(0,SearchWaypoints.Count);

            //StartCoroutine(DelayToMove());
           
            timeRemaining -= Time.deltaTime;
            if ((timeRemaining <= 0))
            {
               
                _currentWaypoint++;
                if(_currentWaypoint >= waypointsPattern[pattern].waypoint.Count)
                {
                    pattern = Random.Range(0, waypointsPattern.Count);
                    _currentWaypoint = 0;
                }
                //if (_currentWaypoint >= SearchWaypoints.Count)
                //{
                //    pattern = Random.Range(0, waypoints.Count);
                //    _currentWaypoint = 0;
                //}

                timeRemaining = timer;
            }
            

            agent.SetDestination(waypointsPattern[pattern].waypoint[_currentWaypoint].position);

        }
    }

    void RotateHead()
    {
        if(isRotate)
        {
            float movement = moveSpeed * Time.deltaTime * direction;
            head.transform.localPosition += new Vector3(movement, 0, 0);

            // ตรวจสอบการชนขอบเขต
            if (head.transform.localPosition.x <= minPos.x || head.transform.localPosition.x >= maxPos.x)
            {
                direction *= -1f; // กลับทิศทางเมื่อชนขอบเขต
            }
        }
        else
        {
            head.transform.localPosition = startPos;
        }
    }

}

public enum State
{
    Idle,
    Search,
    Chase
}

[System.Serializable]
public class StrogeWaypoint
{
    public List<Transform> waypoint;
}
