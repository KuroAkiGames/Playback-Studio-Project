using UnityEngine;

public class BGMController : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play(); // Start playing the BGM
    }

    public void StopBGM()
    {
        audioSource.Stop(); // Stop the music
    }

    public void ChangeVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume); // Adjust the volume (0 to 1)
    }
}
