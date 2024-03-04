using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void OnGUI()
    {
        if (GUILayout.Button("Save"))
        {
            Save();
        }
        else if (GUILayout.Button("Load"))
        {
            Load();
        }

    }
    public void Save()
    {
        Singleton.Instance.inventory.SaveInventory();
        Singleton.Instance.inventoryNote.SaveInventory();
        Singleton.controller.Save();
    }

    public void Load()
    {
        Singleton.Instance.inventory.LoadInventory();
        Singleton.Instance.inventoryNote.LoadInventory();
        Singleton.controller.Load();
    }
}
