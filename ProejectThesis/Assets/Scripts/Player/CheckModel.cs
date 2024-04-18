using System.Collections;
using UnityEngine;

public class CheckModel : MonoBehaviour
{
    [SerializeField] private Vector3 pos;
    [SerializeField] private Quaternion rot;

    private void Update()
    {
        StartCoroutine(SetPosAfterDelay());
    }

    IEnumerator SetPosAfterDelay()
    {
        yield return new WaitForSeconds(5);
        transform.localPosition = pos;
        transform.localRotation = rot;
    }
}
