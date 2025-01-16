using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle2D : MonoBehaviour
{
    [Header("Cycle Settings")]
    public float dayCycleInMinutes = 1f; // Total duration of a full day-night cycle in minutes

    [Header("Background Colors")]
    public SpriteRenderer background;  // Background sprite to change color dynamically
    public Color dayColor = Color.cyan; // Bright blue for the day
    public Color duskColor = new Color(1f, 0.5f, 0f); // Orange for dusk
    public Color nightColor = Color.black; // Dark blue/black for night

    [Header("Sun and Moon")]
    public Transform sun; // The sun sprite
    public Transform moon; // The moon sprite
    public Transform skyCenter; // The pivot point for sun and moon rotation

    [Header("Lighting (Optional)")]
    public Light2D globalLight; // For 2D URP lighting
    public float dayIntensity = 1f;
    public float nightIntensity = 0.2f;

    private float _timeOfDay; // Tracks current time of day (0 = midnight, 1 = next midnight)
    private float _dayCycleInSeconds;

    private void Start()
    {
        // Convert cycle duration to seconds
        _dayCycleInSeconds = dayCycleInMinutes * 60f;

        // Ensure valid references
        if (!background) Debug.LogError("Background SpriteRenderer not assigned.");
        if (!skyCenter) Debug.LogError("Sky Center pivot not assigned.");
    }

    private void Update()
    {
        // Update the time of day
        _timeOfDay += Time.deltaTime / _dayCycleInSeconds;

        // Loop time back to 0 after completing a full day
        if (_timeOfDay > 1f)
            _timeOfDay -= 1f;

        UpdateSky();
    }

    private void UpdateSky()
    {
        // Move the sun and moon around the sky center
        if (sun && moon)
        {
            float sunAngle = Mathf.Lerp(-90f, 270f, _timeOfDay); // Sun moves from left to right
            float moonAngle = sunAngle - 180f; // Moon is on the opposite side of the sun

            sun.position = CalculateSkyPosition(sunAngle);
            moon.position = CalculateSkyPosition(moonAngle);

            sun.gameObject.SetActive(_timeOfDay >= 0.25f && _timeOfDay <= 0.75f); // Sun active during the day
            moon.gameObject.SetActive(!sun.gameObject.activeSelf); // Moon active at night
        }

        // Change background color based on time of day
        if (background)
        {
            if (_timeOfDay < 0.25f) // Early morning (night to day)
                background.color = Color.Lerp(nightColor, dayColor, _timeOfDay / 0.25f);
            else if (_timeOfDay < 0.5f) // Daytime
                background.color = dayColor;
            else if (_timeOfDay < 0.75f) // Evening (day to night)
                background.color = Color.Lerp(dayColor, nightColor, (_timeOfDay - 0.5f) / 0.25f);
            else // Nighttime
                background.color = nightColor;
        }

        // Adjust global lighting intensity (if using URP lighting)
        if (globalLight)
        {
            if (_timeOfDay < 0.25f) // Early morning
                globalLight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, _timeOfDay / 0.25f);
            else if (_timeOfDay < 0.5f) // Daytime
                globalLight.intensity = dayIntensity;
            else if (_timeOfDay < 0.75f) // Evening
                globalLight.intensity = Mathf.Lerp(dayIntensity, nightIntensity, (_timeOfDay - 0.5f) / 0.25f);
            else // Nighttime
                globalLight.intensity = nightIntensity;
        }
    }

    private Vector3 CalculateSkyPosition(float angle)
    {
        // Calculate position for the sun/moon based on the angle
        float radians = angle * Mathf.Deg2Rad;
        float radius = 10f; // Distance from the center (adjust as needed)
        return new Vector3(
            skyCenter.position.x + Mathf.Cos(radians) * radius,
            skyCenter.position.y + Mathf.Sin(radians) * radius,
            0f
        );
    }
}
