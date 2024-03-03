using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueIsTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private NPCConversation dialolgue;
    public string interactionText;

    public string GetInteractionText()
    {
        return interactionText;
    }

    public void Interact()
    {
        StartDialogue();
    }

    void StartDialogue()
    {
      ConversationManager.Instance.StartConversation(dialolgue);
    }
}
