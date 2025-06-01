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
}
