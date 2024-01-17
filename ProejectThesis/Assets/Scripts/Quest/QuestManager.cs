using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public SO_Quest.QuestType currentQuestType;
    public List<SO_Quest> quests = new List<SO_Quest>();
    public GameObject questPanel;

    [Header("Setting")]
    public float distanceToDestinationRang = 2f;

    [Header("Script")]
    public Transform playerPosition;
    public Inventory inventory;
    public QuestWaypoint waypoint;

    [Header("UI Elements")]
    public TMP_Text questTitleText;
    public TMP_Text questDescriptionText;
    public TMP_Text questRewardText;
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
    }

    private void Start()
    {
        questPanel.SetActive(true);
        InitializeQuests();
        UpdateUIWithCurrentQuest();
        DebugCurrentQuest();
    }
    private void Update()
    {
        CheckQuestStatus();
    }

    void InitializeQuests()
    {
        if (quests.Count > 0)
        {
            currentQuestType = quests[0].type;
        }
        StartCurrentQuest();
    }

    void StartCurrentQuest()
    {
        if (quests.Count > 0)
        {
            SO_Quest currentQuest = quests[0];
            Debug.Log("Starting Quest: " + currentQuest.title);

            // Optionally, you might want to handle other quest-related logic here

            // Check if the quest has a NextQuest defined
            if (currentQuest.NextQuest != null)
            {
                // Optionally, you might want to handle transitions or triggers for quest sequences
                Debug.Log("Next Quest: " + currentQuest.NextQuest.title);
            }
        }
    }
    void CheckQuestStatus()
    {
       // List<SO_Quest> questsCopy = new List<SO_Quest>(quests);
        foreach (SO_Quest quest in quests)
        {
            if (quest.type == currentQuestType)
            {
                switch (quest.type)
                {
                    case SO_Quest.QuestType.Speak:
                        SpeakQuest(quest);
                        break;

                    case SO_Quest.QuestType.Collect:
                        CollectQuest(quest);
                        break;

                    case SO_Quest.QuestType.GO_Find:
                        GoFindQuest(quest as SO_GO_FindQuest);                        
                        break;
                }
            }
        }
    }
    void SpeakQuest(SO_Quest quest)
    {

    }

    void CollectQuest(SO_Quest quest)
    {
        SO_CollectQuest collectQuest = quest as SO_CollectQuest;

        if (collectQuest != null)
        {
            foreach (Collecter collecter in collectQuest.itemCollect)
            {
                // Check if the required items are collected
                if (inventory.CheckInventoryForItems(collecter.collectItem, collecter.requiredItemCount))
                {
                    // Items collected for this Collecter
                    Debug.Log("Collected " + collecter.currentItemCount + " out of " + collecter.requiredItemCount + " " + collecter.collectItem.itemName);

                    // Update the current item count
                    collecter.currentItemCount += collecter.requiredItemCount;

                    // Debug the updated current item count
                    Debug.Log("Updated currentItemCount: " + collecter.currentItemCount);

                    // Check if the current item count meets the required count
                    if (collecter.currentItemCount >= collecter.requiredItemCount)
                    {
                        Debug.Log("CollectQuest Completed for " + collecter.collectItem.itemName);

                        // Optionally, you might want to handle other completion-related logic here
                        CompleteQuest(quest);
                    }
                }
                else
                {
                    Debug.Log("CollectQuest Incomplete - Required items not found in the inventory for " + collecter.collectItem.itemName);
                }
            }
        }
    }



    void GoFindQuest(SO_GO_FindQuest quest)
    {
        waypoint.SetPoint(quest.destination);
        waypoint.ToggleWaypoint(quest.activeWaypoint);
        float distanceToDestination = Vector3.Distance(playerPosition.position, quest.destination);

        if(distanceToDestination < distanceToDestinationRang)
        {
            Debug.Log("GO_Find");
            CompleteQuest(quest);
        }
    }

    void CompleteQuest(SO_Quest quest)
    {
        int questIndex = quests.IndexOf(quest);
        if (quest.NextQuest != null)
        {
            // Optionally, you might want to handle transitions or triggers for quest sequences
            Debug.Log("Next Quest: " + quest.NextQuest.title);
            quests.Add(quest.NextQuest);
        }

        if (questIndex != -1)
        {           
            Debug.Log("Quest Completed: " + quest.title);

            // Remove the completed quest from the list
            quests.RemoveAt(questIndex);

            // Check if there are more quests remaining
            if (quests.Count > 0)
            {
                // Set the current quest type to the next quest's type
                currentQuestType = quests[0].type;

                // Optionally, you might want to call a function to handle quest rewards or other completion-related logic.
            }
            else
            {
                // If there are no more quests, you can reset or handle the quest manager state accordingly
                currentQuestType = SO_Quest.QuestType.Speak; // Set a default type or handle as needed
                Debug.Log("All quests completed!");
            }
        }
        else
        {
            Debug.LogWarning("Quest not found in the list!");
        }

        if (currentQuestType != SO_Quest.QuestType.GO_Find)
            waypoint.ToggleWaypoint(false);

        UpdateUIWithCurrentQuest();
        DebugCurrentQuest();
    }

    void UpdateUIWithCurrentQuest()
    {
        if (quests.Count > 0)
        {
            SO_Quest currentQuest = quests[0];

            questTitleText.text =   currentQuest.title;
            questDescriptionText.text =   currentQuest.description;

            if (currentQuest.Reward != null)
            {
               // questRewardText.text = "Reward: " + currentQuest.amountReward + " " + currentQuest.Reward.itemName;
            }
            else
            {
               // questRewardText.text = "Reward: None";
            }

            // Update other UI elements as needed
        }
        else
        {
            // No quests available, clear the UI
            questTitleText.text = "No Active Quest";
            questDescriptionText.text = "";
            //questRewardText.text = "";
            // Clear other UI elements as needed
        }
    }

    void DebugCurrentQuest()
    {
        if (quests.Count > 0)
        {
            SO_Quest currentQuest = quests[0];
            Debug.Log("Current Quest : " + currentQuest.name);
            Debug.Log("Current Quest Type: " + currentQuest.type);
            Debug.Log("Current Quest Title: " + currentQuest.title);
            Debug.Log("Current Quest Description: " + currentQuest.description);
            // Add more debug statements for other quest details as needed
        }
        else
        {
            Debug.Log("No Active Quest");
        }
    }
}
