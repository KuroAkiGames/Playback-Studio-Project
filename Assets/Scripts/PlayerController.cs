using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Respawn System")]
    public Transform respawnPoint; // Current respawn point (updated dynamically by checkpoints)
    public int maxHealth = 1; // Set to 1 since any damage causes instant death

    [Header("References")]
    private Animator animator;
    private Rigidbody2D rb;
    Vector3 movement;
    private int direction = 1;
    bool isJumping = false;
    private bool alive = true;

    private bool isDead;
    private float moveInput;

    void Start()
    {
        // Automatically assign components if missing
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (!animator) animator = GetComponentInChildren<Animator>();

        // Debugging messages for missing components
        if (!rb) Debug.LogError("Rigidbody2D component is missing!");
        if (!animator) Debug.LogError("Animator component is missing or not assigned!");

        // Ensure the respawnPoint is set
        if (respawnPoint == null)
        {
            respawnPoint = new GameObject("FallbackRespawnPoint").transform;
            respawnPoint.position = transform.position; // Default to the player's starting position
        }
        Respawn();
    }

    void Update()
    {
        if (isDead) return; // Disable controls when dead

        // Handle Movement
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }

        // Update Animations
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
    }

    public void TakeDamage()
    {
        if (isDead) return;

        // Instantly kill the character
        Die();
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");

        // Disable movement immediately
        rb.velocity = Vector2.zero;
        rb.simulated = false;

        // Respawn after a delay
        Invoke(nameof(Respawn), 2f); // Delay to allow death animation to play
    }

    private void Respawn()
    {
        isDead = false;
        animator.SetTrigger("Respawn");

        // Reset position and enable movement
        transform.position = respawnPoint.position;
        rb.simulated = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player touches a checkpoint
        if (collision.CompareTag("Checkpoint"))
        {
            // Update respawn point to the checkpoint's position
            respawnPoint = collision.transform;
        }
    }
}
