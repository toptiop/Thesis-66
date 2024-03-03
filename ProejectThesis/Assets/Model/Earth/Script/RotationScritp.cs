using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScritp : MonoBehaviour
{
    public float rotationSpeed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        transform.localRotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
    }

}
