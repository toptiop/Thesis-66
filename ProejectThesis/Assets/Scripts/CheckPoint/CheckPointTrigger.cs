using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    public CheckPoint checkPoint;
    public SaveGame save;

    private void Start()
    {
        checkPoint = FindObjectOfType<CheckPoint>();
        save = FindObjectOfType<SaveGame>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkPoint.currentCheckPoint = transform.position;
            save.Save();
        }
    }

}
