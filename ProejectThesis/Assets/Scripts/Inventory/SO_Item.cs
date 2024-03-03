using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create new Item/Item")]
public class SO_Item : ScriptableObject
{
    [Header("Detail")]
    public Type itemType;
    public Sprite icon;
    public string id;
    public string itemName;
    public string description;
    public int maxStack;

    [Header("Note")]
    public string noteTitle;
    public string noteDescription;
    

    [Header("In Game Object")]
    public GameObject gamePrefab;

    public enum Type { Material, Note}

}
