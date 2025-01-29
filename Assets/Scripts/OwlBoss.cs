using UnityEngine;
using UnityEngine.UI; // For HP bar UI

public class OwlBoss : MonoBehaviour
{
    public float speed = 3f; // Speed for flying and patrolling
    public float diveSpeed = 5f; // Speed of dive attack
    public float hp = 5f; // Starting HP
    public Image hpBar; // Reference to the UI Image that represents HP bar
    public float patrolRangeX = 14f; // Patrol range for X axis (-14 to 14)
    public float ascendHeight = 25f; // Height to ascend on the Y axis
    public Animator animator; // Reference to the Animator for controlling animations
    public Rigidbody2D rb; // Reference to the 2D Rigidbody

    private Vector3 startPosition;
    private bool isDiving = false;
    private bool isPatrolling = true;
    private bool canAttack = true;
    private bool isFlyingUp = true;
    private float initialYPosition;
    private float diveTimer; // Timer for the next dive bomb
    private float nextDiveTime; // Random duration for the next dive bomb

    void Start()
    {
        startPosition = transform.position; // Save the initial position
        initialYPosition = transform.position.y; // Track the initial Y position
        animator.Play("BirdIdleFlying"); // Start the flying animation
        rb.velocity = new Vector2(0, 5f); // Set upward velocity to start flying up
        UpdateHPBar();
        SetRandomDiveTime(); // Set the first random dive time

    }

    void Update()
    {
        if (hp <= 0)
        {
            Die();
        }
        else
        {
            // Check if the boss is in the middle of the dive
            if (isDiving)
            {
                PerformDiveAttack();
            }
            else if (isFlyingUp)
            {
                // Stop ascending once we reach the desired height
                if (transform.position.y >= initialYPosition + ascendHeight)
                {
                    rb.velocity = Vector2.zero; // Stop upward movement
                    isFlyingUp = false; // Stop flying up and start patrolling
                }
            }
            else if (isPatrolling)
            {
                PatrolAlongXAxis();
            }

            // Update the dive timer and check if it's time for a dive bomb
            if (!isDiving)
            {
                diveTimer += Time.deltaTime; // Increase the timer each frame

                if (diveTimer >= nextDiveTime)
                {
                    StartDiveAttack(); // Start dive bomb after the timer reaches the set duration
                    SetRandomDiveTime(); // Reset and set a new random duration for the next dive bomb
                }
            }
        }
    }

    // Handle the boss's patrol movement along the X axis (-14 to 14)
    private void PatrolAlongXAxis()
    {
        float xPos = Mathf.PingPong(Time.time * speed, patrolRangeX * 2) - patrolRangeX; // Patrol from -14 to 14
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }

    // Trigger the dive attack
    private void PerformDiveAttack()
    {
        if (transform.position.y > -2f) // Assuming -2 is the point to stop diving
        {
            rb.velocity = new Vector2(0, -diveSpeed); // Move down for dive attack
        }
        else
        {
            isDiving = false;
            isPatrolling = true; // Resume patrolling
            canAttack = true; // Allow the player to attack
            animator.Play("BirdIdleFlying"); // Return to idle flying animation
        }
    }

    // Trigger dive attack (This would be called by a timer or an event)
    public void StartDiveAttack()
    {
        if (canAttack)
        {
            isDiving = true;
            isPatrolling = false; // Stop patrolling when diving
            canAttack = false;
            animator.Play("DiveBomb"); // Play dive bomb animation
        }
    }

    // Set a random time for the next dive bomb (between 10 and 15 seconds)
    private void SetRandomDiveTime()
    {
        nextDiveTime = Random.Range(10f, 15f); // Random time between 10 and 15 seconds
        diveTimer = 0f; // Reset the timer
    }

    // Decrease HP and update the HP bar
    public void TakeDamage(float damage)
    {
        hp -= damage;
        UpdateHPBar();
        animator.Play("Hit"); // Play hit animation
    }

    // Update the HP bar UI
    private void UpdateHPBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = hp / 5f; // 5 is the max HP value
        }
    }

    // Perform boss death and unlock Super Jump
    public void Die()
    {
        // Play death animation or effect
        // Perform any additional death logic here (e.g., triggering game over screen)
        PlayerProgress.UnlockSuperJump();
        Destroy(gameObject); // Destroy the boss object after death
    }
}
