using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerScene : MonoBehaviour
{
    [SerializeField]private LoadingScreen load;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            load.LoadingScreenProgress(2);// Load Lv2
        }
    }
}
