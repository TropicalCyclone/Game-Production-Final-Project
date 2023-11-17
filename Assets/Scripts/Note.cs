using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Note : MonoBehaviour
{
    [SerializeField] private NotePicker _picker;
    [SerializeField] private int _order;
    [SerializeField] private AudioSource _audioSource;
    private Note _note;
    public Note GetNote { get { return _note; } }

    void Start()
    {
        _note = this;
    }

    public void NotePickup()
    {
        _picker.PickupNote(_order);
    }

    public void PlayMusic()
    {
        _audioSource.Play();
    }
}