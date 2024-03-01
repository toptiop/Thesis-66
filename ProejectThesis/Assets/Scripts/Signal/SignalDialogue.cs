using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalDialogue : MonoBehaviour
{
    public void StarterDialogue(NPCConversation conversation)
    {
        if(conversation != null)
        {
            ConversationManager.Instance.StartConversation(conversation);
        }
    }
}
