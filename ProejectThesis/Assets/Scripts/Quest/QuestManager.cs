using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public SO_Quest.QuestType currentQuestType;
    public List<SO_Quest> quests = new List<SO_Quest>();

    [Header("Script")]
    public Transform playerPosition;
    public Inventory inventory;
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
        InitializeQuests();
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
    }
    void CheckQuestStatus()
    {
        List<SO_Quest> questsCopy = new List<SO_Quest>(quests);
        foreach (SO_Quest quest in questsCopy)
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
            if (inventory.CheckInventoryForItems(collectQuest.collectItem, collectQuest.requiredItemCount))
            {
                Debug.Log("CollectQuest Completed!");
                CompleteQuest(quest);
            }
            else
            {
                Debug.Log("CollectQuest Incomplete - Required items not found in the inventory.");
            }
        }
    }

    void GoFindQuest(SO_GO_FindQuest quest)
    {
        float distanceToDestination = Vector3.Distance(playerPosition.position, quest.destination);

        if(distanceToDestination < 2.0)
        {
            Debug.Log("GO_Find");
            CompleteQuest(quest);
        }
    }

    void CompleteQuest(SO_Quest quest)
    {
        int questIndex = quests.IndexOf(quest);

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
    }


}
