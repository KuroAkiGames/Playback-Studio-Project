using UnityEngine;
using UnityEngine.SceneManagement; // Required to check the active scene

public class PlayerProgress : MonoBehaviour
{
    public static bool hasSuperJump = false;  // Track if the player has unlocked the Super Jump skill

    void Awake()
    {
        if (PlayerPrefs.HasKey("HasSuperJump"))
        {
            hasSuperJump = PlayerPrefs.GetInt("HasSuperJump") == 1;
        }
        else
        {
            hasSuperJump = false;
        }

        if (SceneManager.GetActiveScene().name == "Lv2Swamp")
        {
            hasSuperJump = true; // Enable SuperJump for this level
        }
        Debug.Log($"SuperJump Enabled in {SceneManager.GetActiveScene().name}: {hasSuperJump}");
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
