using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(EnemyFieldOfView))]
public class FieldOfViewEditor : Editor
{
 /* private void OnSceneGUI()
    {
        EnemyFieldOfView fov = (EnemyFieldOfView)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position + Vector3.up * fov.heightOffset, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position + Vector3.up * fov.heightOffset, fov.transform.position + viewAngle01 * fov.radius + Vector3.up * fov.heightOffset);
        Handles.DrawLine(fov.transform.position + Vector3.up * fov.heightOffset, fov.transform.position + viewAngle02 * fov.radius + Vector3.up * fov.heightOffset);

        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position + Vector3.up * fov.heightOffset, fov.playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }*/
}
#endif