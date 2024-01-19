using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Create new Quest/Quest")]
public class SO_Quest : ScriptableObject
{
    [Header("Preset")]
    public QuestType type;
    //Base Quest
    [Header("GENERAL DETIALS")]
    public string title;
    public string description;
    public Sprite icon;
    public SO_Item Reward;
    public int amountReward;
    public SO_Quest NextQuest;
    public enum QuestType {Collect, GO_Find};

    [Header("CollectQuest DETIALS")]
    public List<Collecter> itemCollect;

    [Header("FindQuest DETIALS")]
    public bool activeWaypoint;
    public Vector3 destination; // จุดที่ผู้เล่นต้องไปหา 

   
}

[Serializable]
public class Collecter
{
    public int currentItemCount;
    public int requiredItemCount; // จำนวนของที่ผู้เล่นต้องเก็บ
    public SO_Item collectItem; // ไอเท็มที่ต้องเก็บ
}
