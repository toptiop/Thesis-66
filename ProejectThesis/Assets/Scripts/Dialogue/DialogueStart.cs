using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueStart : MonoBehaviour
{
    public NPCConversation conversation;
    void Start()
    {
        ConversationManager.Instance.StartConversation(conversation);
    }

  
}
