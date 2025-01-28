using System.Collections;
using System.Collections.Generic;
using UnityEngine;




using System.Collections;
using UnityEngine;


public class PineconeSpawner : MonoBehaviour
{
    [Header("Pinecone Settings")]
    public GameObject pineconePrefab; // Prefab for the pinecone
    public float spawnInterval = 2f; // Time interval between spawns (e.g., 2 seconds)
    public int maxPinecones = 20; // Max number of pinecones that can spawn
    public float spawnHeight = 50f; // Height where the pinecone spawns (Y-axis)
    public float fallSpeed = 5f; // Speed of the pinecone falling if not using physics

    private int pineconesSpawned = 0; // Track the number of pinecones spawned

    void Start()
    {
        // Start the spawning coroutine
        StartCoroutine(SpawnPinecones());
    }

    private IEnumerator SpawnPinecones()
    {
        while (pineconesSpawned < maxPinecones) // Spawn up to maxPinecones
        {
            SpawnPinecone();
            pineconesSpawned++; // Increment the counter after each spawn
            yield return new WaitForSeconds(spawnInterval); // Wait before spawning the next pinecone
        }
    }

    private void SpawnPinecone()
    {
        // Calculate the camera's visible width in world space
        float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;

        // Add margin to both sides (e.g., 1 unit each side)
        float margin = 1f;
        float minX = -cameraWidth + margin;  // Left margin
        float maxX = cameraWidth - margin;   // Right margin

        // Randomize the X position within the camera's viewport width
        float randomX = Random.Range(-cameraWidth, cameraWidth);

        // Spawn position at the top of the screen with the random X and fixed Y (spawnHeight)
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, 0f);

        // Instantiate the pinecone prefab
        GameObject newPinecone = Instantiate(pineconePrefab, spawnPosition, Quaternion.identity);

        // Add the PineconeBehavior script for falling logic
        PineconeBehavior pineconeBehavior = newPinecone.AddComponent<PineconeBehavior>();
        pineconeBehavior.fallSpeed = fallSpeed;
    }
}

public class PineconeBehavior : MonoBehaviour
{
    [Header("Pinecone Settings")]
    public float fallSpeed = 5f; // Speed at which the pinecone falls, used for manual movement

    private Rigidbody2D rb;

    void Start()
    {
        // Get the Rigidbody2D component if it exists
        rb = GetComponent<Rigidbody2D>();

        // If Rigidbody2D is found, it will fall automatically due to gravity
        if (rb != null)
        {
            rb.gravityScale = 1; // Use Unity's built-in gravity
        }
    }

    void Update()
    {
        // If no Rigidbody2D, manually move the pinecone
        if (rb == null)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ensure the player object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Find the HealthManager component on the player
            HealthManager healthManager = other.GetComponent<HealthManager>();

            // If the player has the HealthManager component, reduce their health/lives
            if (healthManager != null)
            {
                healthManager.TakeDamage(1); // Adjust the value (1) based on how much damage you want to deal
            }

            // Destroy the pinecone upon collision
            Destroy(gameObject);
        }

        // If the pinecone hits the ground, destroy it
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
