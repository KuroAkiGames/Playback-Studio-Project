using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Pause Menu UI")]
    public GameObject pauseMenuUI; // The pause menu UI to toggle visibility
    public Slider volumeSlider; // Volume slider to control game volume
    public KeyCode pauseKey = KeyCode.P; // Key to toggle pause state

    private bool isPaused = false; // Track if the game is paused

    void Start()
    {
        // Ensure that the pause menu is hidden initially
        pauseMenuUI.SetActive(false);

        // Initialize volume slider to match current volume level
        volumeSlider.value = AudioListener.volume;

        // Add listener to volume slider to update the volume in real-time
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void Update()
    {
        // Toggle pause when the specified key is pressed (P)
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // Pause the game (freeze time and show the menu)
    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Freeze game time (pauses all gameplay)
        pauseMenuUI.SetActive(true); // Show the pause menu
    }

    // Resume the game (unfreeze time and hide the menu)
    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume normal game time
        pauseMenuUI.SetActive(false); // Hide the pause menu
    }

    // Set the volume based on the slider's value
    void SetVolume(float volume)
    {
        AudioListener.volume = volume; // Adjust the audio listener volume
    }

    // Optionally, you can add other buttons for "Restart" or "Quit" in your pause menu UI
    public void QuitGame()
    {
        Time.timeScale = 1f; // Ensure time is resumed when quitting
        Application.Quit(); // Quit the application (works in build)
    }

    // Restart the game by reloading the current scene
    public void RestartGame()
    {
        Time.timeScale = 1f; // Ensure time is resumed when restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload current scene
    }
}
