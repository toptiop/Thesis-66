using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLoad : MonoBehaviour
{
    SaveGame save;
    private void Start()
    {
        save = FindObjectOfType<SaveGame>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            save.Load();
            Destroy(gameObject);
        }
    }
}
