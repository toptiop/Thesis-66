using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collect Quest", menuName = "Create new Quest/Collect Quest")]
public class SO_CollectQuest : SO_Quest
{
    [Header("CollectQuest DETIALS")]
    public int requiredItemCount; // จำนวนของที่ผู้เล่นต้องเก็บ
    public SO_Item collectItem; // ไอเท็มที่ต้องเก็บ
}
