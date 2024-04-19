using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnabledEnemy : MonoBehaviour
{
    [SerializeField] NavMeshAgent e;
    [SerializeField] Enemy enemy;
    [SerializeField] float timer = 1;
    private void Start()
    {
        StartCoroutine(DaleyEnemy(timer));
    }
    IEnumerator DaleyEnemy(float time)
    {
        yield return new WaitForSeconds(time);

        e.enabled = true;
        enemy.enabled = true;
    }
}
