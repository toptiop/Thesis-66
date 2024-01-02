using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Quest", menuName = "Create new Quest/Quest")]
public class SO_Quest : ScriptableObject
{
    //Base Quest
    [Header("GENERAL DETIALS")]
    public string title;
    public string description;
    public Sprite icon;
    public SO_Item Reward;
    public int amountReward;
    public QuestType type;

    public enum QuestType {Speak, Collect, GO_Find};
}
