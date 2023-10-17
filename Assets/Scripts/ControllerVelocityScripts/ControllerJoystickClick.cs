using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerJoystickClick : MonoBehaviour
{
    public InputActionReference JoystickClick = null;


    private void Awake()
    {
        JoystickClick.action.started += Action;
    }
    private void OnDestroy()
    {
        JoystickClick.action.started -= Action;
    }

    private void Action(InputAction.CallbackContext context)
    {

    }
}
