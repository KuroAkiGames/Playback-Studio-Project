using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // This will store the checkpoint's position.
    public Transform checkpointPosition;

    private void Start()
    {
        // If no position is assigned in the inspector, use the object's position as the checkpoint.
        if (checkpointPosition == null)
        {
            checkpointPosition = transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player collides with the checkpoint
        if (collision.CompareTag("Player"))
        {
            // Update the respawn point in the PlayerController
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.respawnPoint = checkpointPosition;
                Debug.Log("Checkpoint reached! Respawn point updated.");
            }

            // Optionally, you can add some visual feedback like changing the checkpoint's color
            // to show the player that they've activated the checkpoint.
            ChangeCheckpointColor();
        }
    }

    // You can call this method to give some feedback when the checkpoint is activated.
    private void ChangeCheckpointColor()
    {
        // Example: Change the checkpoint to green (indicating it has been activated).
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.green;
        }
    }
}
