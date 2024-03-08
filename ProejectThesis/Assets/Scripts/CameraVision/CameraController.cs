using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform rotationPoint; // The point around which the camera rotates
    public float rotationSpeed = 30f; // Rotation speed in degrees per second
    public float detectionRadius = 10f; // Radius within which the camera can detect the player
    public float detectionAngle = 60f; // Field of view angle for detection
    public LayerMask playerLayer; // Layer mask for the player
    public LayerMask obstructionLayer; // Layer mask for obstructions

    private bool playerDetected = false; // Flag indicating whether the player is detected

    void Update()
    {
        // Check if the rotation point is set
        if (rotationPoint != null)
        {
            // Calculate the direction from the camera to the rotation point
            Vector3 directionToRotationPoint = rotationPoint.position - transform.position;

            // Calculate the Quaternion that represents the rotation towards the rotation point
            Quaternion targetRotation = Quaternion.LookRotation(directionToRotationPoint);

            // Smoothly rotate the camera towards the rotation point
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Check for player detection
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);

            // Iterate through detected colliders
            foreach (Collider collider in hitColliders)
            {
                // Check if the detected collider belongs to the player
                if (collider.CompareTag("Player"))
                {
                    // Check if the player is within the camera's field of view
                    Vector3 directionToPlayer = collider.transform.position - transform.position;
                    float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);

                    if (angleToPlayer <= detectionAngle / 2f)
                    {
                        // Check for obstructions
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRadius, obstructionLayer))
                        {
                            Debug.DrawRay(transform.position, directionToPlayer * hit.distance, Color.green);
                            if (hit.collider.CompareTag("Player"))
                            {
                                playerDetected = true;
                                // You can trigger some actions here like raising an alarm, alerting security, etc.
                                Debug.Log("Player detected!");
                            }
                            else
                            {
                                playerDetected = false;
                            }
                        }
                        else
                        {
                            playerDetected = true;
                            // You can trigger some actions here like raising an alarm, alerting security, etc.
                            Debug.Log("Player detected!");
                        }
                    }
                    else
                    {
                        playerDetected = false;
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("Rotation point not set for the security camera.");
        }
    }

    // Gizmos for visualizing the detection radius and angle in the Unity Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Vector3 viewAngleA = Quaternion.AngleAxis(detectionAngle / 2f, transform.up) * transform.forward * detectionRadius;
        Vector3 viewAngleB = Quaternion.AngleAxis(-detectionAngle / 2f, transform.up) * transform.forward * detectionRadius;
        Gizmos.DrawLine(transform.position, transform.position + viewAngleA);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB);
    }
}

