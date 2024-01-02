using UnityEngine;
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue Data")]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public struct DialogueEntry
    {
        public string speaker;
        [TextArea(3, 10)]
        public string message;
    }

    public DialogueEntry[] dialogueEntries;
}
