using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAt : MonoBehaviour
{
    public Camera mainCamera;

    private void Start()
    {
        mainCamera = FindObjectOfType(typeof(Camera)) as Camera;
    }

    void Update()
    {
        // Rotate the canvas to face the camera
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
            mainCamera.transform.rotation * Vector3.up);
    }
}
