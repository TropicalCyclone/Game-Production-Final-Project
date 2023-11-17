using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class SoundTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip _soundEffect;
    public void PlayAudio(AudioClip clip)
    {
        float clipLength = clip.length;

        source.clip = clip;
        source.Play();
        StartCoroutine(StartMethod(clipLength));
    }

    private IEnumerator StartMethod(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);

        Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(_soundEffect)
            PlayAudio(_soundEffect);
        }
    }
}
