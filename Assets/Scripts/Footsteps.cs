using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioClip[] footstepSounds; // Array of footstep sounds to choose from
    public AudioSource audioSource;    // Reference to the AudioSource

    public float stepInterval = 0.5f;  // Time between steps (adjust based on movement speed)
    private float stepTimer;

    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>(); // Ensure your character has a CharacterController
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the character is moving
        if (characterController.isGrounded && characterController.velocity.magnitude > 0.1f)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0)
            {
                PlayFootstepSound();
                stepTimer = stepInterval;  // Reset the step timer
            }
        }
    }

    private void PlayFootstepSound()
    {
        if (footstepSounds.Length == 0) return;

        // Choose a random footstep sound
        AudioClip footstep = footstepSounds[Random.Range(0, footstepSounds.Length)];

        // Play the footstep sound at the character's position
        audioSource.PlayOneShot(footstep);
    }
}