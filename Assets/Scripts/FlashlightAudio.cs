using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightAudio : MonoBehaviour
{
    private float startVolume;
    private bool start;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip Walkaudio;
    [SerializeField] private AudioClip Runaudio;
    [SerializeField] private bool isMovement;

    private bool isWalk = false;
    private bool run;
    // Start is called before the first frame update
    void Awake()
    {
        if (!isMovement)
        {
            startVolume = _audioSource.volume;
            _audioSource.volume = 0;
            _audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWalk()
    {
        
        AudioClip audio;
        if (run)
        {
            audio = Runaudio;
        }
        else
        {
            audio = Walkaudio;      
        }
        _audioSource.clip = audio;
        if (!isWalk)
        {
            
            _audioSource.Play();
            Debug.Log("Walking");
            isWalk = true;
        }
    }

    public void EndWalk()
    {
        isWalk = false;
        _audioSource.Stop();
    }

    public void RunStart()
    {
        run = true;
    }
    public void RunEnd()
    {
        run = false;
    }
    public void ShakeStart()
    {
        _audioSource.volume = 1f;
        //StartCoroutine(FadeIn(_audioSource,0.5f));
        Debug.Log("Shake start");
    }

    public void ShakeStop()
    {
        _audioSource.volume = 0f;
        //StartCoroutine(FadeOut(_audioSource, 0.5f));
        Debug.Log("Shake stop");
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
