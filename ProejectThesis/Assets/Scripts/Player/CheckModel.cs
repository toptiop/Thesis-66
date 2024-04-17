using System.Collections;
using UnityEngine;

public class CheckModel : MonoBehaviour
{
    [SerializeField] private Vector3 pos;

    private void Update()
    {
        StartCoroutine(SetPosAfterDelay());
    }

    IEnumerator SetPosAfterDelay()
    {
        yield return new WaitForSeconds(5);
        transform.localPosition = pos; 
    }
}
