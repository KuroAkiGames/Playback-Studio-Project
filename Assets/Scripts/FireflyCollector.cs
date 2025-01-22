using UnityEngine;
using TMPro;

public class FireflyCollector : MonoBehaviour
{
    public TextMeshProUGUI fireflyText; // Reference to TMP Text for firefly count
    public int fireflyCount = 0;        // Current number of fireflies collected
    public AudioSource playerAudioSource; // Reference to Player's AudioSource
    public AudioClip fireflyCollectSound; // The sound to play when collecting firefly

    private void Start()
    {
        // Initialize the firefly counter display
        UpdateFireflyUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collides with a firefly
        if (collision.CompareTag("Firefly"))
        {
            CollectFirefly(collision.gameObject);
        }
    }

    private void CollectFirefly(GameObject firefly)
    {
        // Play the collection sound using the Player's AudioSource
        if (playerAudioSource != null && fireflyCollectSound != null)
        {
            playerAudioSource.PlayOneShot(fireflyCollectSound); // Play sound instantly
        }
        else
        {
            Debug.LogWarning("AudioSource or Firefly Collect Sound is missing!");
        }

        // Increment firefly count and update UI
        fireflyCount += 1;
        UpdateFireflyUI();

        // Destroy the firefly GameObject immediately
        Destroy(firefly);
    }

    private void UpdateFireflyUI()
    {
        // Update the text with a three-digit formatted number (e.g., 000, 001, 002)
        fireflyText.text = fireflyCount.ToString("D3");
    }
}
