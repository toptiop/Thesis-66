using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif


[ExecuteInEditMode]
public class WaypointGroup : MonoBehaviour
{
    public Color color = Color.white;
    [HideInInspector]public List<Transform> waypoints = new List<Transform>();

    public bool showGUI = true;

    private void Reset()
    {
        foreach (Transform t in waypoints)
        {
            Destroy(t);
            waypoints.Clear();
        }
        while (waypoints.Count < 4)
        {
            CreateNewWaypoint();
        }

        waypoints[0].transform.localPosition = new Vector3(1, 0, 0);
        waypoints[1].transform.localPosition = new Vector3(-1, 0, 0);
        waypoints[2].transform.localPosition = new Vector3(0, 0, 1);
        waypoints[3].transform.localPosition = new Vector3(0, 0, -1);
    }

    public void CreateNewWaypoint()
    {
        Debug.Log("New WayPoint");
        GameObject newWaypoint = new GameObject("waypoint");
        newWaypoint.transform.parent = transform;
        newWaypoint.transform.localPosition = new Vector3(0, 0, 0);
        waypoints.Add(newWaypoint.transform);
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (showGUI)
        {
            if (waypoints == null || waypoints.Count < 2)
                return;

            Gizmos.color = color;

            // Draw lines between all waypoints for a complete connection
            for (int i = 0; i < waypoints.Count; i++)
            {
                for (int j = i + 1; j < waypoints.Count; j++)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[j].position);
                }
            }
        }
    }
#endif
}
