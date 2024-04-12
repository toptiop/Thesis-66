using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGO : MonoBehaviour
{
    public bool isnextQuest;
    public SO_Quest nextQuest;
    public void UpdateQuestProgress(ScriptableObject data)
    {
        QuestManager.instance.UpdateQuestProgress(ObjectiveType.Gather, data);

        if (isnextQuest)
        {
            QuestManager.instance.AcceptQuest(nextQuest);
        }
    }

}
