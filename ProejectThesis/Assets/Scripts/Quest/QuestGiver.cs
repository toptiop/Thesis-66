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
}
