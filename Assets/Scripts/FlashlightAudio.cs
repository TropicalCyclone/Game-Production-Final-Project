using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightAudio : MonoBehaviour
{
    private float startVolume;
    [SerializeField] private AudioSource _audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        startVolume = _audioSource.volume;
        _audioSource.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShakeStart()
    {
        StartCoroutine(FadeIn(_audioSource,0.5f));
    }

    public void ShakeStop()
    {
        StartCoroutine(FadeOut(_audioSource, 0.5f));
    }

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
    }

    public IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.pitch = (Random.Range(0.6f, .9f));
        audioSource.Play();
        audioSource.volume = 0;
        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }       
    }
}
