using UnityEngine;
using UnityEngine.SceneManagement; // Required to check the active scene

public class PlayerProgress : MonoBehaviour
{
    public static bool hasSuperJump = false;  // Track if the player has unlocked the Super Jump skill

    void Awake()
    {
        // Load the progress state (will persist between scenes)
        if (PlayerPrefs.HasKey("HasSuperJump"))
        {
            hasSuperJump = PlayerPrefs.GetInt("HasSuperJump") == 1;
        }
        else
        {
            hasSuperJump = false;
        }

        // Check if the current level is Lv2Swamp
        if (SceneManager.GetActiveScene().name == "Lv2Swamp")
        {
            hasSuperJump = true; // Enable SuperJump for this level
        }
        Debug.Log($"SuperJump Enabled: {PlayerProgress.hasSuperJump}");

    }

    public static void UnlockSuperJump()
    {
        // Set the skill as unlocked
        hasSuperJump = true;

        // Save this progress to PlayerPrefs
        PlayerPrefs.SetInt("HasSuperJump", 1);
        PlayerPrefs.Save();
    }
}
