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
            QuestManager questManager = other.GetComponent<QuestManager>(); // Cache the QuestManager component

            if (questManager.OngoingQuest.Count > 0)
            {
                foreach (var quest in questManager.OngoingQuest)
                {
                    if (quest.trackerQuest != null)
                    {
                        foreach (var objective in quest.trackerQuest.objectives)
                        {
                            if (objective.targetDetail != null && objective.targetDetail == Point)
                            {
                                questManager.UpdateQuestProgress(ObjectiveType.Go, Point);
                                Destroy(gameObject);
                                return; // Exit the method after destroying the waypoint
                            }
                        }
                    }
                }
            }
        }
    }
}
