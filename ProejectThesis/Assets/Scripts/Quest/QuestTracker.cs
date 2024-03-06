using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestTracker
{
    [Header("Quest Data")]
    public SO_Quest trackerQuest;

    [Header("Quest Details")]
    public string questName;
    public string questDescription;
    public Objective[] objectives;
    public bool questCanComplete;

    public QuestTracker(SO_Quest questData)
    {
        trackerQuest = questData;

        questName = questData.questName;
        questDescription = questData.questDescription;

        objectives = new Objective[questData.objectives.Length];

        for (int i = 0; i < questData.objectives.Length; i++)
        {
            objectives[i] = new Objective(questData.objectives[i]);
        }

        foreach (Objective obj in objectives)
        {
            SO_WayPoint Point = obj.targetDetail as SO_WayPoint;
            if (Point != null)
            {
                QuestManager.instance.waypoint.ToggleWaypoint(true);
                QuestManager.instance.waypoint.SetPoint(Point.waypoint);
            }
        }

    }

    public void UpdateProgress(ObjectiveType type, ScriptableObject targetData)
    {
        if (objectives == null)
            return;

        foreach (Objective obj in objectives)
        {
            if (obj.type == type)
            {

                if (obj.targetDetail == targetData)
                {
                    obj.currentAmount++;
                    if (obj.currentAmount >= obj.requiredAmount)
                    {
                        obj.isCompleted = true;
                        CheckComplete();
                    }
                }

                if (obj.targetDetail == targetData)
                {
                    SO_WayPoint Point = obj.targetDetail as SO_WayPoint;
                    if (Point != null)
                    {
                        float distanceToDestination = Vector3.Distance(QuestManager.instance.waypoint.playerTransform.position, Point.waypoint);
                        if (distanceToDestination < QuestManager.instance.distanceToDestinationRang)
                        {
                            //obj.currentAmount++;
                            if (obj.currentAmount >= obj.requiredAmount)
                            {
                                QuestManager.instance.waypoint.ToggleWaypoint(false);
                                obj.isCompleted = true;
                                CheckComplete();
                            }
                        }
                    }
                }


            }
        }
    }

    public void CheckComplete()
    {
        bool allComplete = true;

        foreach (Objective obj in objectives)
        {
            if (!obj.isCompleted)
            {
                allComplete = false;
                break;
            }
        }

        if (!allComplete)
        {
            questCanComplete = false;
            Debug.Log("Cannot complete yet.");
        }
        else
        {
            QuestManager.instance.CompleteQuest(0);
            questCanComplete = true;
        }
    }
}
