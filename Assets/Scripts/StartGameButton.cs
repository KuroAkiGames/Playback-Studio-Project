using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    public string sceneToLoad = "Lv1Forest"; // Name of the scene to load

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
        button.onClick.AddListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad);
    }
}