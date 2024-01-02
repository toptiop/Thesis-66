using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private void StartDialogue()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.StartDialogue(dialogue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartDialogue();
            Debug.Log("DialogueTrigger");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.EndDialogue();
            Debug.Log("DialogueTrigger");
        }
    }
}
