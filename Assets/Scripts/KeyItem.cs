using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyItem : MonoBehaviour
{
    public List<XRBaseInteractable> interactables = new List<XRBaseInteractable>();
    public GameObject[] objectsToDisable;

    private int interactedCount = 0;
    private HashSet<XRBaseInteractable> interactedSet = new HashSet<XRBaseInteractable>();

    [System.Obsolete]
    void Start()
    {
        // Add all interactable objects to the list.
        XRBaseInteractable[] allInteractables = GetComponents<XRBaseInteractable>();
        interactables.AddRange(allInteractables);

        foreach (XRBaseInteractable interactable in interactables)
        {
            interactable.onSelectEnter.AddListener(OnSelectEnter);
        }
    }

    [System.Obsolete]
    private void OnDestroy()
    {
        foreach (XRBaseInteractable interactable in interactables)
        {
            interactable.onSelectEnter.RemoveListener(OnSelectEnter);
        }
    }

    [System.Obsolete]
    private void OnSelectEnter(XRBaseInteractor interactor)
    {
        if (interactables.Contains(interactor.selectTarget))
        {
            // Check if it's the first interaction
            if (!interactedSet.Contains(interactor.selectTarget)) 
            {
                interactedSet.Add(interactor.selectTarget);
                interactedCount++;

                if (interactedCount == interactables.Count)
                {
                    foreach (GameObject obj in objectsToDisable)
                    {
                        obj.SetActive(false);
                    }
                }
            }
        }
    }
}