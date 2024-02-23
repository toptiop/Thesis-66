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
    [SerializeField]
    private InputManager playerInput;
    [SerializeField]
    private InputRobotManager robotInput;

    private void Start()
    {
        playerInput = FindAnyObjectByType<InputManager>();
        robotInput = FindAnyObjectByType<InputRobotManager>();

        textPanel.SetActive(false);
    }
    private void Update()
    {
        if (playerInput != null && robotInput != null && currentDialogue != null)
        {
            if (playerInput.next || robotInput.next)
            {
                DisplayNextEntry();
                playerInput.next = false;
                robotInput.next = false;
            }
            else if (playerInput.back || robotInput.back)
            {
                DisplayPreviousEntry();
                playerInput.back = false;
                robotInput.back = false;
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        GameManager.Instance.InteractUI = true;
        Singleton.controller.canMove = true;
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
        GameManager.Instance.InteractUI = false;
        Singleton.controller.canMove = false;
    }


}
