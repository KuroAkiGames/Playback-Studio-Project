using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // The character (player) to follow
    public Vector3 offset;         // Offset of the camera relative to the player
    public float smoothSpeed = 0.125f;  // Smoothness of camera follow

    void LateUpdate()
    {
        if (target != null)
        {
            // Desired camera position based on the target's position and offset
            Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, offset.z);

            // Smoothly interpolate the camera's position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update camera's position
            transform.position = smoothedPosition;
        }
    }
}