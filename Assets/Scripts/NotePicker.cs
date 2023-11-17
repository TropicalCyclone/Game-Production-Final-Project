using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePicker : MonoBehaviour
{
    [SerializeField] private List<Note> notes;
    private int _pickupOrder = 0;
    // Start is called before the first frame update

    public void StartGame()
    {
        notes[_pickupOrder].PlayMusic();
    }

    public void CorrectPickup()
    {
        notes[_pickupOrder].gameObject.SetActive(false);
        if (_pickupOrder < notes.Count)
        {
            _pickupOrder++;
            notes[_pickupOrder].PlayMusic();
        }
        if (_pickupOrder == notes.Count)
        {
            AllNotesPickedUp();
        }
    }

    public void AllNotesPickedUp()
    {
        Debug.Log("All notes picked up!");
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



