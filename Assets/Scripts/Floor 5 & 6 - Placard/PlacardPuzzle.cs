using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlacardPuzzle : MonoBehaviour
{
    private List<XRSocketInteractor> sockets = new List<XRSocketInteractor>();
    public bool isPuzzleComplete = false;

    [Header("Placard Sockets")]
    public XRSocketInteractor socket1;
    public XRSocketInteractor socket2;
    public XRSocketInteractor socket3;

    [Header("Placards")]
    public XRGrabInteractable placard1;
    public XRGrabInteractable placard2;
    public XRGrabInteractable placard3;

    public Floor56Manager floor56Manager;
    private Dictionary<XRSocketInteractor, XRBaseInteractable> socketPlacardPairs = new Dictionary<XRSocketInteractor, XRBaseInteractable>();

    [System.Obsolete]
    private void Start()
    {
        Debug.Log("PlacardPuzzle Start");
        sockets.AddRange(GetComponentsInChildren<XRSocketInteractor>());

        socketPlacardPairs.Add(socket1, placard1);
        socketPlacardPairs.Add(socket2, placard2);
        socketPlacardPairs.Add(socket3, placard3);

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

            XRBaseInteractable expectedPlacard = socketPlacardPairs[socket];

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
            
            // Deactivate the GameObject when the puzzle is complete
            if (floor56Manager != null)
            {
                floor56Manager.HideAllObjects();
            }
        }
        else
        {
            Debug.Log("Puzzle Incomplete.");
            // You can handle other cases here
        }
    }
}