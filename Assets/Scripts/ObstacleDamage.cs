using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    // When another object (the player) enters the collider of the obstacle
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object colliding with the obstacle has the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the PlayerController script from the player object
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                // Call the TakeDamage method to instantly kill the player
                playerController.TakeDamage();
            }
        }
    }
}
