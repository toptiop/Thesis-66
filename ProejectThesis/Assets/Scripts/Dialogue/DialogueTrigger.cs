using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private List<dialogueString> dialogueStrings = new List<dialogueString>();
    [SerializeField] private Transform NPCTransfrom;

    private bool hasSpoken = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !hasSpoken)
        {
            other.gameObject.GetComponent<DialogueManager>().DialogueStart(dialogueStrings, NPCTransfrom);
            hasSpoken = true;
        }
    }
}

[System.Serializable]
public class dialogueString
{
    public string text; //Represent the text that the npc says.
    public bool isEnd; // Represent if the line is the final line for the conversation.

    [Header("Branch")]
    public bool isQuestion;
    public string answeroption1;
    public string answeroption2;
    public int option1Indexjump;
    public int option2Indexjump;

    [Header("Triggered Events")]
    public UnityEvent startDialogueEvent;
    public UnityEvent endDialogueEvent;
}