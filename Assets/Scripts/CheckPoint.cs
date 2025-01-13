using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // This will store the checkpoint's position.
    public Transform checkpointPosition;
    public AudioClip checkpointSound;  // The sound to play when checkpoint is activated
    private AudioSource audioSource;   // The audio source component for the sound

    private void Start()
    {
        // If no position is assigned in the inspector, use the object's position as the checkpoint.
        if (checkpointPosition == null)
        {
            checkpointPosition = transform;
        }

        // Initialize the AudioSource component and ensure it doesn't play automatically
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();  // Add AudioSource if it doesn't exist
        }

        {
            audioSource.clip = checkpointSound;
            audioSource.loop = true;  // Set the sound to loop
            audioSource.playOnAwake = false;  // Don't play immediately, we'll trigger it manually
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player collides with the checkpoint
        if (collision.CompareTag("Player"))
        {
            // Update the respawn point in the PlayerController
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.respawnPoint = checkpointPosition;
                Debug.Log("Checkpoint reached! Respawn point updated.");

                // Play the checkpoint sound (if it's not already playing)
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }

            // Optionally, you can add some visual feedback like changing the checkpoint's color
            // to show the player that they've activated the checkpoint.
            ChangeCheckpointColor();
        }

        private void Update()
        {
            // Adjust the volume of the sound based on the distance from the player
            if (audioSource.isPlaying)
            {
                // Calculate the distance between the player and the checkpoint
                float distance = Vector2.Distance(transform.position, Camera.main.transform.position);

                // Normalize the distance to adjust volume (you can change these values based on the desired range)
                float maxDistance = 10f; // The maximum distance for full volume (you can adjust this)
                float minDistance = 0f;  // The minimum distance where the sound is at full volume

                // Adjust the volume based on the distance
                float volume = Mathf.Clamp01(1 - (distance / maxDistance));
                audioSource.volume = volume;
            }
        }
        // You can call this method to give some feedback when the checkpoint is activated.
        private void ChangeCheckpointColor()
        {
            // Example: Change the checkpoint to green (indicating it has been activated).
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = Color.white;
            }
        }
    }
}
