using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioClip[] footstepSounds; // Array of footstep sounds to choose from
    public AudioSource audioSource;   // Reference to the AudioSource

    public float stepInterval = 0.5f; // Time between steps (adjust based on movement speed)
    private float stepTimer;

    private Rigidbody2D rb2D;         // Reference to Rigidbody2D

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // Ensure your character has a Rigidbody2D
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Check if the character is moving
        if (rb2D != null && rb2D.velocity.magnitude > 0.1f) // Adjust threshold as needed
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0)
            {
                PlayFootstepSound();
                stepTimer = stepInterval; // Reset the step timer
            }
        }
        else
        {
            stepTimer = 0f; // Reset the timer if the character is stationary
        }
    }

    private void PlayFootstepSound()
    {
        if (footstepSounds.Length == 0) return;

        // Choose a random footstep sound
        AudioClip footstep = footstepSounds[Random.Range(0, footstepSounds.Length)];

        // Play the footstep sound
        audioSource.PlayOneShot(footstep);
    }
}