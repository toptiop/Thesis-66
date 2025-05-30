using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

    public static PlayerController controller;
    public static AutoPilotRobot ai;
    public PlayerStatus status;
    public InputManager inputManager;
    public Inventory inventory;
    public InventoryUI invUI;
    public InventoryNote inventoryNote;
    public InventoryVideo inventoryVideo;
    public CameraFollow cameraFollow;
    public Detection detection;
    public QuestManager quest;
    public ActionUI actionUI;
    public switchcharacters switchcharacters;
    public ItemInGameManager itemInGameManager;
    public CheckPoint checkPoint;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        #region FindScript
        controller = FindAnyObjectByType<PlayerController>();
        ai = FindAnyObjectByType<AutoPilotRobot>();
        status = FindAnyObjectByType<PlayerStatus>();
        inputManager = FindAnyObjectByType<InputManager>();
        inventory = FindAnyObjectByType<Inventory>();
        inventoryNote = FindAnyObjectByType<InventoryNote>();
        inventoryVideo = FindAnyObjectByType<InventoryVideo>();
        cameraFollow = FindAnyObjectByType<CameraFollow>();
        detection = FindAnyObjectByType<Detection>();
        quest = FindAnyObjectByType<QuestManager>();
        actionUI = FindAnyObjectByType<ActionUI>();
        switchcharacters = FindAnyObjectByType<switchcharacters>();
        itemInGameManager = FindObjectOfType<ItemInGameManager>();
        checkPoint = FindObjectOfType<CheckPoint>();
        invUI = FindObjectOfType<InventoryUI>();
        #endregion
    }
}
