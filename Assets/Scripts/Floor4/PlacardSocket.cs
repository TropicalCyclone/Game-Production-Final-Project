using System.Net.Sockets;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlacardSocket : XRSocketInteractor
{   
    // To only accept the placards
    
    [System.Obsolete]
    public override bool CanSelect(XRBaseInteractable interactable)
    {
        if (interactable is PlacardObject)
        {
            return base.CanSelect(interactable);
        }

        return false;
    }
}
