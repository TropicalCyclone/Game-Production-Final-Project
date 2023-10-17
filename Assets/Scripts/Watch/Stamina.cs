using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class Stamina : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.XR.Interaction.Toolkit.ContinuousMoveProviderBase continuousMoveProviderBase;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float RunSpeed;
    [SerializeField]
    private HealthBarBehaviour healthBar;
    [SerializeField]
    private float MaxStamina;
    [SerializeField]
    private float playerStamina;
    [SerializeField]
    private float dSpeed;
    [SerializeField]
    private Vector2 move;
    private Vector2 lastMove;

    

    private bool isRunning = false;
    private bool isMoving;
    private bool canRun;


    public InputActionReference JoystickClick = null;
    public InputActionReference PlayerMove;



    private void Awake()
    {
        if (continuousMoveProviderBase)
        {
            moveSpeed = continuousMoveProviderBase.moveSpeed;
        }
        playerStamina = MaxStamina;
        healthBar.SetHealth(playerStamina, MaxStamina);
        JoystickClick.action.started += Running;
    }
    private void OnDestroy()
    {
        JoystickClick.action.started -= Running;
    }

    private void Update()
    {

        move = PlayerMove.action.ReadValue<Vector2>();
        if (move != lastMove)
        {
            isMoving = move.magnitude > 0.1f;
            lastMove = move;
        }

        if (isRunning && playerStamina > 0)
        {
            
            DecreaseEnergy();
        }
        else
        {
            if (playerStamina <= 0)
            {
                isRunning = false;
            }

            // Enable running when stamina is at least 50%
            if (playerStamina >= MaxStamina * 0.5f)
            {
                canRun = true;
            }

            if (!isRunning && playerStamina < MaxStamina)
            {
                IncreaseEnergy();
            }
        }

        if (!isMoving)
        {
            isRunning = false;
        }

        if (isRunning)
        {
            continuousMoveProviderBase.moveSpeed = RunSpeed;
        }
        else
        {
            continuousMoveProviderBase.moveSpeed = moveSpeed;
        }
    }

    private void DecreaseEnergy()
    {
        playerStamina -= dSpeed * Time.deltaTime;
        healthBar.SetHealth(playerStamina,MaxStamina);
        //UpdateStaminaBar();

        // Disable running when stamina is less than or equal to 0
        if (playerStamina <= 0)
        {
            isRunning = false;
            canRun = false;
        }
    }

    private void IncreaseEnergy()
    {
        playerStamina += dSpeed * Time.deltaTime;
        playerStamina = Mathf.Clamp(playerStamina, 0f, MaxStamina);
        healthBar.SetHealth(playerStamina, MaxStamina);
        //UpdateStaminaBar();

        // Enable running when stamina is at least 50%
        if (playerStamina >= MaxStamina * 0.5f)
        {
            canRun = true;
        }
    }

    private void Running(InputAction.CallbackContext context)
    {
        if (canRun && playerStamina > 0)
        {
            isRunning = true;
        }
    }

}
