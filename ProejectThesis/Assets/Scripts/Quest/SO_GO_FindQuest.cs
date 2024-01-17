using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GO Find Quest", menuName = "Create new Quest/GO Find Quest")]
public class SO_GO_FindQuest : SO_Quest
{
    [Header("FindQuest DETIALS")]
    public bool activeWaypoint;
    public Vector3 destination; // จุดที่ผู้เล่นต้องไปหา 
}
