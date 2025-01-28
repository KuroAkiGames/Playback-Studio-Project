using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 1; // Maximum health
    private int currentHealth; // Current health

    [Header("Patrol Settings")]
    public float moveSpeed = 2f; // Speed of enemy movement
    public float patrolRange = 5f; // Patrol distance (left and right)
    private float startPositionX; // Initial X position
    private bool movingRight = true; // Direction of movement

    [Header("Attack Settings")]
    public float attackRange = 3f; // Range to detect the player
    public float attackCooldown = 1f; // Time between attacks
    private float attackTimer;

    [Header("Player Detection")]
    public string playerTag = "Player"; // Tag for the player
    private Transform player; // Reference to the player's transform

    [Header("Components")]
    private Animator anim; // Reference to Animator
    private Rigidbody2D rb; // Reference to Rigidbody2D
    private Vector3 originalScale; // Store the original scale
    private Vector2 CharacterDirection;

    private bool isAttacking = false;
    private bool PlayerRayHit;
    public bool DebugCollision;


    void Start()
    {
        // Initialize health
        currentHealth = maxHealth;

        // Store the original scale at the start
        originalScale = transform.localScale;

        // Initialize other components
        startPositionX = transform.position.x;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Ensure Rigidbody2D is configured correctly
        rb.gravityScale = 0; // No gravity for side-to-side patrol
        rb.freezeRotation = true; // Prevent rotation

        // Set initial facing direction (right)
        if (movingRight)
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); // Face right
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); // Face left
        }
    }

    void Update()
    {
        Patrol(); // Call Patrol in Update for continuous movement

        if (isAttacking)
        {
            // If attacking, stop patrol movement
            rb.velocity = Vector2.zero;
            return;
        }

        if (PlayerInRange())
        {
            AttackPlayer();
        }

        DebugEnemyCollision();
    }

    private void DebugEnemyCollision()
    {
        // Define the offset (e.g., 1 unit above the character's feet)
        Vector3 offset = new Vector3(0, 1f, 0);

        // Calculate the starting position of the raycast
        Vector3 raycastOrigin = transform.position + offset;

        // Perform the raycast
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, CharacterDirection, attackRange);

        // Draw the debug ray for visualization
        Debug.DrawRay(raycastOrigin, CharacterDirection * attackRange, Color.red);

        // Check the raycast result
        if (hit)
        {
            PlayerRayHit = true;
            if (DebugCollision)
            {
                Debug.Log("Hit is true");
            }
        }
        else
        {
            PlayerRayHit = false;
            if (DebugCollision)
            {
                Debug.Log("Hit is false");
            }
        }
    }

    private void Patrol()
    {
        // Determine patrol boundaries
        float leftBoundary = startPositionX - patrolRange;
        float rightBoundary = startPositionX + patrolRange;

        // Move the enemy left or right
        if (movingRight)
        {
            rb.velocity = new Vector2(moveSpeed, 0); // Move right
            // Flip the sprite to face right (rotate 180 degrees)
            transform.rotation = Quaternion.Euler(0, 180, 0);
            CharacterDirection = Vector2.right;
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, 0); // Move left
            // Flip the sprite to face left (rotate 0 degrees)
            transform.rotation = Quaternion.Euler(0, 0, 0);
            CharacterDirection = -Vector2.right;
        }

        // Switch direction when reaching boundaries
        if (transform.position.x >= rightBoundary)
        {
            movingRight = false;
        }
        else if (transform.position.x <= leftBoundary)
        {
            movingRight = true;
        }

        // Trigger the walking animation
        anim.SetBool("isWalk", true);
    }

    private bool PlayerInRange()
    {
        // Find all colliders in the attack range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D col in colliders)
        {
            // Check if the collider belongs to the player by tag
            if (col.CompareTag(playerTag))
            {
                player = col.transform; // Set the player reference
                return true;
            }
        }
        return false;
    }

    private void AttackPlayer()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            return;
        }

        // Trigger attack animation
        anim.SetTrigger("Attack");

        Debug.Log("Attack Range " + attackRange);

        if (player != null && PlayerRayHit)
        {
            Debug.Log("Enemy attacks the player!");

            // Get the PlayerController component
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                // Calculate the hit direction
                Vector2 hitDirection = (player.position - transform.position).normalized;

                // Call the TakeDamage method with the hit direction
                playerController.TakeDamage(hitDirection);
            }
            else
            {
                Debug.LogWarning("PlayerController not found on player!");
            }
        }

        attackTimer = attackCooldown; // Reset the attack cooldown
        isAttacking = true;
        Invoke(nameof(ResetAttack), 0.5f); // Adjust duration based on attack animation length
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce health by the damage amount

        // Optional: Play damage animation or effect
        anim.SetTrigger("get_hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Play death animation
        anim.SetTrigger("die");

        // Disable enemy movement and collider
        rb.velocity = Vector2.zero;
        rb.isKinematic = true; // Stop physics interactions
        GetComponent<Collider2D>().enabled = false;

        // Destroy the enemy after a delay (to allow death animation to play)
        Destroy(gameObject, 1f); // Adjust the delay as needed
    }
}