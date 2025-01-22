using UnityEngine;

public class HealthUIManager : MonoBehaviour
{
    private static HealthUIManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // Persist across scenes
    }
}