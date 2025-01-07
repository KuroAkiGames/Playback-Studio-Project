using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    public float parallaxSpeed;        // Speed at which the background moves
    public Transform cameraTransform;  // Reference to the camera

    private Vector3 previousCameraPosition;
    private float backgroundWidth;     // Width of the background object

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;  // Default to the main camera if not set
        }
        previousCameraPosition = cameraTransform.position;

        // Get the width of the background by using the renderer bounds
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Calculate how far the camera has moved since last frame
        float cameraMovement = cameraTransform.position.x - previousCameraPosition.x;

        // Move the background based on the camera movement and the parallax speed
        transform.position -= new Vector3(cameraMovement * parallaxSpeed, 0, 0);

        // If the background has moved past the screen, reset its position for looping
        if (transform.position.x < -backgroundWidth)
        {
            // Reposition the background to the right side
            transform.position = new Vector3(transform.position.x + backgroundWidth * 2, transform.position.y, transform.position.z);
        }

        // Update the previous camera position to the current position for next frame
        previousCameraPosition = cameraTransform.position;
    }
}
