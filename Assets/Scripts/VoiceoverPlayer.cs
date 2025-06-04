using UnityEngine;  // Gives access to Unity features like AudioSource and MonoBehaviour

// This script handles playing and stopping a voiceover sound for the car
public class VoiceoverPlayer : MonoBehaviour
{
    public AudioSource audioSource;  // Assign the voiceover sound here in the Inspector

    // Plays the voiceover sound if it's not already playing
    public void PlayVoiceover()
    {
        // Check if the AudioSource is valid and not currently playing
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();  // Start playing the voiceover
        }
    }

    // Stops the voiceover sound if it's currently playing
    public void StopVoiceover()
    {
        // Check if the AudioSource is valid and currently playing
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();  // Stop the voiceover
        }
    }
}
