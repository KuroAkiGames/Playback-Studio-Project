using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3; // Max health per life
    public int currentHealth;

    [Header("Lives Settings")]
    public int maxLives = 3; // Max lives
    public int currentLives;

    [Header("UI Settings")]
    public Image[] lifeHearts; // Assign heart UI images in the inspector
    public Sprite fullHeart;  // Sprite for a full heart
    public Sprite emptyHeart; // Sprite for an empty heart

    [Header("References")]
    public PlayerController playerController;

    private bool isDead = false;

    void Start()
    {
        // Initialize health and lives
        currentHealth = maxHealth;
        currentLives = maxLives;
        UpdateLifeUI();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            LoseLife();
        }
        else
        {
            UpdateLifeUI(); // Update health UI
        }
    }

    public void LoseLife()
    {
        currentLives--;
        UpdateLifeUI();

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            StartCoroutine(HandleDeathAndRespawn());
        }
    }

    public void Heal(int amount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            UpdateLifeUI();
        }
    }

    private IEnumerator HandleDeathAndRespawn()
    {
        isDead = true;

        // Trigger death animation
        playerController.TriggerDeathAnimation();

        yield return new WaitForSeconds(2f); // Wait for death animation to play

        // Respawn logic
        currentHealth = maxHealth;
        playerController.Respawn();

        isDead = false;
        UpdateLifeUI();
    }

    private void GameOver()
    {
        isDead = true;
        Debug.Log("Game Over!");

        // Load the GameOver scene
        SceneManager.LoadScene("GameOver");
    }

    private void UpdateLifeUI()
    {
        // Update hearts based on current lives
        for (int i = 0; i < lifeHearts.Length; i++)
        {
            if (i < currentLives)
            {
                lifeHearts[i].sprite = fullHeart;
                lifeHearts[i].enabled = true; // Show heart
            }
            else
            {
                lifeHearts[i].sprite = emptyHeart;
                lifeHearts[i].enabled = false; // Hide heart
            }
        }
    }
}
