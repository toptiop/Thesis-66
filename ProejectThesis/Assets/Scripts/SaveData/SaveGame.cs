using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public static SaveGame Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        
    }
    //private void OnGUI()
    //{
    //    if (GUILayout.Button("Save"))
    //    {
    //        Save();
    //    }
    //    else if (GUILayout.Button("Load"))
    //    {
    //        Load();
    //    }

    //}
    public void Save()
    {
        Singleton.Instance.inventory.SaveInventory();
        Singleton.Instance.inventoryNote.SaveInventory();
        Singleton.Instance.inventoryVideo.SaveInventory();
        //Singleton.controller.Save();
    }

    public void Load()
    {
        Singleton.Instance.inventory.LoadInventory();
        Singleton.Instance.inventoryNote.LoadInventory();
        Singleton.Instance.inventoryVideo.LoadInventory();
        //Singleton.controller.Load();
    }
}
