using UnityEngine;
using UnityEngine.SceneManagement; // Required to load scenes

public class LevelLoader : MonoBehaviour
{
    [Tooltip("The name of the specific scene to load. Leave empty to load the next scene in build order.")]
    public string sceneToLoad;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collided with the object
        if (collision.gameObject.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Trigger version for entering trigger zones
        if (other.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // Load a specific scene
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            // Load the next scene in the build index
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.LogWarning("No more scenes to load! Make sure to set the build order in Build Settings.");
            }
        }
    }
}