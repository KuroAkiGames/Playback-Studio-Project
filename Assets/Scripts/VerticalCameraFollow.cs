using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollowVertical : MonoBehaviour
{
    public Transform target; // The character (player) to follow
    public Vector3 offset = new Vector3(0, 10, -10); // Default offset
    public float smoothSpeed = 0.125f; // Smoothness of camera follow

    private float fixedXPosition; // Fixed X-coordinate for the camera
    private float fixedZPosition; // Fixed Z-coordinate for the camera

    [Header("Ground Adjustment")]
    public float groundOffset = 5f; // The extra space between the camera and the ground

    void Start()
    {
        // Initialize the fixed X and Z coordinates based on the camera's starting position
        fixedXPosition = transform.position.x;
        fixedZPosition = transform.position.z;

        // Set the camera's starting position to ensure it's at Y = 0 or higher
        float startYPosition = Mathf.Max(0, target.position.y + offset.y + groundOffset);
        transform.position = new Vector3(fixedXPosition, startYPosition, fixedZPosition);
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired Y position relative to the target with an offset
            float desiredYPosition = Mathf.Max(0, target.position.y + offset.y + groundOffset);

            // Calculate the desired position, keeping X and Z fixed
            Vector3 desiredPosition = new Vector3(fixedXPosition, desiredYPosition, fixedZPosition);

            // Smoothly interpolate the camera's position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update the camera's position
            transform.position = smoothedPosition;
        }
    }
}



