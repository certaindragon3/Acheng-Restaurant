using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

namespace AchengRestaurant.Core
{
    /// <summary>
    /// VR locomotion controller supporting both continuous movement and teleportation
    /// Attach this to your XR Origin
    ///
    /// Requirements:
    /// - XR Origin in scene
    /// - XR Interaction Toolkit package installed
    /// - Input actions configured for XR controllers
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class XRMovementController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 1.5f;
        [SerializeField] private float sprintMultiplier = 1.5f;
        [SerializeField] private bool enableSmoothMovement = true;
        [SerializeField] private bool enableTeleportation = true;

        [Header("Rotation Settings")]
        [SerializeField] private float snapTurnAngle = 30f;
        [SerializeField] private bool enableSnapTurn = true;

        [Header("Comfort Settings")]
        [Tooltip("Reduce peripheral vision during movement to reduce motion sickness")]
        [SerializeField] private bool enableVignette = false;
        [SerializeField] private float vignetteIntensity = 0.5f;

        [Header("Input Actions")]
        [SerializeField] private InputActionReference moveAction;
        [SerializeField] private InputActionReference turnAction;
        [SerializeField] private InputActionReference sprintAction;

        // Components
        private CharacterController characterController;
        private Transform xrCamera;

        // State
        private bool isSprinting = false;
        private float lastTurnTime = 0f;
        private const float TURN_COOLDOWN = 0.3f; // Prevent rapid turning

        #region Unity Lifecycle

        void Awake()
        {
            // Get components
            characterController = GetComponent<CharacterController>();

            // Find XR camera (usually under XR Origin)
            xrCamera = Camera.main?.transform;
            if (xrCamera == null)
            {
                Debug.LogError("[XRMovement] Main camera not found! Make sure XR Origin is set up correctly.");
            }
        }

        void OnEnable()
        {
            // Enable input actions
            if (moveAction != null)
                moveAction.action.Enable();
            if (turnAction != null)
                turnAction.action.Enable();
            if (sprintAction != null)
                sprintAction.action.Enable();
        }

        void OnDisable()
        {
            // Disable input actions
            if (moveAction != null)
                moveAction.action.Disable();
            if (turnAction != null)
                turnAction.action.Disable();
            if (sprintAction != null)
                sprintAction.action.Disable();
        }

        void Update()
        {
            // Handle continuous movement
            if (enableSmoothMovement)
            {
                HandleContinuousMovement();
            }

            // Handle snap turning
            if (enableSnapTurn)
            {
                HandleSnapTurn();
            }

            // Handle sprint toggle
            HandleSprint();
        }

        #endregion

        #region Movement Logic

        /// <summary>
        /// Handle continuous movement with left thumbstick
        /// </summary>
        private void HandleContinuousMovement()
        {
            if (moveAction == null || xrCamera == null)
                return;

            // Read input
            Vector2 inputAxis = moveAction.action.ReadValue<Vector2>();

            if (inputAxis.magnitude < 0.1f)
                return; // Dead zone

            // Calculate movement direction based on camera forward
            Vector3 cameraForward = xrCamera.forward;
            Vector3 cameraRight = xrCamera.right;

            // Flatten to XZ plane (no vertical movement)
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            // Combine input with camera direction
            Vector3 moveDirection = cameraForward * inputAxis.y + cameraRight * inputAxis.x;

            // Apply speed
            float currentSpeed = moveSpeed;
            if (isSprinting)
                currentSpeed *= sprintMultiplier;

            Vector3 movement = moveDirection * currentSpeed * Time.deltaTime;

            // Move using CharacterController
            characterController.Move(movement);

            // Apply vignette effect if enabled
            if (enableVignette && inputAxis.magnitude > 0.1f)
            {
                // TODO: Implement vignette shader effect
                // This reduces peripheral vision during movement to combat motion sickness
            }
        }

        /// <summary>
        /// Handle snap turning with right thumbstick
        /// </summary>
        private void HandleSnapTurn()
        {
            if (turnAction == null)
                return;

            // Check cooldown
            if (Time.time - lastTurnTime < TURN_COOLDOWN)
                return;

            // Read input
            Vector2 turnInput = turnAction.action.ReadValue<Vector2>();

            // Only turn if thumbstick is pushed significantly left or right
            if (Mathf.Abs(turnInput.x) < 0.7f)
                return;

            // Determine turn direction
            float turnAngle = turnInput.x > 0 ? snapTurnAngle : -snapTurnAngle;

            // Rotate XR Origin
            transform.RotateAround(xrCamera.position, Vector3.up, turnAngle);

            // Update cooldown
            lastTurnTime = Time.time;

            Debug.Log($"[XRMovement] Snap turn: {turnAngle}°");
        }

        /// <summary>
        /// Handle sprint toggle
        /// </summary>
        private void HandleSprint()
        {
            if (sprintAction == null)
                return;

            // Read sprint button (trigger)
            float sprintValue = sprintAction.action.ReadValue<float>();
            isSprinting = sprintValue > 0.5f;
        }

        #endregion

        #region Teleportation

        /// <summary>
        /// Teleport to a specific position
        /// Called by teleportation system or other scripts
        /// </summary>
        public void TeleportTo(Vector3 targetPosition)
        {
            if (!enableTeleportation)
            {
                Debug.LogWarning("[XRMovement] Teleportation is disabled");
                return;
            }

            // Calculate offset between XR Origin and camera
            Vector3 offset = transform.position - xrCamera.position;
            offset.y = 0; // Keep original height

            // Move XR Origin to target position (accounting for camera offset)
            Vector3 newPosition = targetPosition + offset;
            transform.position = newPosition;

            Debug.Log($"[XRMovement] Teleported to: {targetPosition}");
        }

        #endregion

        #region Public API

        /// <summary>
        /// Set movement speed at runtime
        /// </summary>
        public void SetMoveSpeed(float speed)
        {
            moveSpeed = Mathf.Max(0.1f, speed);
        }

        /// <summary>
        /// Enable/disable continuous movement
        /// </summary>
        public void SetContinuousMovementEnabled(bool enabled)
        {
            enableSmoothMovement = enabled;
        }

        /// <summary>
        /// Enable/disable teleportation
        /// </summary>
        public void SetTeleportationEnabled(bool enabled)
        {
            enableTeleportation = enabled;
        }

        /// <summary>
        /// Enable/disable vignette comfort feature
        /// </summary>
        public void SetVignetteEnabled(bool enabled)
        {
            enableVignette = enabled;
        }

        #endregion

        #region Debug Helpers

        [ContextMenu("Print Movement State")]
        private void PrintMovementState()
        {
            Debug.Log($"=== XR MOVEMENT STATE ===\n" +
                      $"Speed: {moveSpeed} m/s\n" +
                      $"Sprinting: {isSprinting}\n" +
                      $"Continuous Movement: {enableSmoothMovement}\n" +
                      $"Teleportation: {enableTeleportation}\n" +
                      $"Snap Turn: {enableSnapTurn} ({snapTurnAngle}°)");
        }

        #endregion
    }
}
