using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

    public static PlayerController controller;
    public InputManager inputManager;
    public Inventory inventory;
    public CameraFollow cameraFollow;
    public Detection detection;
    public QuestManager quest;
    public ActionUI actionUI;
    public switchcharacters switchcharacters;
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

        #region FindScript
        controller = FindAnyObjectByType<PlayerController>();
        inputManager = FindAnyObjectByType<InputManager>();
        inventory = FindAnyObjectByType<Inventory>();
        cameraFollow = FindAnyObjectByType<CameraFollow>();
        detection = FindAnyObjectByType<Detection>();
        quest = FindAnyObjectByType<QuestManager>();
        actionUI = FindAnyObjectByType<ActionUI>();
        switchcharacters = FindAnyObjectByType<switchcharacters>();
        #endregion
    }
}
