using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public QuestTracker[] OngoingQuest;
    public QuestUI[] OngoingQuestUI;

    public QuestWaypoint waypoint;
    public float distanceToDestinationRang = 2f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        OngoingQuest = new QuestTracker[OngoingQuestUI.Length];
    }

    public void AcceptQuest(SO_Quest quest)
    {
        foreach (QuestTracker tracker in OngoingQuest)
        {
            if (tracker.trackerQuest == null)
                continue;

            if (tracker.trackerQuest == quest)
            {
                Debug.Log("You already accepter this quest");
                return;
            }
        }

        bool isFull = true;

        for (int i = 0; i < OngoingQuest.Length; i++)
        {
            if (OngoingQuest[i].trackerQuest == null)
            {
                OngoingQuest[i] = new QuestTracker(quest);
                isFull = false;

                //UpdateUI;
                OngoingQuestUI[i].SetValue(OngoingQuest[i], i);
                break;
            }
        }

        if (isFull)
        {
            Debug.Log("Cannot accept more quest");
        }
        else
        {
            Debug.Log("The quest \"" + quest.questName + "\" has been accepted");
        }
    }

    public void CancelQuest(int index)
    {
        if (OngoingQuest[index].trackerQuest != null)
        {
            Debug.Log("The quest \"" + OngoingQuest[index].questName + "\" has been accepted");

            OngoingQuest[index].trackerQuest = null;
        }
    }

    public void CompleteQuest(int index)
    {
        if (OngoingQuest[index].trackerQuest != null)
        {
            string itemList = "";

            if (OngoingQuest[index].trackerQuest.ItemReward.Count > 0)
            {
                foreach (SO_Item item in OngoingQuest[index].trackerQuest.ItemReward)
                {
                    if (item == null)
                        continue;

                    itemList = "Item Reward : " + item.itemName + "\n";
                }
            }
            Debug.Log(
               "The quest \"" + OngoingQuest[index].questName + "\" has been completed" + "\n" +
               "Gold reward \"" + OngoingQuest[index].trackerQuest.goldReward + "\n" +
               "Exp reward \"" + OngoingQuest[index].trackerQuest.expReward + "\n" +
               itemList
               );
            OngoingQuest[index].trackerQuest = null;
            OngoingQuestUI[0].gameObject.SetActive(false);
        }
    }

    public void UpdateQuestProgress(ObjectiveType type, ScriptableObject targetData)
    {
        if (OngoingQuest == null)
            return;

        for (int i = 0; i < OngoingQuest.Length; i++)
        {
            if (OngoingQuest[i].trackerQuest != null)
            {
                OngoingQuest[i].UpdateProgress(type, targetData);
                //UpdateUI
                OngoingQuestUI[i].UpdateProgress(OngoingQuest[i].questCanComplete);
            }
        }
    }
}
