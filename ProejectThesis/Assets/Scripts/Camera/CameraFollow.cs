using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the target object (your character)
    public Vector3 offset = new Vector3(0f, 3f, -5f); // Offset from the target object
    public float smoothSpeed = 0.125f; // Smoothing factor for camera movement

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not set for CameraFollow script.");
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target); // Make the camera look at the target
    }
}
