using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float movePower = 5f;
    public float jumpPower = 10f; // Normal jump power
    public float superJumpPower = 20f; // Power for Super Jump
    public float flightControlPower = 5f; // Control for flight (upward/downward force)
    public float gravityScaleWhenFlying = 0.5f; // Reduced gravity when flying
    public float knockbackForce = 10f; // Adjust the knockback force as needed

    [Header("Health System")]
    public HealthManager healthManager;

    [Header("Spells")]
    public GameObject magicArrowPrefab; // Assign the Magic Arrow prefab in the Inspector
    public Transform spellSpawnPoint; // Position where the arrow will spawn
    public float magicArrowCooldown = 2f; // Cooldown time for Magic Arrow

    [Header("Respawn System")]
    public Transform respawnPoint; // Current respawn point (updated dynamically by checkpoints)
    public int maxHealth = 3; // Set to 3 for full health (3 hits)

    [Header("References")]
    private Rigidbody2D rb;
    private Animator anim;
    private int direction = 1; // Player's direction for flipping the sprite
    private bool isJumping = false;
    private bool isDead = false;
    private float moveInput;

    public Animator uiAnim;

    public bool DebugCollision;

    // Super Jump/Flight Flag
    private bool canSuperJump = false;  // To track if the player has the skill
    private bool isFlying = false; // To track if the player is flying

    // Cooldown Tracking
    private bool isMagicArrowOnCooldown = false;
    private float magicArrowCooldownTimer = 0f;
    private bool isCastingMagicArrow = false; // Prevents spamming during animation or cooldown

    void Start()
    {
        // Automatically assign components if missing
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        // Debugging messages for missing components
        if (!rb) Debug.LogError("Rigidbody2D component is missing!");
        if (!anim) Debug.LogError("Animator component is missing or not assigned!");

        // Ensure the respawnPoint is set
        if (respawnPoint == null)
        {
            respawnPoint = new GameObject("FallbackRespawnPoint").transform;
            respawnPoint.position = transform.position; // Default to the player's starting position
        }
        Respawn();

        // Check if the player has unlocked the Super Jump/Flight skill
        canSuperJump = PlayerProgress.hasSuperJump;

    }

    void Update()
    {
        if (isDead) return; // Disable controls when dead

        HandleMovement();
        HandleJump();
        HandleSuperJumpOrFlight();  // Handle Super Jump or Flight input
        HandleAttack();
        UpdateAnimations();
        UpdateCooldown(); // Update the magic arrow cooldown timer
        UpdateAnimations();

    }

    private void HandleMovement()
    {
        // Read horizontal input
        moveInput = Input.GetAxisRaw("Horizontal");

        // Move the player
        Vector3 moveVelocity = Vector3.zero;
        if (moveInput < 0)
        {
            direction = -1;
            moveVelocity = Vector3.left;
            if (!anim.GetBool("isJump")) anim.SetBool("isRun", true); // Only run if not jumping
        }
        else if (moveInput > 0)
        {
            direction = 1;
            moveVelocity = Vector3.right;
            if (!anim.GetBool("isJump")) anim.SetBool("isRun", true); // Only run if not jumping
        }
        else
        {
            anim.SetBool("isRun", false); // Stop running if no input
        }

        // Flip the player sprite based on direction
        transform.localScale = new Vector3(direction, 1, 1);

        // Apply movement
        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    private void HandleJump()
    {
        if (isJumping || isFlying)
        {
            return; // If the player is already jumping or flying, don't handle another jump
        }

        // Normal Jump when pressing the jump button
        if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0) && !anim.GetBool("isJump"))
        {
            isJumping = true;
            anim.SetBool("isJump", true);

            // Apply normal jump force
            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rb.velocity = new Vector2(rb.velocity.x, 0); // Zero out vertical velocity before jumping
            rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
        }
    }

    private void HandleSuperJumpOrFlight()
    {
        // If the player has the Super Jump skill, allow flight-like behavior
        if (canSuperJump)
        {
            if (Input.GetButtonDown("Jump") && !isJumping && !isFlying)
            {
                // Start flying (super jump effect)
                isFlying = true;
                anim.SetTrigger("isJump");
                rb.gravityScale = gravityScaleWhenFlying;  // Reduced gravity during flight
            }

            if (isFlying)
            {
                // Handle continuous upward/downward control during flight
                if (Input.GetButton("Jump"))
                {
                    // Fly upward (apply a small constant force upward)
                    rb.velocity = new Vector2(rb.velocity.x, flightControlPower);
                }
                else if (Input.GetButtonUp("Jump"))
                {
                    // Stop flying when the jump button is released, let gravity pull the player down
                    rb.velocity = new Vector2(rb.velocity.x, 0); // Stop vertical velocity
                    isFlying = false;
                    rb.gravityScale = 5; // Restore gravity to normal
                    //anim.SetBool("isJump", false);
                }
            }
        }
    }

    private void HandleAttack()
    {
        // Check for input and that magic arrow is neither on cooldown nor mid-casting
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isMagicArrowOnCooldown && !isCastingMagicArrow)
        {
            FireMagicArrow();
        }
    }

    private void FireMagicArrow()
    {
        // Prevent additional casting during the attack
        isCastingMagicArrow = true;

        // Trigger the attack animation
        anim.SetTrigger("attack");

        // Instantiate the magic arrow
        GameObject magicArrow = Instantiate(magicArrowPrefab, spellSpawnPoint.position, Quaternion.identity);

        // Adjust direction
        Vector3 arrowScale = magicArrow.transform.localScale;
        arrowScale.x *= direction;
        magicArrow.transform.localScale = arrowScale;

        Vector3 arrowRotation = magicArrow.transform.eulerAngles;
        arrowRotation.z = direction == -1 ? 180f : 0f;
        magicArrow.transform.eulerAngles = arrowRotation;

        // Start the cooldown
        isMagicArrowOnCooldown = true;
        magicArrowCooldownTimer = magicArrowCooldown;

        // Allow casting again after a short delay (sync with attack animation)
        Invoke(nameof(ResetCasting), 0.5f); // Adjust delay as per the attack animation length
    }

    private void ResetCasting()
    {
        isCastingMagicArrow = false;
    }

    private void UpdateAnimations()
    {
        // Update the "speed" parameter in Animator based on movement input
        anim.SetFloat("speed", Mathf.Abs(moveInput));

        // If grounded and not jumping, reset the jump animation
        if (Mathf.Approximately(rb.velocity.y, 0) && isJumping)
        {
            anim.SetTrigger("isLanding"); // Reset jump animation when grounded
            isJumping = false;
        }
    }

    public void TakeDamage(Vector2 hitDirection)
    {
        Debug.Log("Player took damage! Hit Direction: " + hitDirection);

        if (healthManager != null)
        {
            healthManager.TakeDamage(1); // Take 1 damage for non-lethal hits
        }

        // Trigger the Hurt animation
        anim.SetTrigger("hurt");

        // Apply knockback in the opposite direction of the hit
        Vector2 knockback = hitDirection.normalized * knockbackForce;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y); // Maintain current vertical velocity

        if (healthManager.currentHealth <= 0)
        {
            TriggerDeathAnimation();
        }
    }

    private void UpdateCooldown()
    {
        // Decrement the cooldown timer if it is active
        if (isMagicArrowOnCooldown)
        {
            magicArrowCooldownTimer -= Time.deltaTime;

            if (magicArrowCooldownTimer <= 0f)
            {
                isMagicArrowOnCooldown = false;
                magicArrowCooldownTimer = 0f; // Reset the timer
            }
        }
    }

    public void TriggerDeathAnimation()
    {
        isDead = true;
        anim.SetTrigger("die");

        // Disable movement immediately
        rb.velocity = Vector2.zero;
        rb.simulated = false;
        anim.SetBool("isRun", false);  // Make sure "isRun" is false to avoid running animation
        anim.SetBool("isJump", false); // Make sure "isJump" is false to avoid jump animation
        uiAnim.SetBool("isLoaded", true);

        // Respawn after a delay
        Invoke(nameof(Respawn), 2f); // Delay to allow death animation to play
    }

    public void Respawn()
    {
        isDead = false;
        anim.SetTrigger("idle");
        uiAnim.SetBool("isLoaded", false); 

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