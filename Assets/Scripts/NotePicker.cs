using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NotePicker : MonoBehaviour
{
    [SerializeField] private List<Note> notes;
    private int _pickupOrder = 0;

    [SerializeField] private UnityEvent AllNotesDone;
    // Start is called before the first frame update

    public void StartGame()
    {
        notes[_pickupOrder].PlayMusic();
    }

    public void CorrectPickup()
    {
        notes[_pickupOrder].gameObject.SetActive(false);
        if (_pickupOrder == notes.Count)
        {
            AllNotesPickedUp();
            return;
        }
        
        if (_pickupOrder < notes.Count)
        {
            _pickupOrder++;
            ActivateNote(_pickupOrder);
        }
    }

    public void ActivateNote(int value)
    {
        notes[value].gameObject.SetActive(true);
        notes[value].PlayMusic();
    }

    public void AllNotesPickedUp()
    {
        Debug.Log("All notes picked up!");
        AllNotesDone.Invoke();
        // Add your logic for what happens when all notes are picked up
    }

    public void FailedOrder()
    {
        //Jumpscare();
    }

    public void PickupNote(int order)
    {
       if (order == _pickupOrder)
        {
            CorrectPickup();
        }
        else
        {
            FailedOrder();
        }
    }
}



