using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerScene : MonoBehaviour
{
    [SerializeField]private LoadingScreen load;
    [SerializeField]private SaveGame save;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            save.Save();
            load.LoadingScreenProgress(2);// Load Lv2
        }
    }
}
