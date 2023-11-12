using UnityEngine;

public class TriggerAudio : MonoBehaviour
{
    public AudioClip[] TriggerSounds;
    public float Volume = 0.5f;
    public AudioPlayer audioPlayer; // Drag and drop the GameObject with the AudioPlayer script attached.

    [HideInInspector]
    private bool isPlayed;

    public enum PlayMode
    {
        Single,
        Multiple
    }

    public PlayMode playMode = PlayMode.Single;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayed)
        {
            if (TriggerSounds != null && TriggerSounds.Length > 0 && audioPlayer != null)
            {
                if (playMode == PlayMode.Single)
                {
                    int randomIndex = Random.Range(0, TriggerSounds.Length);
                    audioPlayer.PlayAudio(TriggerSounds[randomIndex], Volume);
                }
                else if (playMode == PlayMode.Multiple)
                {
                    foreach (AudioClip clip in TriggerSounds)
                    {
                        audioPlayer.PlayAudio(clip, Volume);
                    }
                }

                isPlayed = true;
            }
        }
    }
}