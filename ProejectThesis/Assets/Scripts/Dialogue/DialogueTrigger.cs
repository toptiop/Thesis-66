using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private NPCConversation conversation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(conversation != null)
            {
                ConversationManager.Instance.StartConversation(conversation);
            }
        }
    }
}
