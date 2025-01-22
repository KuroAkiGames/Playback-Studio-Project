using UnityEngine;
using UnityEngine.UI;

public class QuitGameButton : MonoBehaviour
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
        button.onClick.AddListener(OnQuitButtonClick);
    }

    private void OnQuitButtonClick()
    {
        // Quit the application
        Debug.Log("Quitting the game...");
        Application.Quit();

        // If you're testing in the Unity editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
