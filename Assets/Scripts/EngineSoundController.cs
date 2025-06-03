using UnityEngine;

public class EngineSoundController : MonoBehaviour
{
    public AudioSource audioSource;

    private bool engineOn = false;

    public void ToggleEngine()
    {
        if (engineOn)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.Play();
        }

        engineOn = !engineOn;
    }

    public void StopEngine()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            engineOn = false;
        }
    }
}
