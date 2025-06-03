using UnityEngine;

public class VoiceoverPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayVoiceover()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    public void StopVoiceover()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

}
