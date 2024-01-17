using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

    public PlayerController controller;
    public InputManager inputManager;
    public Inventory inventory;
    public CameraFollow cameraFollow;
    public DialogueManager dialogueManager;
    public Detection detection;
    public QuestManager quest;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        controller = FindAnyObjectByType<PlayerController>();
        inputManager = FindAnyObjectByType<InputManager>();
        inventory = FindAnyObjectByType<Inventory>();
        cameraFollow = FindAnyObjectByType<CameraFollow>();
        dialogueManager = FindAnyObjectByType<DialogueManager>();
        detection = FindAnyObjectByType<Detection>();
        quest = FindAnyObjectByType<QuestManager>();
    }
}
