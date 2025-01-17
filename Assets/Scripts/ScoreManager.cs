using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Singleton instance
    public static ScoreManager instance;

    // The current score
    private int score = 0;

    // Reference to the UI text component to show the score
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        // Ensure there's only one instance of the GameManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to add score
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = $"Score: {score}";
    }
}
