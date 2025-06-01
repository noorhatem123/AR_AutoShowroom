using UnityEngine;

public class EngineSoundController : MonoBehaviour
{
    private AudioSource audioSource;
    public bool isRunning = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleEngine()
    {
        if (!audioSource) return;

        if (isRunning)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.Play();
        }

        isRunning = !isRunning;
    }
}
