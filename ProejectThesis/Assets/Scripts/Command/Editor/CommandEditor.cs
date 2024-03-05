using UnityEngine;
using Cinemachine.Editor;

#if UNITY_EDITOR
using UnityEditor;
#endif


#if UNITY_EDITOR
[CustomEditor(typeof(Command))]
public class CommandEditor : Editor
{
    private SerializedProperty commandTypeProperty;


    //Door
    private SerializedProperty doorTyperProperty;

    //Door Hacking
    private SerializedProperty doorHackingTypeProperty;
    private SerializedProperty hackingTypeProperty;

    //Active Switch
    private SerializedProperty activeSwitchTypeProperty;
    private SerializedProperty switchTypeProperty;

    //Share Power
    private SerializedProperty sharePowerTypeProperty;
    private SerializedProperty shareTypeProperty;

    private void OnEnable()
    {
        commandTypeProperty = serializedObject.FindProperty("typeCommand");

        //Door Hacking
        doorHackingTypeProperty = serializedObject.FindProperty("doorHacking");
        hackingTypeProperty = serializedObject.FindProperty("isHacking");

        //Active Switch
        activeSwitchTypeProperty = serializedObject.FindProperty("door");
        switchTypeProperty = serializedObject.FindProperty("isActiveObj");

        //Share Power
        sharePowerTypeProperty = serializedObject.FindProperty("sharePowerToObj");
        shareTypeProperty = serializedObject.FindProperty("isShare");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(commandTypeProperty);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("setPos"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("setRotation"));
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("GroundedOffset"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("GroundedRadius"));
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("isRobot"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layerRobot"));


        EditorGUILayout.Space();
        
         Commander commandType = (Commander)commandTypeProperty.enumValueIndex;

        if(commandType == Commander.HackDoor)
        {
            EditorGUILayout.PropertyField(doorHackingTypeProperty, new GUIContent("Door Hacking"));
            EditorGUILayout.PropertyField(hackingTypeProperty, new GUIContent("Is Hacking"));
        }
        else if(commandType == Commander.ActiveSwitch)
        {
            EditorGUILayout.PropertyField(activeSwitchTypeProperty, new GUIContent("Active"));
            EditorGUILayout.PropertyField(switchTypeProperty, new GUIContent("Is Active"));
        }
        else if(commandType == Commander.SharePower)
        {
            EditorGUILayout.PropertyField(sharePowerTypeProperty, new GUIContent("Share Power"));
            EditorGUILayout.PropertyField(shareTypeProperty, new GUIContent("Is Share"));
        }

        //Set null;
         if (commandType != Commander.HackDoor)
        {
            doorHackingTypeProperty.objectReferenceValue = null;
            hackingTypeProperty.boolValue = false;
        }
        else if (commandType != Commander.ActiveSwitch)
        {
            activeSwitchTypeProperty.objectReferenceValue = null;
        }
        else if (commandType != Commander.SharePower)
        {
            sharePowerTypeProperty.objectReferenceValue = null;
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("timeToActive"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("imgActive"));

        serializedObject.ApplyModifiedProperties();
    }
}
#endif