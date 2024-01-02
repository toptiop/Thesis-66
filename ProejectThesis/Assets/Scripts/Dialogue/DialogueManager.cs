using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject textPanel;
    public TMP_Text speakerText;
    public TMP_Text messageText;

    [SerializeField]
    private int currentEntryIndex = 0;
    [SerializeField]
    private Dialogue currentDialogue;

    private void Start()
    {
        textPanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            DisplayNextEntry();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DisplayPreviousEntry();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        textPanel.SetActive(true);
        currentDialogue = dialogue;
        currentEntryIndex = 0;
        DisplayCurrentEntry();
    }

    public void DisplayNextEntry()
    {
        if (currentEntryIndex < currentDialogue.dialogueEntries.Length - 1)
        {
            currentEntryIndex++;
            DisplayCurrentEntry();
        }
        else
        {
            EndDialogue();
        }
    }

    public void DisplayPreviousEntry()
    {
        if (currentEntryIndex > 0)
        {
            currentEntryIndex--;
            DisplayCurrentEntry();
        }
    }

    private void DisplayCurrentEntry()
    {
        Dialogue.DialogueEntry entry = currentDialogue.dialogueEntries[currentEntryIndex];
        speakerText.text = entry.speaker;
        messageText.text = entry.message;
    }

    public void EndDialogue()
    {
        // You can implement additional logic here if needed
        textPanel.SetActive(false);
        Debug.Log("End of dialogue");
    }


}
