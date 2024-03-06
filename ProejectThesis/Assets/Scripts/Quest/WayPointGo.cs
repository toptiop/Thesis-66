using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointGo : MonoBehaviour
{
    public SO_WayPoint Point;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<QuestManager>().UpdateQuestProgress(ObjectiveType.Go, Point);
            Destroy(gameObject);
        }
    }
}
