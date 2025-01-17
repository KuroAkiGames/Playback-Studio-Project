using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float moveSpeed = 2f; // Speed of enemy patrol
    public float patrolRange = 3f; // Distance the enemy will patrol left and right
    private float patrolStartPosition;
    private bool movingRight = true;

    [Header("Attack Settings")]
    public float attackRange = 2f; // Range to detect the player
    public float attackCooldown = 1f; // Time between attacks
    private float attackTimer;

    [Header("Player Detection")]
    public LayerMask playerLayer; // Layer for detecting the player
    private Transform player; // Reference to the player's transform

    [Header("Components")]
    private Animator animator; // Reference to the Animator
    private Rigidbody2D rb; // Reference to Rigidbody2D

    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        patrolStartPosition = transform.position.x;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            // If attacking, stop patrol movement
            rb.velocity = Vector2.zero;
            return;
        }

        Patrol();

        if (PlayerInRange())
        {
            AttackPlayer();
        }
    }

    private void Patrol()
    {
        // Patrol logic: Move left and right within the patrol range
        float targetPositionX = movingRight ? patrolStartPosition + patrolRange : patrolStartPosition - patrolRange;

        if (movingRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }

        // Reverse direction when reaching patrol boundaries
        if (movingRight && transform.position.x >= targetPositionX)
        {
            movingRight = false;
        }
        else if (!movingRight && transform.position.x <= targetPositionX)
        {
            movingRight = true;
        }

        // Trigger idle or walk animation
        animator.SetBool("isWalking", true);
    }

    private bool PlayerInRange()
    {
        // Check if the player is within attack range
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        if (playerCollider != null)
        {
            player = playerCollider.transform;
            return true;
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
        animator.SetTrigger("Attack");

        // Handle damage or interaction with the player (e.g., reduce health)
        if (player != null && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            Debug.Log("Enemy attacks the player!");
            // Add damage logic here, e.g., player.TakeDamage(damageAmount);
        }

        attackTimer = attackCooldown; // Reset the attack cooldown
        isAttacking = true;
        Invoke(nameof(ResetAttack), 0.5f); // Adjust duration based on attack animation length
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw attack range in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Draw patrol range
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(patrolStartPosition - patrolRange, transform.position.y, transform.position.z),
                        new Vector3(patrolStartPosition + patrolRange, transform.position.y, transform.position.z));
    }
}
