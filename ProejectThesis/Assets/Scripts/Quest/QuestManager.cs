using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public List<QuestTracker> OngoingQuest;
    public List<QuestUI> OngoingQuestUI;

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

        if (OngoingQuestUI == null)
        {
            Debug.LogError("OngoingQuestUI list is not assigned!");
        }
    }

    private void Start()
    {
        OngoingQuest = new List<QuestTracker>();
    }

    public void AcceptQuest(SO_Quest quest)
    {
        foreach (QuestTracker tracker in OngoingQuest)
        {
            if (tracker.trackerQuest == quest)
            {
                Debug.Log("You already accepted this quest");
                return;
            }
        }

        if (OngoingQuest.Count < OngoingQuestUI.Count)
        {
            QuestTracker newQuestTracker = new QuestTracker(quest);
            OngoingQuest.Add(newQuestTracker);

            int newIndex = OngoingQuest.IndexOf(newQuestTracker);
            OngoingQuestUI[newIndex].SetValue(newQuestTracker, newIndex);

            Debug.Log("The quest \"" + quest.questName + "\" has been accepted");
        }
        else
        {
            Debug.Log("Cannot accept more quests");
        }
    }

    public void CancelQuest(int index)
    {
        if (index >= 0 && index < OngoingQuest.Count)
        {
            Debug.Log("The quest \"" + OngoingQuest[index].questName + "\" has been canceled");
            OngoingQuest.RemoveAt(index);
            OngoingQuestUI[index].gameObject.SetActive(false);
        }
    }

    public void CompleteQuest(int index)
    {
        if (index >= 0 && index < OngoingQuest.Count)
        {
            string itemList = "";

            if (OngoingQuest[index].trackerQuest.ItemReward.Count > 0)
            {
                foreach (SO_Item item in OngoingQuest[index].trackerQuest.ItemReward)
                {
                    if (item == null)
                        continue;

                    itemList += "Item Reward : " + item.itemName + "\n";
                }
            }

            Debug.Log(
               "The quest \"" + OngoingQuest[index].questName + "\" has been completed" + "\n" +
               "Gold reward \"" + OngoingQuest[index].trackerQuest.goldReward + "\n" +
               "Exp reward \"" + OngoingQuest[index].trackerQuest.expReward + "\n" +
               itemList
               );

            OngoingQuest.RemoveAt(index);
            OngoingQuestUI[index].gameObject.SetActive(false);
        }
    }

    public void UpdateQuestProgress(ObjectiveType type, ScriptableObject targetData)
    {
        if (OngoingQuest == null)
            return;

        for (int i = 0; i < OngoingQuest.Count; i++)
        {
            if (OngoingQuest[i].trackerQuest != null)
            {
                OngoingQuest[i].UpdateProgress(type, targetData);
                //OngoingQuestUI[i].UpdateProgress(OngoingQuest[i].questCanComplete);
            }
        }
    }
}
