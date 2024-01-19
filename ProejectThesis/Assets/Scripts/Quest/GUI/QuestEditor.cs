using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(SO_Quest))]
public class QuestEditor : Editor
{
    private SerializedProperty questTypeProperty;
    private SerializedProperty itemCollectProperty;
    private SerializedProperty activeWaypointProperty;
    private SerializedProperty destinationProperty;

    private void OnEnable()
    {
        // Initialize serialized properties
        questTypeProperty = serializedObject.FindProperty("type");
        itemCollectProperty = serializedObject.FindProperty("itemCollect");
        activeWaypointProperty = serializedObject.FindProperty("activeWaypoint");
        destinationProperty = serializedObject.FindProperty("destination");
    }

    public override void OnInspectorGUI()
    {
        // Update the serialized object
        serializedObject.Update();

        // Display quest type
        EditorGUILayout.PropertyField(questTypeProperty);

        // Display general details
        EditorGUILayout.PropertyField(serializedObject.FindProperty("title"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("description"));
        //EditorGUILayout.PropertyField(serializedObject.FindProperty("icon"));
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Reward"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("amountReward"));

       

        // Check the quest type and show relevant details
        SO_Quest.QuestType questType = (SO_Quest.QuestType)questTypeProperty.enumValueIndex;
        if (questType == SO_Quest.QuestType.Collect)
        {
            EditorGUILayout.PropertyField(itemCollectProperty, new GUIContent("Item Collect"));
            // You can add more fields specific to CollectQuest if needed
        }
        else if (questType == SO_Quest.QuestType.GO_Find)
        {
            EditorGUILayout.PropertyField(activeWaypointProperty, new GUIContent("Active Waypoint"));
            EditorGUILayout.PropertyField(destinationProperty, new GUIContent("Destination"));
            // You can add more fields specific to FindQuest if needed
        }

        // Display next quest
        EditorGUILayout.PropertyField(serializedObject.FindProperty("NextQuest"));

        // Apply changes to the serialized object
        serializedObject.ApplyModifiedProperties();
    }
}
#endif