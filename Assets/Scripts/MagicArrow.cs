using UnityEngine;

public class MagicArrow : MonoBehaviour
{
    public float speed = 10f; // Speed of the arrow
    public int damage = 1; // Damage dealt to enemies
    public float lifetime = 3f; // Time before the arrow disappears
    private Vector3 startPosition; // Tracks the starting position of the arrow
    public float maxRange = 10f; // Maximum range the arrow can travel

    private void Start()
    {
        // Record the starting position of the arrow
        startPosition = transform.position;

        // Destroy the arrow after a set lifetime to prevent clutter
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move the arrow forward in its current direction
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Check if the arrow has traveled beyond its maximum range
        if (Vector3.Distance(startPosition, transform.position) >= maxRange)
        {
            Destroy(gameObject); // Destroy the arrow if it exceeds its maximum range
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Try to find the EnemyController and apply damage
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Apply damage
            }

            // Destroy the arrow after hitting an enemy
            Destroy(gameObject);
        }

        if (other.CompareTag("Environment"))
        {
            // Destroy the arrow if it hits the environment
            Destroy(gameObject);
        }
    }
}
