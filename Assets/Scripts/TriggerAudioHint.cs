using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioClip audioClip;
    public float maxVolume = 1.0f;
    
    private bool isInsideCollider = false;
    private AudioSource audioSource;

    private void Start()
    {
        // Try to get the AudioSource component on the GameObject
        audioSource = GetComponent<AudioSource>();

        // If there's no AudioSource component, create one
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = audioClip;
        audioSource.volume = 0.0f;
        audioSource.loop = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInsideCollider = true;
            audioSource.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isInsideCollider)
        {
            Debug.Log("The audio is playing.");
            Vector3 playerPosition = other.transform.position;
            Vector3 center = transform.position;
            float distance = Vector3.Distance(playerPosition, center);
            float maxDistance = transform.localScale.x * 0.5f; // Max distance based on collider size
            float volume = Mathf.Clamp(1.0f - (distance / maxDistance), 0, 1.0f);

            audioSource.volume = volume * maxVolume;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInsideCollider = false;
            audioSource.Stop();
        }
    }
}