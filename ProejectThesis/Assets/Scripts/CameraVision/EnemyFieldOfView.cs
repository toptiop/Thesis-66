using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif



[ExecuteInEditMode]
public class EnemyFieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;
    public float heightOffset = 0.0f;
    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    [Header("GUI")]
    [SerializeField] private bool showGUI = true;
    private void Start()
    {
        playerRef = GameObject.Find("TargetHit");
        StartCoroutine(FOVRoutine());
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
            Vector3 directionToTarget = (target.position - (transform.position + Vector3.up * heightOffset)).normalized;

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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(showGUI)
        {
            Handles.color = Color.white;
            Handles.DrawWireArc(transform.position + Vector3.up * heightOffset, Vector3.up, Vector3.forward, 360, radius);

            Vector3 viewAngle01 = DirectionFromAngle(transform.eulerAngles.y, -angle / 2);
            Vector3 viewAngle02 = DirectionFromAngle(transform.eulerAngles.y, angle / 2);

            Handles.color = Color.yellow;
            Handles.DrawLine(transform.position + Vector3.up * heightOffset, transform.position + viewAngle01 * radius + Vector3.up * heightOffset);
            Handles.DrawLine(transform.position + Vector3.up * heightOffset, transform.position + viewAngle02 * radius + Vector3.up * heightOffset);

            if (canSeePlayer)
            {
                Handles.color = Color.green;
                Handles.DrawLine(transform.position + Vector3.up * heightOffset, playerRef.transform.position);
            }
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
#endif
}
