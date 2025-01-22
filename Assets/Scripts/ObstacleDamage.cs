using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    // When another object (the player) enters the collider of the obstacle
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.LoseLife(); // Instantly lose a life
            }
        }
    }
}
