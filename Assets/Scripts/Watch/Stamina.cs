using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.XR.Interaction.Toolkit.ContinuousMoveProviderBase continuousMoveProviderBase;
    [SerializeField]
    private float moveSpeed, RunSpeed, MaxStamina, playerStamina, dSpeed;
    [SerializeField]
    private HealthBarBehaviour healthBar;
    private Vector2 move;
    private Vector2 lastMove;

    [SerializeField]
    private Image Status;
    [SerializeField]
    private Sprite RunningSprite, TiredSprite;
    
    private bool isRunning = false, isMoving, canRun;

    public InputActionReference JoystickClick = null, PlayerMove;



    private void Awake()
    {
        if (continuousMoveProviderBase)
        {
            moveSpeed = continuousMoveProviderBase.moveSpeed;
        }
        playerStamina = MaxStamina;
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
            continuousMoveProviderBase.moveSpeed = RunSpeed;
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
        if (continuousMoveProviderBase)
        {
            if (isRunning)
            {
                continuousMoveProviderBase.moveSpeed = RunSpeed;
            }
            else
            {
                continuousMoveProviderBase.moveSpeed = moveSpeed;
            }
        }
        if (Status)
        {
            if (canRun)
            {
                Status.sprite = RunningSprite;
            }
            else
            {
                continuousMoveProviderBase.moveSpeed = moveSpeed / 3f;
                Status.sprite = TiredSprite;
            }
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
