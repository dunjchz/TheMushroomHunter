using System.Collections;
using UnityEngine;

public class SoundEffectsController : MonoBehaviour
{
    public AudioClip soundClip;  // Reference to your sound clip
    public float playbackSpeed = 0.5f; // Adjust this value to change playback speed
    public float pauseDuration = 1.0f;  // Adjust this value to change the pause duration
    public float volume = 1.0f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Ensure AudioSource is attached to the GameObject
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the sound clip to the AudioSource
        audioSource.clip = soundClip;
        // Set the sound to loop
        audioSource.loop = true;
        // Set the playback speed
        audioSource.pitch = playbackSpeed;
        // Set volume
        audioSource.volume = volume;
        // Start playing the sound
        audioSource.Play();

        // Start coroutine to add pause between each loop
        StartCoroutine(PlayWithPause());
    }

    IEnumerator PlayWithPause()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(audioSource.clip.length / playbackSpeed);
            audioSource.Stop();  // Stop the sound
            yield return new WaitForSecondsRealtime(pauseDuration); // Wait for the pause duration
            audioSource.Play();  // Start the sound again
        }
    }
}
