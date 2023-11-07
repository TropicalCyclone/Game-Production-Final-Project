using UnityEngine;

public class TriggerAudio : MonoBehaviour
{
    public AudioClip TriggerSound;
    public float Volume = 0.5f;

    [HideInInspector]
    private bool isPlayed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayed)
        {
            if (TriggerSound)
            {
                AudioSource audioSource = gameObject.GetComponent<AudioSource>();

                if (!audioSource)
                {
                    audioSource = gameObject.AddComponent<AudioSource>();
                }

                audioSource.clip = TriggerSound;
                audioSource.volume = Volume;
                audioSource.Play();

                isPlayed = true;
            }
        }
    }
}