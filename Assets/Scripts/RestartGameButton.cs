using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartGameButton : MonoBehaviour
{
    private void Start()
    {
        // Add a Button component to this GameObject if it's missing
        Button button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("No Button component found on this GameObject. Please attach a Button component.");
            return;
        }

        // Add a listener to the button click event
        button.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnRestartButtonClick()
    {
        // Retrieve the last active scene name stored in PlayerPrefs
        string lastScene = PlayerPrefs.GetString("LastScene", "Lv1Forest"); // Default to "Lv1Forest" if no scene is found

        // Load the last scene the player was in
        SceneManager.LoadScene(lastScene);
    }

    public static void SetLastScene(string sceneName)
    {
        // Store the current scene name when transitioning to the Game Over scene
        PlayerPrefs.SetString("LastScene", sceneName);
        PlayerPrefs.Save(); // Ensure the data is saved
    }
}
