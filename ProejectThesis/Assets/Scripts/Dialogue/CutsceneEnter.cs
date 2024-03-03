using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEnter : MonoBehaviour
{
    public GameObject player;
    public GameObject cutsceneCam;
    BoxCollider col;
    [SerializeField] float timer;
    PlayerController _controller;
    private void Awake()
    {
        col = GetComponent<BoxCollider>();
        _controller = player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            col.enabled = false;
            cutsceneCam.SetActive(true);
            _controller.canMove = true;
            StartCoroutine(FinishCut());
        }
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(timer);
        _controller.canMove = false;
        cutsceneCam.SetActive(false);
        Destroy( gameObject );
    }
}
