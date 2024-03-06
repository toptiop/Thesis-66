using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public SO_Quest quest;
    public SO_Quest quest2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Accept : " + quest);
            other.GetComponent<QuestManager>().AcceptQuest(quest);
        }
    }
    private void OnGUI()
    {
        if (GUILayout.Button("quest1"))
        {
            Debug.Log("Accept : " + quest);
            QuestManager.instance.AcceptQuest(quest);
        }

        if (GUILayout.Button("quest2"))
        {
            Debug.Log("Accept : " + quest2);
            QuestManager.instance.AcceptQuest(quest2);
        }
    }
}
