using UnityEngine;

public class DayNightSkyBox : MonoBehaviour
{
    public float dayCycleInMinutes = 1f; // Total duration of a day in minutes
    private float _timeOfDay;           // Tracks the current time of day (0 to 1)
    private float _dayCycleInSeconds;   // Duration of a day in seconds

    void Start()
    {
        _dayCycleInSeconds = dayCycleInMinutes * 60f;
    }

    void Update()
    {
        // Increment the time of day
        _timeOfDay += Time.deltaTime / _dayCycleInSeconds;

        // Loop the time of day back to 0 after it completes a full cycle
        if (_timeOfDay > 1f)
            _timeOfDay -= 1f;

        // Blend between day and night based on the time of day
        RenderSettings.skybox.SetFloat("_Blend", Mathf.PingPong(_timeOfDay * 2, 1f));
    }
}
