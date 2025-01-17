using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireflyScript : MonoBehaviour
{
    // Score related to the firefly collection
    private int score = 0;

    // Reference to the TextMeshPro score text component
    public TextMeshProUGUI scoreText;

    // Call this method when a coin (firefly) is collected
    public void CoinCollected()
    {
        // Increase the score by 1
        score++;

        // Update the score on the UI text
        scoreText.text = $"Score: {score}";
    }

    // Detect when a trigger event happens
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object colliding is the player
        if (collision.CompareTag("Player"))
        {
            // Update the score in the appropriate script
            // Assuming the score is managed by another object (like a GameManager or ScoreManager)
            // ScoreManager.instance.AddScore(1); // This assumes you have a GameManager script that manages the score

            // Destroy the firefly object after it's collected
            Destroy(gameObject);
        }
    }
}
