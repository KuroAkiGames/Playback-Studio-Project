using UnityEngine;

public class CatFollower : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform player; // Reference to the player's Transform
    public float followDistance = 2f; // Minimum distance to maintain from the player
    public float followSpeed = 3f; // Speed of the cat

    [Header("Animation Settings")]
    public Animator animator; // Reference to the Animator component
    private bool isWalking = false;

    [Header("Respawn Settings")]
    public Vector3 offset = new Vector3(1f, 0f, 0f); // Offset from the player's respawn position

    private Rigidbody2D rb; // Rigidbody2D for movement
    private Vector3 respawnPosition; // Position to respawn the cat

    void Start()
    {
        // Find the player if not assigned
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("Player not found! Make sure the player is tagged 'Player'.");
            }
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing!");
        }

        if (animator == null)
        {
            Debug.LogError("Animator component is not assigned!");
        }
    }

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (player == null || animator == null) return;

        // Calculate the distance between the cat and the player
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > followDistance)
        {
            // Move towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * followSpeed, rb.velocity.y);

            // Flip the cat sprite based on movement direction
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // Face right
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1); // Face left
            }

            // Trigger walking animation
            if (!isWalking)
            {
                isWalking = true;
                animator.SetBool("isWalking", true);
            }
        }
        else
        {
            // Stop moving
            rb.velocity = Vector2.zero;

            // Stop walking animation
            if (isWalking)
            {
                isWalking = false;
                animator.SetBool("isWalking", false);
            }
        }

        // Jumping animation based on vertical velocity
        animator.SetBool("isJumping", rb.velocity.y > 0.1f);
    }

    public void RespawnWithPlayer(Vector3 playerRespawnPosition)
    {
        // Move the cat to the player's respawn position
        transform.position = playerRespawnPosition + offset;
        rb.velocity = Vector2.zero;
    }
}
