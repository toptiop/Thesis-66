using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatForm : MonoBehaviour
{
    private CharacterController player;
    public Transform pos;
    public Vector3 lastPos;

    private void Start()
    {
        lastPos = transform.position;
    }
    private void Update()
    {
        if(lastPos != transform.position)
        {
            if(player != null)
            {
                player.enabled = false;
            }
            
        }
        else
        {
            if (player != null)
            {
                player.enabled = true;

            }
            StartCoroutine(SetlastPos());
        }
    }

    IEnumerator SetlastPos()
    {
        yield return new WaitForSeconds(2);
        lastPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.GetComponent<CharacterController>();

            Debug.Log("Player Enter");
            other.transform.position = pos.position;
            other.transform.SetParent(pos);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = pos.position;
           // other.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {            
            player = null;
            other.transform.SetParent(null);
        }
    }
}
