#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaypointGroup))]
public class WaypointGroupEditor : Editor
{

    private SerializedProperty colorProperty;

    private void OnEnable()
    {
        colorProperty = serializedObject.FindProperty("color");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WaypointGroup waypointGroup = (WaypointGroup)target;        

        GUILayout.Space(10);

        if (GUILayout.Button("Create Waypoint"))
        {
            waypointGroup.CreateNewWaypoint();
        }

        if (GUILayout.Button("Duplicate Selected"))
        {
            DuplicateSelectedWaypoint(waypointGroup);
        }

        if (GUILayout.Button("Remove Selected Waypoint"))
        {
            RemoveSelectedWaypoint(waypointGroup);
        }
        var iconContent = EditorGUIUtility.IconContent("Animation.Record");
        
        for(int i = 0; waypointGroup.waypoints.Count > i; i++)
        {
            if(waypointGroup.waypoints.Count > 0)
            {
                EditorGUIUtility.SetIconForObject(waypointGroup.waypoints[i].gameObject, (Texture2D)iconContent.image);
            }
        }

        EditorGUILayout.Space();

        serializedObject.ApplyModifiedProperties();
    }

    public void DuplicateSelectedWaypoint(WaypointGroup waypointGroup)
    {
        Debug.Log("Duplicating selected Waypoint...");

        // Check if any waypoints are selected
        if (Selection.activeGameObject != null && waypointGroup.waypoints.Contains(Selection.activeGameObject.transform))
        {
            // Get the selected waypoint GameObject
            GameObject selectedWaypoint = Selection.activeGameObject;

            // Duplicate the selected waypoint GameObject
            GameObject duplicatedWaypoint = Instantiate(selectedWaypoint, selectedWaypoint.transform.position, selectedWaypoint.transform.rotation);

            // Rename the duplicated waypoint
            duplicatedWaypoint.name = selectedWaypoint.name;

            // Set the duplicated waypoint's parent to the current WaypointGroup
            duplicatedWaypoint.transform.parent = waypointGroup.transform;

            // Add the duplicated waypoint's transform to the waypoints list
            waypointGroup.waypoints.Add(duplicatedWaypoint.transform);

            Debug.Log("Selected Waypoint duplicated.");
        }
        else
        {
            Debug.LogWarning("Select a waypoint in the scene to duplicate.");
        }
    }

    private void RemoveSelectedWaypoint(WaypointGroup waypointGroup)
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        foreach (GameObject selectedObject in selectedObjects)
        {
            if (selectedObject != null && waypointGroup.waypoints.Contains(selectedObject.transform))
            {
                waypointGroup.waypoints.Remove(selectedObject.transform);
                DestroyImmediate(selectedObject);
                Debug.Log("Waypoint removed.");
            }
            else
            {
                Debug.LogWarning("Select a waypoint in the scene to remove.");
            }
        }
    }
}
#endif