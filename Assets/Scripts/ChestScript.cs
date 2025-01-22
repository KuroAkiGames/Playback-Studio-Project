using UnityEngine;

public class ChestScript : MonoBehaviour
{
    private Animator chestAnimator;
    private bool hasPlayerCollided = false;

    // Audio Source for the chest
    public AudioSource chestAudioSource;
    public AudioClip openChestSound;  // The sound to play when the chest opens

    void Start()
    {
        chestAnimator = GetComponent<Animator>();

        // Ensure the chest has an AudioSource
        if (chestAudioSource == null)
        {
            chestAudioSource = GetComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player collides with the chest
        if (collision.CompareTag("Player"))
        {
            hasPlayerCollided = true;
            Debug.Log("Player has collided with chest");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hasPlayerCollided = false;
    }

    void Update()
    {
        if (hasPlayerCollided && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Open Chest");

            // Play the chest opening sound
            if (chestAudioSource != null && openChestSound != null)
            {
                chestAudioSource.PlayOneShot(openChestSound);
            }
            else
            {
                Debug.LogWarning("AudioSource or openChestSound is missing!");
            }

            // Trigger chest open animation
            chestAnimator.SetBool("bOpenChest", true);
        }
    }
}
