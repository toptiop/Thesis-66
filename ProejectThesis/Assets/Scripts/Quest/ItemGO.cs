using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGO : MonoBehaviour
{
    public void UpdateQuestProgress(ScriptableObject data)
    {
        QuestManager.instance.UpdateQuestProgress(ObjectiveType.Gather, data);
    }
}
