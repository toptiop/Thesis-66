using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Speak Quest", menuName = "Create new Quest/Speak Quest")]
public class SO_SpeakQuest : SO_Quest
{
    [Header("GENERAL DETIALS")]
    public List<string> Speeches = new List<string>();
    public int SpeecIndex = 0;

}
