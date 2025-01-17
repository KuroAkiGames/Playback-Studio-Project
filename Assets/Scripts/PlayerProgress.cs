using UnityEngine;

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
