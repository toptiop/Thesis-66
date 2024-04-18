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
            StartCoroutine(DelayLoad());
            Destroy(gameObject,2.5f);
        }
    }

    IEnumerator DelayLoad()
    {
        yield return new WaitForSeconds(2);
        save.Load();
    }
}
