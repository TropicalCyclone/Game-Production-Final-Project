using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public void PlayAudio(AudioClip audioClip, float volume = 1.0f)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource, audioClip.length); // Automatically destroy the AudioSource after the clip has finished playing.
    }
}