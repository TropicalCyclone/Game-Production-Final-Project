using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NotesPuzzle : MonoBehaviour
{
    // This is still in-order from placard 1 to socket 1 (I just turned the multiple variable to an array)

    private List<XRSocketInteractor> sockets = new List<XRSocketInteractor>();
    public bool isPuzzleComplete = false;

    [Header("Placard Sockets")]
    public List<XRSocketInteractor> placardSockets = new List<XRSocketInteractor>();

    [Header("Placards")]
    public List<XRGrabInteractable> placards = new List<XRGrabInteractable>();

    private Dictionary<XRSocketInteractor, XRGrabInteractable> socketPlacardPairs = new Dictionary<XRSocketInteractor, XRGrabInteractable>();

    [System.Obsolete]
    private void Start()
    {
        Debug.Log("PlacardPuzzle Start");
        sockets.AddRange(GetComponentsInChildren<XRSocketInteractor>());

        // Assign placards to sockets in any order
        for (int i = 0; i < Mathf.Min(placardSockets.Count, placards.Count); i++)
        {
            socketPlacardPairs.Add(placardSockets[i], placards[i]);
        }

        foreach (XRSocketInteractor socket in sockets)
        {
            socket.onSelectEnter.AddListener(OnObjectInserted);
            socket.onSelectExit.AddListener(OnObjectRemoved);
        }
    }

    [System.Obsolete]
    private void OnObjectInserted(XRBaseInteractable interactable)
    {
        Debug.Log("OnObjectInserted: " + interactable.name);
        CheckCondition();
    }

    [System.Obsolete]
    private void OnObjectRemoved(XRBaseInteractable interactable)
    {
        Debug.Log("OnObjectRemoved: " + interactable.name);
        CheckCondition();
    }

    [System.Obsolete]
    private void CheckCondition()
    {
        bool allSocketsFilledCorrectly = true;

        foreach (XRSocketInteractor socket in sockets)
        {
            if (!socketPlacardPairs.ContainsKey(socket))
            {
                continue;
            }

            XRGrabInteractable expectedPlacard = socketPlacardPairs[socket];

            if (socket.selectTarget != expectedPlacard)
            {
                allSocketsFilledCorrectly = false;
                break;
            }
        }

        isPuzzleComplete = allSocketsFilledCorrectly;

        if (isPuzzleComplete)
        {
            Debug.Log("Puzzle Complete!");
        }
        else
        {
            Debug.Log("Puzzle Incomplete.");
            // You can handle other cases here
        }
    }
}