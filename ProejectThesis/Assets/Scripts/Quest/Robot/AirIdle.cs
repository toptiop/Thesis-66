using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirIdle : MonoBehaviour
{
    [SerializeField]
    private Vector3 startPos;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float height = 1f;
    [SerializeField]
    private float smoothSpeed = 0.1f;
    [SerializeField]
    private float smoothSpeedAirTime = 1f;
    void Start()
    {
        startPos = transform.position;
    }


    void Update()
    {
        
    }

    public void StartResetAir()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = startPos.y;
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);
    }
    public void AirTimeIdle()
    {
        StartCoroutine(ResterAir());
    }

    IEnumerator ResterAir()
    {
        yield return new WaitForSeconds(2);

        float newY = startPos.y + Mathf.Sin(Time.time * speed) * height;

        Vector3 currentPos = transform.position;
        Vector3 targetPos = new Vector3(currentPos.x, newY, currentPos.z);

        transform.position = Vector3.Lerp(currentPos, targetPos, Time.deltaTime * smoothSpeedAirTime);
    }
}
