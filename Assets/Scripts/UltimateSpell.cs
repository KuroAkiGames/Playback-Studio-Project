using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UltimateSpell : MonoBehaviour
{
    [Header("UI Elements")]
    public Text chantText; // UI Text to display the chant
    public Image thunderboltImage; // PNG of the thunderbolt
    public CanvasGroup screenFlash; // CanvasGroup for the white flash effect

    [Header("Audio")]
    public AudioSource thunderSFX; // Thunder sound effect
    public AudioSource backgroundMusic; // Optional dramatic chase music

    [Header("Timing")]
    public float chantLineInterval = 4f; // Time between each chant line
    public float flashDuration = 1f; // Duration of the white flash at the end
    public float slowMotionDuration = 2f; // Duration of slow motion effect

    [Header("Chant Lines")]
    [TextArea] public string[] chantLines; // Array of chant lines

    private int currentLineIndex = 0;
    private bool chantComplete = false;
    private bool isInSlowMotion = false;

    private void Start()
    {
        // Initialize UI and visuals
        chantText.text ="";
        thunderboltImage.enabled = false;
        screenFlash.alpha = 0;

        // Start chant sequence
        StartCoroutine(PlayChant());
    }

    private IEnumerator PlayChant()
    {
        // Display each line of the chant over time
        while (currentLineIndex < chantLines.Length)
        {
            chantText.text = chantLines[currentLineIndex];
            currentLineIndex++;

            yield return new WaitForSeconds(chantLineInterval);
        }

        // Chant is complete, trigger the finale
        chantComplete = true;
        StartCoroutine(TriggerThunderstrike());
    }

    private IEnumerator TriggerThunderstrike()
    {
        // Start slow motion effect
        StartSlowMotion();

        // Flash the screen white
        float elapsedTime = 0f;
        while (elapsedTime < flashDuration)
        {
            screenFlash.alpha = Mathf.Lerp(0, 1, elapsedTime / flashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        screenFlash.alpha = 1;

        // Show the thunderbolt image and play SFX
        thunderboltImage.enabled = true;
        thunderSFX.Play();

        // Wait for the slow motion effect duration
        yield return new WaitForSeconds(slowMotionDuration);

        // Fade out the flash
        elapsedTime = 0f;
        while (elapsedTime < flashDuration)
        {
            screenFlash.alpha = Mathf.Lerp(1, 0, elapsedTime / flashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // End the level
        EndLevel();
    }

    private void StartSlowMotion()
    {
        // Reduce time scale to simulate slow motion
        Time.timeScale = 0.2f; // Slow motion factor, you can adjust this value
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // Adjust physics time step to match the new time scale

        // Optionally, you could apply slow motion to specific animations or movements for your character and wolf
        // You can add effects like a "blur" or "vignette" during this time to heighten the slow motion effect
    }

    private void EndLevel()
    {
        // Reset time scale before transitioning to the next scene
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        // Replace with your scene transition or game-ending logic
        SceneManager.LoadScene("NextScene"); // Load the next scene
    }

    private void OnDestroy()
    {
        // Ensure time scale is reset when the object is destroyed (to prevent global slow-motion from affecting other scenes)
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}
