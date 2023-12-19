using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create new Item")]
public class SO_Item : ScriptableObject
{
    [Header("Detail")]
    public Sprite icon;
    public string id;
    public string itemName;
    public string description;
    public int maxStack;

    [Header("In Game Object")]
    public GameObject gamePrefab;

}
