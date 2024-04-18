using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyFieldOfView))]
public class CameraTwo : MonoBehaviour
{
    [Header("State Camera")]
    public State currentState;
    public enum State { Search, LockOn };

    [Header("Setting vision")]
    public float visionRange = 10;
    public float visionAngle = 360f;

    [Header("Time Active")]
    public float timeActive = 5;
    public float timeSee = 5;
    public bool active = true;
    [SerializeField] private float timer;
    [SerializeField] private float time;

    [Header("Lighting")]
    public Light light;

    private EnemyFieldOfView vision;
    [SerializeField]private VisionCone cone;

    private void Awake()
    {
        vision = GetComponent<EnemyFieldOfView>();
   
    }
    private void Start()
    {
        timer = timeActive;
        time = timeSee;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    void EnterState(State state)
    {
        ExitState();
        currentState = state;

        switch (currentState)
        {
            case State.Search:
                break;
            case State.LockOn:
               
                break;
        }
    }

    void UpdateState()
    {
        switch (currentState)
        {
            case State.Search:

                if (vision.canSeePlayer)
                    EnterState(State.LockOn);

                timer -= Time.deltaTime;

                if (timer <= 0 && !vision.canSeePlayer)
                {
                    active = !active; // Toggle the active state
                    vision.enabled = active;
                    vision.visionActive = active;
                    light.enabled = active;
                    cone.gameObject.SetActive(active);
                    timer = timeActive;
                }

                break;
            case State.LockOn:

                if (vision.canSeePlayer)
                {
                    time -= Time.deltaTime;
                    if (time <= 0)
                    {
                        StartCoroutine(Singleton.Instance.status.DIE());
                        time = timeSee;
                        EnterState(State.Search);
                    }

                }
                else
                {
                    time = timeSee;
                }
                break;
        }
    }
    void ExitState()
    {
        switch (currentState)
        {
            case State.Search:
                break;
            case State.LockOn:
                break;
        }
    }
}