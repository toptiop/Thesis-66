using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public int questIndex;

    public TMP_Text title;
    public TMP_Text description;

    //public Button completeBTN;

    public void SetValue(QuestTracker tracker, int index)
    {
        gameObject.SetActive(true);

        questIndex = index;

        title.text = tracker.questName;
        description.text = tracker.questDescription;

        // completeBTN.interactable = tracker.questCanComplete;
    }

    public void UpdateProgress(bool canComplete)
    {
        //Update currentAmount

        //completeBTN.interactable = canComplete;
    }

    public void CompleteQuest()
    {
        QuestManager.instance.CompleteQuest(questIndex);
        gameObject.SetActive(false);
    }

    public void CancelQuest()
    {
        QuestManager.instance.CancelQuest(questIndex);
        gameObject.SetActive(false);
    }
}
