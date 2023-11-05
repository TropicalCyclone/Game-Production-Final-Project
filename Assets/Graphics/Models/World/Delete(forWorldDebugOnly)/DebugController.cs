using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DebugController : MonoBehaviour
	{
		private enum ViewMode
		{
			NormalFirstPerson
		}
		
		// Inputs
		
		private float MouseXInput => Input.GetAxis("Mouse X");
		private float MouseYInput => Input.GetAxis("Mouse Y");

		private bool IsJumpingInput => Input.GetKeyDown(jumpKey) && canJump;
		private bool IsSprintingInput => Input.GetKey(sprintKey) && canSprint;
		private bool IsZoomingInput => Input.GetKey(zoomKey) && canZoom;
		private bool IsHandToolInput => Input.GetKeyDown(handToolKey) && canUseHandTool;
		
		private bool IsCrouchingInput => Input.GetKey(crouchKey) && canCrouch;
		private bool IsHidingCursorInput => Input.GetKey(showCursorKey) && canHideCursor;

		[Header("Controls")]
		[SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
		[SerializeField] private KeyCode jumpKey = KeyCode.Space;
		[SerializeField] private KeyCode crouchKey = KeyCode.C;
		[SerializeField] private KeyCode showCursorKey = KeyCode.LeftAlt;
		[SerializeField] private KeyCode zoomKey = KeyCode.Mouse1;
		[SerializeField] private KeyCode handToolKey = KeyCode.E;

		[Header("Function Options")] 
		[SerializeField] private bool canMove = true;
		[SerializeField] private bool canJump = true;
		[SerializeField] private bool canLook = true;
		[SerializeField] private bool canSprint = true;
		[SerializeField] private bool canCrouch = true;
		[SerializeField] private bool canHideCursor = true;
		[SerializeField] private bool canZoom = true;
		[SerializeField] private bool useStamina = false;
		[SerializeField] private bool canUseHandTool = true;
		[SerializeField] private bool canHeadBob = true;

		[Header("Cameras")] 
		[SerializeField] private ViewMode currentViewMode = ViewMode.NormalFirstPerson;
		[SerializeField] private Camera mainCamera;
		[SerializeField] private GameObject cameraTargetObject;
		[SerializeField] private float maxLookUp = 90.0f;
		[SerializeField] private float maxLookDown = -90.0f;
		[SerializeField] private float normalFOV = 60f;
		[SerializeField] private float zoomFOV = 150f;
		private float _cinemachineTargetPitch;
		private float _cinemachineTargetYaw;


		[Header("Move Parameters")]
		[SerializeField] private float walkSpeed = 4.0f;
		[SerializeField] private float sprintSpeed = 6.0f;
		[SerializeField] private float crouchSpeed = 1.0f;
		[SerializeField] private float slopeSpeedMultiplier  = 0.5f;
		[SerializeField] private float speedChangeRate = 10.0f;
		private CharacterController _controller;
		private Vector2 _currentInput;
		private float _speed;
		private float _verticalVelocity;
		private float _terminalVelocity = 53.0f;

		[Header("Jump Parameters")]
		[SerializeField] private float jumpHeight = 1.2f;
		[SerializeField] private float jumpTimeout = 0.1f; //Time required to pass before being able to jump again. Set to 0f to instantly jump again
		[SerializeField] private float fallTimeout = 0.15f; //Time required to pass before entering the fall state. Useful for walking down stairs
		[SerializeField] private float gravity = -15.0f;
		private float _jumpTimeoutDelta;
		private float _fallTimeoutDelta;

		[Header("Crouch Parameters")] 
		[SerializeField] private bool canStand = true;
		[SerializeField] private bool isCurrentlyCrouching = false;
		[SerializeField] private float crouchHeight = 1f;
		[SerializeField] private float standHeight = 2f;
		[SerializeField] private float crouchTransitionSpeed = 10f;
		[SerializeField] private float standCamTargetHeight = 1.8f;
		[SerializeField] private float crouchCamTargetHeight = 1f;
		[SerializeField] private float crouchTolerance = 0.001f;
		[SerializeField] private float crouchRayOffset = 0.5f; // must match char controller radius

		[Header("Aim Parameters")]
		[SerializeField] private LayerMask aimColliderLayerMask;
		[SerializeField] private bool invertXLook = false;
		[SerializeField] private bool invertYLook = false;
		private Vector3 _aimDirection;
		private Vector3 _aimWorldPosition;
		private float _aimSensitivity = 1f;
		private float _rotationVelocity;
		private float _targetRotation = 0.0f;
		private Collider[] _colliders;
		
		[Header("Player Grounded")]
		[SerializeField] private bool grounded = true;
		[SerializeField] private float groundedOffset = -0.14f; //Useful for rough ground
		[SerializeField] private float groundedRadius = 0.5f; //Should match the radius of the CharacterController
		[SerializeField] private LayerMask groundLayers;
		[SerializeField] private Vector3 groundSpherePosition;
		
		[Header("Stamina Parameters")]
		[SerializeField] private float maxStamina = 100;
		[SerializeField] private float minStaminaUsable = 10f;
		[SerializeField] private float staminaDrain = 5;
		[SerializeField] private float timeBeforeStart = 5;
		[SerializeField] private float staminaRegenValue = 2;
		private float _timeSinceLastStaminaRegen  = 5f;
		private float _currentStamina;
		
		[Header("Headbob Parameters")]
        [SerializeField] private float walkBobSpeed = 14f;
        [SerializeField] private float walkBobAmount = 0.05f;
        [SerializeField] private float SprintBobSpeed = 18f;
        [SerializeField] private float SprintBobAmount = 0.11f;
        [SerializeField] private float crouchBobSpeed = 7f;
        [SerializeField] private float crouchBobAmount = 0.02f;
        private float timer = 20;

		[Header("HandTool Parameters")] 
		[SerializeField] private GameObject currentHandTool; //data type should be changed

		[SerializeField] private float forceMagnitude;

		private const float Threshold = 0.01f;

		public float GetStaminaValue => _currentStamina;
		
		private void Awake()
		{
			// get a reference to our main camera
			if (mainCamera == null)
			{
				mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
			}
		}

		private void Start()
		{
			_cinemachineTargetYaw = cameraTargetObject.transform.rotation.eulerAngles.y;

			
			_controller = GetComponent<CharacterController>();
			
			// reset our timeouts on start
			_jumpTimeoutDelta = jumpTimeout;
			_fallTimeoutDelta = fallTimeout;

			_currentStamina = maxStamina;
			_timeSinceLastStaminaRegen = timeBeforeStart;

		}

		private void Update()
		{
			JumpAndGravity();
			GroundedCheck();
			CursorHandler();
			ZoomHandler();
			Move();
			Crouch();
			StaminaHandler();
			HandToolHandler();
			HandleHeadBob();

			mainCamera.transform.position = cameraTargetObject.transform.position;
			mainCamera.transform.rotation = cameraTargetObject.transform.rotation;

		}

		private void LateUpdate()
		{
			CameraRotation();
		}
		

		private void GroundedCheck()
		{
			// set sphere position, with offset
			groundSpherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
			grounded = Physics.CheckSphere(groundSpherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
		}

		// ***** PLAYER LOOK *****
		
		private void CameraRotation()
		{
			//stop look
			if (!canLook) return;

			Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
			Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
			
			if (currentViewMode == ViewMode.NormalFirstPerson)
			{
				//FIRST PERSON

				if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, aimColliderLayerMask))
				{
					_aimWorldPosition = hit.point;
				}

				// if there is an input
				if (new Vector2(MouseXInput, MouseYInput).sqrMagnitude >= Threshold)
				{
					_cinemachineTargetPitch += MouseYInput * _aimSensitivity;
					
					_rotationVelocity = MouseXInput * _aimSensitivity;

					// clamp our pitch rotation
					_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, maxLookDown, maxLookUp);

					// Update Cinemachine camera target pitch
					cameraTargetObject.transform.localRotation = Quaternion.Euler(invertYLook ? _cinemachineTargetPitch : -_cinemachineTargetPitch, 0.0f, 0.0f);
					
					// rotate the player left and right
					transform.Rotate(Vector3.up * (invertXLook ? -_rotationVelocity : _rotationVelocity));
				}
			}

		}
		
		private void CursorHandler()
		{
			if (IsHidingCursorInput)
			{
				canLook = false;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			else
			{
				canLook = true;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}

		private void ZoomHandler()
		{
			float targetFOV = IsZoomingInput ? zoomFOV : normalFOV;
			mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * 10f);
		}
		
		// ***** PLAYER MOVE *****
		
		private void Move()
		{
			if (!canMove) return;
			
			// set target speed based on move speed, sprint speed and if sprint is pressed
			float targetSpeed = PlayerSpeed();
			
			float moveX = Input.GetAxisRaw("Horizontal");
			float moveY = Input.GetAxisRaw("Vertical");
			_currentInput = new Vector2(moveX, moveY);

			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (_currentInput == Vector2.zero) targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			
			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * speedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}

			// normalise input direction
			Vector3 inputDirection = new Vector3(_currentInput.x, 0.0f, _currentInput.y).normalized;
			
			//adjust speed on slopes
			if (_controller.isGrounded)
			{
				RaycastHit hit;
				Physics.Raycast(transform.position, Vector3.down, out hit);
				float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
				_speed *= Mathf.Max(slopeSpeedMultiplier, Mathf.Cos(slopeAngle * Mathf.Deg2Rad));
			}

			if (currentViewMode == ViewMode.NormalFirstPerson)
			{
				//FIRST PERSON
				// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
				// if there is a move input rotate player when the player is moving
				if (_currentInput != Vector2.zero)
				{
					// move
					inputDirection = transform.right * _currentInput.x + transform.forward * _currentInput.y;
				}
				// move the player
				_controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
			}
			else
			{

				Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

				// move the player
				_controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
				                 new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
			}

		}

		private float PlayerSpeed()
		{
			return (IsSprintingInput && !IsCrouchingInput && !isCurrentlyCrouching) ? sprintSpeed : (IsCrouchingInput || (isCurrentlyCrouching && !canStand)) ? crouchSpeed : walkSpeed;
		}
		
		private void HandleHeadBob()
		{
			if (!canHeadBob) return;
			
			if (!_controller.isGrounded) return;

			if (Mathf.Abs(_controller.velocity.x) > 0.1f || Mathf.Abs(_controller.velocity.z) > 0.1f)
			{
				timer += Time.deltaTime * (IsCrouchingInput ? crouchBobSpeed : IsSprintingInput ? SprintBobSpeed : walkBobSpeed);
				cameraTargetObject.transform.localPosition = new Vector3( 
					  cameraTargetObject.transform.localPosition.x,
					  cameraTargetObject.transform.localPosition.y + Mathf.Sin(timer)* (IsCrouchingInput ? crouchBobAmount : IsSprintingInput ? SprintBobAmount : walkBobAmount),
					  cameraTargetObject.transform.localPosition.z);
			}
		}

		// ***** PLAYER JUMP *****
		
		private void JumpAndGravity()
		{
			if (grounded)
			{
				// reset the fall timeout timer
				_fallTimeoutDelta = fallTimeout;

				// stop our velocity dropping infinitely when grounded
				if (_verticalVelocity < 0.0f)
				{
					_verticalVelocity = -2f;
				}

				// Jump
				if (IsJumpingInput && _jumpTimeoutDelta <= 0.0f)
				{
					// the square root of H * -2 * G = how much velocity needed to reach desired height
					_verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
					
				}

				// jump timeout
				if (_jumpTimeoutDelta >= 0.0f)
				{
					_jumpTimeoutDelta -= Time.deltaTime;
				}
			}
			else
			{
				// reset the jump timeout timer
				_jumpTimeoutDelta = jumpTimeout;

				// fall timeout
				if (_fallTimeoutDelta >= 0.0f)
				{
					_fallTimeoutDelta -= Time.deltaTime;
				}
			}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (_verticalVelocity < _terminalVelocity)
			{
				_verticalVelocity += gravity * Time.deltaTime;
			}
		}

		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}
		
		// ***** PLAYER CROUCH *****

		private void Crouch()
		{
			//weird code but ok
			float crouchedHeight = IsCrouchingInput ? crouchHeight : standHeight;
			float camHeight = IsCrouchingInput ? crouchCamTargetHeight : standCamTargetHeight;
			
			//Checks is there is something above
			// Calculate the starting position for the raycast with the offset
			Vector3 raycastStartPos = transform.position + transform.forward * crouchRayOffset;
			// Use the modified starting position for the raycast
			canStand = !Physics.Raycast(raycastStartPos, Vector3.up, 3f, ~LayerMask.GetMask("Player"));
			
			isCurrentlyCrouching = Math.Abs(_controller.height - crouchHeight) < crouchTolerance;
			
			if (canStand)
			{
				AdjustController(crouchedHeight, camHeight);
			}
			
			if (!canCrouch || !canStand) return;
			
			if (Math.Abs(_controller.height - crouchedHeight) > crouchTolerance) 
			{
				AdjustController(crouchedHeight, camHeight);
			}

		}
		
		private void AdjustController(float height, float camHeight)
		{
			float center = height / 2;

			_controller.height = Mathf.LerpUnclamped(_controller.height, height, crouchTransitionSpeed * Time.deltaTime);
			_controller.center = Vector3.LerpUnclamped(_controller.center, new Vector3(0, center, 0), crouchTransitionSpeed * Time.deltaTime);
			
			cameraTargetObject.transform.localPosition = Vector3.LerpUnclamped(cameraTargetObject.transform.localPosition, new Vector3(0, camHeight, 0), crouchTransitionSpeed * Time.deltaTime);
		}

		// ***** PLAYER INTERACTION *****

		private void HandToolHandler()
		{
			if (IsHandToolInput)
			{
				currentHandTool.SetActive(!currentHandTool.activeInHierarchy);
			}
		}

		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			Rigidbody rb = hit.collider.attachedRigidbody;

			if (rb != null)
			{
				Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
				forceDirection.y = 0;
				forceDirection.Normalize();
				
				rb.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
			}
		}

		// ***** PLAYER STAMINA *****

		private void StaminaHandler()
		{
			if (!useStamina) return;

			if (_timeSinceLastStaminaRegen <= timeBeforeStart)
			{
				_timeSinceLastStaminaRegen += Time.deltaTime;
			}

			if (IsSprintingInput && _currentInput != Vector2.zero)
			{
				_currentStamina -= staminaDrain * Time.deltaTime;

				if (_currentStamina < 0)
					_currentStamina = 0;

				if (_currentStamina <= 0)
				{
					canSprint = false;
					
					// Check if the stamina has reached the minimum usable threshold
					if (_currentStamina >= minStaminaUsable)
					{
						// Allow sprinting again once the minimum usable stamina is reached
						canSprint = true;
					}
				}

				// Reset timeSinceLastStaminaRegen if the player is sprinting
				_timeSinceLastStaminaRegen = 0f;
			}
			else if (!IsSprintingInput && _currentStamina < maxStamina )
			{
				// Check if enough time has passed since the last stamina regen
				if (_timeSinceLastStaminaRegen >= timeBeforeStart)
				{
					_currentStamina += staminaRegenValue * Time.deltaTime;

					if (_currentStamina > maxStamina)
						_currentStamina = maxStamina;

					// Set canSprint to true if stamina is greater than zero
					canSprint = _currentStamina > 0 && _currentStamina >= minStaminaUsable;
				}
			}
		}
		
		// ***** DEBUG *****
		
		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;

			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(groundSpherePosition, groundedRadius);
			
		}
	}