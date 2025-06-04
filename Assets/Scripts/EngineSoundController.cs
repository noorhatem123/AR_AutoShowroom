using UnityEngine;  // Gives access to Unity's core features like AudioSource

// This script controls the car engine sound: turning it on/off and resetting it
public class EngineSoundController : MonoBehaviour
{
    public AudioSource audioSource;  // The engine sound to play (drag the sound here in the Inspector)

    private bool engineOn = false;   // Tracks whether the engine is currently on or off

    // This function is called when the user taps the "Toggle Engine" button
    public void ToggleEngine()
    {
        if (engineOn)
        {
            audioSource.Stop();      // If engine is already on, stop the sound
        }
        else
        {
            audioSource.Play();      // If engine is off, start playing the sound
        }

        engineOn = !engineOn;        // Flip the state (true becomes false, false becomes true)
    }

    // This function stops the engine sound and marks engine as off
    // Useful for resetting the car
    public void StopEngine()
    {
        // Only stop if the sound is playing and AudioSource is valid
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();     // Stop playing the sound
            engineOn = false;       // Make sure the state is set to "off"
        }
    }
}
