using UnityEngine;
using UnityEditor;

public class SetItemTransform : MonoBehaviour
{
    public string uniqueId;

    public ItemTransform itemTransform = new ItemTransform();

    public DoorActive active;
    public DoorKey doorKey;
    public DoorHack hack;
    // Update is called once per frame
    void Update()
    {
        itemTransform.uniqueId = uniqueId;  
        itemTransform.Position = transform.position;

        Quaternion quaternion = transform.rotation;
        itemTransform.Rotation = quaternion;    
    }

    private void Reset()
    {
        uniqueId = System.Guid.NewGuid().ToString();
        itemTransform.uniqueId = uniqueId;
    }

    private void ResetId()
    {
        uniqueId = System.Guid.NewGuid().ToString();
        itemTransform.uniqueId = uniqueId;
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(SetItemTransform))]
    private class ScriptableObjectIdEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var idObject = target as SetItemTransform;

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Force Reset ID", GUILayout.Width(120)))
            {
                idObject.ResetId();
            }
            GUILayout.EndHorizontal();
        }
    }

#endif
}
