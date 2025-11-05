using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

namespace AchengRestaurant.Core
{
    /// <summary>
    /// Controls VR hand/controller visualization and interaction ray
    /// Attach this to each XR Controller (Left and Right)
    ///
    /// Requirements:
    /// - XR Interaction Toolkit package
    /// - XRRayInteractor component on same GameObject
    /// - Input actions configured
    /// </summary>
    public class XRHandController : MonoBehaviour
    {
        [Header("Hand Type")]
        [SerializeField] private Hand handType = Hand.Right;

        [Header("Visual Elements")]
        [SerializeField] private GameObject handModel;
        [SerializeField] private GameObject controllerModel;
        [SerializeField] private LineRenderer rayLine;

        [Header("Ray Settings")]
        [SerializeField] private float rayMaxDistance = 10f;
        [SerializeField] private Color rayDefaultColor = Color.white;
        [SerializeField] private Color rayHoverColor = Color.green;
        [SerializeField] private float rayWidth = 0.02f;

        [Header("Haptic Feedback")]
        [SerializeField] private float hapticIntensity = 0.5f;
        [SerializeField] private float hapticDuration = 0.1f;

        [Header("Input Actions")]
        [SerializeField] private InputActionReference selectAction;
        [SerializeField] private InputActionReference activateAction;
        [SerializeField] private InputActionReference gripAction;

        // Components
        private UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor rayInteractor;

        // State
        private bool isHoveringObject = false;
        private GameObject currentHoverTarget;

        public enum Hand
        {
            Left,
            Right
        }

        #region Unity Lifecycle

        void Awake()
        {
            // Get XRRayInteractor component
            rayInteractor = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor>();
            if (rayInteractor == null)
            {
                Debug.LogWarning($"[XRHand-{handType}] XRRayInteractor not found! Adding one...");
                rayInteractor = gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor>();
            }

            // Initialize ray line
            if (rayLine != null)
            {
                InitializeRayLine();
            }
        }

        void OnEnable()
        {
            // Subscribe to interaction events
            if (rayInteractor != null)
            {
                rayInteractor.hoverEntered.AddListener(OnHoverEntered);
                rayInteractor.hoverExited.AddListener(OnHoverExited);
                rayInteractor.selectEntered.AddListener(OnSelectEntered);
                rayInteractor.selectExited.AddListener(OnSelectExited);
            }

            // Enable input actions
            if (selectAction != null)
                selectAction.action.Enable();
            if (activateAction != null)
                activateAction.action.Enable();
            if (gripAction != null)
                gripAction.action.Enable();
        }

        void OnDisable()
        {
            // Unsubscribe from interaction events
            if (rayInteractor != null)
            {
                rayInteractor.hoverEntered.RemoveListener(OnHoverEntered);
                rayInteractor.hoverExited.RemoveListener(OnHoverExited);
                rayInteractor.selectEntered.RemoveListener(OnSelectEntered);
                rayInteractor.selectExited.RemoveListener(OnSelectExited);
            }

            // Disable input actions
            if (selectAction != null)
                selectAction.action.Disable();
            if (activateAction != null)
                activateAction.action.Disable();
            if (gripAction != null)
                gripAction.action.Disable();
        }

        void Update()
        {
            // Update ray visual
            UpdateRayVisual();
        }

        #endregion

        #region Ray Visualization

        /// <summary>
        /// Initialize ray line renderer
        /// </summary>
        private void InitializeRayLine()
        {
            rayLine.startWidth = rayWidth;
            rayLine.endWidth = rayWidth;
            rayLine.material = new Material(Shader.Find("Sprites/Default"));
            rayLine.startColor = rayDefaultColor;
            rayLine.endColor = rayDefaultColor;
            rayLine.positionCount = 2;
        }

        /// <summary>
        /// Update ray visual each frame
        /// </summary>
        private void UpdateRayVisual()
        {
            if (rayLine == null)
                return;

            // Start position (controller position)
            Vector3 startPos = transform.position;
            rayLine.SetPosition(0, startPos);

            // End position (raycast hit point or max distance)
            Vector3 endPos = startPos + transform.forward * rayMaxDistance;

            // Check if hovering over something
            if (rayInteractor != null && rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                endPos = hit.point;
                rayLine.startColor = rayHoverColor;
                rayLine.endColor = rayHoverColor;
            }
            else
            {
                rayLine.startColor = rayDefaultColor;
                rayLine.endColor = rayDefaultColor;
            }

            rayLine.SetPosition(1, endPos);
        }

