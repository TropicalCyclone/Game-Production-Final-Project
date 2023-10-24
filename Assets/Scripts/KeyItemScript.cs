using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyItemScript : MonoBehaviour
{
    public XRBaseInteractable interactable;
    public GameObject[] objectsToDisable;

    private bool interacted = false;

    [System.Obsolete]
    void Start()
    {
        if (interactable == null)
        {
            interactable = GetComponent<XRBaseInteractable>();
        }
        
        interactable.onSelectEnter.AddListener(OnSelectEnter);
    }

    [System.Obsolete]
    private void OnDestroy()
    {
        interactable.onSelectEnter.RemoveListener(OnSelectEnter);
    }

    private void OnSelectEnter(XRBaseInteractor interactor)
    {
        if (!interacted)
        {
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(false);
            }

            interacted = true;
        }
    }
}
