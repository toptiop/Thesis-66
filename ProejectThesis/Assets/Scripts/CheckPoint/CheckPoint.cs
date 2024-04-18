using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Vector3 currentCheckPoint;
    SaveGame save;

    private void Start()
    {
        save = FindObjectOfType<SaveGame>();
    }

    public void RespawnPlayer(GameObject player)
    {
        Debug.Log("Respawn");
        save.loadTransform = true;
        player.transform.position = currentCheckPoint;
    }
}