        /// <summary>
        /// Enable or disable ray visual
        /// </summary>
        public void SetRayVisible(bool visible)
        {
            if (rayLine != null)
                rayLine.enabled = visible;
        }

        #endregion

        #region Interaction Events

        /// <summary>
        /// Called when controller starts hovering over an object
        /// </summary>
        private void OnHoverEntered(HoverEnterEventArgs args)
        {
            isHoveringObject = true;
            currentHoverTarget = args.interactableObject.transform.gameObject;

            Debug.Log($"[XRHand-{handType}] Hover entered: {currentHoverTarget.name}");

            // Trigger light haptic feedback
            TriggerHaptic(hapticIntensity * 0.3f, hapticDuration * 0.5f);
        }

        /// <summary>
        /// Called when controller stops hovering over an object
        /// </summary>
        private void OnHoverExited(HoverExitEventArgs args)
        {
            isHoveringObject = false;
            currentHoverTarget = null;

            Debug.Log($"[XRHand-{handType}] Hover exited");
        }

        /// <summary>
        /// Called when controller selects (triggers) an object
        /// </summary>
        private void OnSelectEntered(SelectEnterEventArgs args)
        {
            GameObject selectedObject = args.interactableObject.transform.gameObject;
            Debug.Log($"[XRHand-{handType}] Selected: {selectedObject.name}");

            // Trigger haptic feedback
            TriggerHaptic(hapticIntensity, hapticDuration);
        }

        /// <summary>
        /// Called when controller releases selection
        /// </summary>
        private void OnSelectExited(SelectExitEventArgs args)
        {
            Debug.Log($"[XRHand-{handType}] Selection released");
        }

        #endregion

        #region Haptic Feedback

        /// <summary>
        /// Trigger haptic feedback on controller
        /// </summary>
        public void TriggerHaptic(float intensity, float duration)
        {
            // Unity XR Interaction Toolkit haptics
            // Note: Actual implementation depends on your XR device
            // This is a placeholder for the concept

            if (rayInteractor != null && rayInteractor.xrController != null)
            {
                rayInteractor.xrController.SendHapticImpulse(intensity, duration);
            }
        }

        /// <summary>
        /// Trigger a quick haptic pulse
        /// </summary>
        public void TriggerQuickPulse()
        {
            TriggerHaptic(0.5f, 0.05f);
        }

        #endregion

        #region Hand Model Management

        /// <summary>
        /// Show hand model (hide controller model)
        /// Useful for hand tracking
        /// </summary>
        public void ShowHandModel()
        {
            if (handModel != null)
                handModel.SetActive(true);
            if (controllerModel != null)
                controllerModel.SetActive(false);
        }

        /// <summary>
        /// Show controller model (hide hand model)
        /// Useful for controller-based interaction
        /// </summary>
        public void ShowControllerModel()
        {
            if (handModel != null)
                handModel.SetActive(false);
            if (controllerModel != null)
                controllerModel.SetActive(true);
        }

        #endregion

        #region Public API

        /// <summary>
        /// Check if currently hovering over an object
        /// </summary>
        public bool IsHovering()
        {
            return isHoveringObject;
        }

        /// <summary>
        /// Get currently hovered object
        /// </summary>
        public GameObject GetHoverTarget()
        {
            return currentHoverTarget;
        }

        /// <summary>
        /// Get hand type
        /// </summary>
        public Hand GetHandType()
        {
            return handType;
        }

        #endregion

        #region Debug Helpers

        [ContextMenu("Trigger Test Haptic")]
        private void TestHaptic()
        {
            TriggerHaptic(1.0f, 0.2f);
        }

        [ContextMenu("Print Hand State")]
        private void PrintHandState()
        {
            Debug.Log($"=== XR HAND STATE ({handType}) ===\n" +
                      $"Hovering: {isHoveringObject}\n" +
                      $"Target: {(currentHoverTarget != null ? currentHoverTarget.name : "None")}\n" +
                      $"Ray Visible: {(rayLine != null && rayLine.enabled)}");
        }

        #endregion
    }
}
