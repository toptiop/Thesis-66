using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = ("Quest System / Create New Quest"))]
public class SO_Quest : ScriptableObject
{
    [Header("Quest Data")]
    public string questName;
    public string questDescription;

    public Objective[] objectives;

    [Header("Reward")]
    public int goldReward;
    public int expReward;

    public List<SO_Item> ItemReward;
}
