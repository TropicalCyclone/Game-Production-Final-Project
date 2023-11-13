using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;

public class Stamina : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.XR.Interaction.Toolkit.ContinuousMoveProviderBase continuousMoveProviderBase;

    [Header("Player Speed")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float RunSpeed;

    [Header("Stamina")]
    [SerializeField] private float MaxStamina;
    [SerializeField] private float playerStamina;

    [Header("Stamina Decrease/Increase Speed")]
    [SerializeField] private float dSpeed;

    [Header("Healthbar Script")]
    [SerializeField] private HealthBarBehaviour healthBar;

    [Header("Events")]
    [SerializeField] private UnityEvent isWalking;
    [SerializeField] private UnityEvent playerIsRunning;

    private Vector2 move;
    private Vector2 lastMove;

    [SerializeField]
    private Image Status;
    [SerializeField]
    private Sprite RunningSprite, TiredSprite;
    
    private bool isRunning = false, isMoving, canRun;

    public InputActionReference JoystickClick = null, PlayerMove;

    public void SetMoveSpeed(float input)
    {
        moveSpeed = input;

    }

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
        healthBar.SetHealth(playerStamina, MaxStamina);
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
        }
        if (!isRunning && playerStamina < MaxStamina)
        {
            IncreaseEnergy();
        }

        if (!isMoving)
        {
            isRunning = false;
        }
        else
        {
            isWalking.Invoke();
        }
        if (continuousMoveProviderBase)
        {
            if (isRunning)
            {
                playerIsRunning.Invoke();
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
        if (healthBar)
        {
            healthBar.SetHealth(playerStamina, MaxStamina);
        }

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
        if (healthBar)
        {
            healthBar.SetHealth(playerStamina, MaxStamina);
        }

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
