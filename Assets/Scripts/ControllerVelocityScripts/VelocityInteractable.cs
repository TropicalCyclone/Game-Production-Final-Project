using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VelocityInteractable : XRGrabInteractable
{
    private FlashlightShake flashlight;
    private ControllerVelocity controllerVelocity = null;
    private float timeSinceLastShake;
    [SerializeField]
    private float MinShakeInterval;
    [SerializeField]
    private float ShakeDetectionThreshold;
    private float sqrShakeDetectionThreshold;

    private void Start()
    {
        flashlight = GetComponent<FlashlightShake>();
        sqrShakeDetectionThreshold = Mathf.Pow(sqrShakeDetectionThreshold, 2);
    }

    protected override void Awake()
    {
        base.Awake();
    }

    
     protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        controllerVelocity = args.interactorObject.transform.GetComponent<ControllerVelocity>();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        controllerVelocity = null;
    }   

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (isSelected)
        {
            if(updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                ApplyVelocity();
            }
        }
    }
    
    private void ApplyVelocity()
    {
        /*
        Vector3 velocity = controllerVelocity ? controllerVelocity.Velocity : Vector3.zero;

        Debug.Log(velocity);

        if (velocity.sqrMagnitude >= ShakeDetectionThreshold && Time.unscaledTime >= timeSinceLastShake + MinShakeInterval)
        {
           flashlight.RechargeBattery();


        }
        */
    }

}
