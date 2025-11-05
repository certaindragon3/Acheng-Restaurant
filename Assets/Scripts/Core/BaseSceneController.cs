using UnityEngine;
using AchengRestaurant.Data;

namespace AchengRestaurant.Core
{
    /// <summary>
    /// BASE CLASS for all scene controllers
    /// CRITICAL: Every scene MUST have exactly ONE controller that inherits from this
    ///
    /// STATE MANAGEMENT RULES:
    /// 1. OnSceneEnter() - Called when scene becomes active
    ///    - Subscribe to SessionManager events here
    ///    - Initialize scene-specific state from SessionManager
    ///    - DO NOT assume any initial values
    ///
    /// 2. OnSceneExit() - Called when scene is being unloaded
    ///    - Unsubscribe from ALL events (prevent memory leaks!)
    ///    - Save scene state to SessionManager if needed
    ///    - Stop all coroutines
    ///    - Clean up temporary objects
    ///
    /// 3. NEVER use static variables for scene-specific data
    /// 4. ALWAYS clean up in OnSceneExit() - this prevents state pollution
    /// </summary>
    public abstract class BaseSceneController : MonoBehaviour
    {
        [Header("Scene Info")]
        [SerializeField] protected string sceneName;
        [SerializeField] protected GameState expectedState;

        protected bool _isSceneActive = false;
        public bool IsSceneActive => _isSceneActive;

        #region Unity Lifecycle

        /// <summary>
        /// Called when scene is loaded (Unity lifecycle)
        /// DO NOT OVERRIDE - use OnSceneEnter() instead
        /// </summary>
        protected virtual void OnEnable()
        {
            _isSceneActive = true;
            OnSceneEnter();
        }

        /// <summary>
        /// Called when scene is unloaded (Unity lifecycle)
        /// DO NOT OVERRIDE - use OnSceneExit() instead
        /// </summary>
        protected virtual void OnDisable()
        {
            _isSceneActive = false;
            OnSceneExit();
        }

        protected virtual void Start()
        {
            // Validate scene setup
            ValidateSceneSetup();
        }

        #endregion

        #region Scene Lifecycle (Override These)

        /// <summary>
        /// Called when scene becomes active
        /// OVERRIDE THIS to initialize your scene
        /// </summary>
        protected virtual void OnSceneEnter()
        {
            Debug.Log($"[{GetType().Name}] Scene Enter: {sceneName}");

            // Subscribe to session events
            SubscribeToEvents();

            // Load scene state from SessionManager
            LoadSceneState();
        }

        /// <summary>
        /// Called when scene is being unloaded
        /// OVERRIDE THIS to clean up your scene
        /// CRITICAL: Always call base.OnSceneExit() to ensure cleanup!
        /// </summary>
        protected virtual void OnSceneExit()
        {
            Debug.Log($"[{GetType().Name}] Scene Exit: {sceneName}");

            // Unsubscribe from ALL events (prevent memory leaks)
            UnsubscribeFromEvents();

            // Save scene state to SessionManager if needed
            SaveSceneState();

            // Stop all coroutines
            StopAllCoroutines();
        }

        /// <summary>
        /// Subscribe to SessionManager events
        /// OVERRIDE THIS to add your event subscriptions
        /// </summary>
        protected virtual void SubscribeToEvents()
        {
            // Example:
            // SessionManager.Instance.OnStateChanged += HandleStateChanged;
            // SessionManager.Instance.OnDishCompleted += HandleDishCompleted;
        }

        /// <summary>
        /// Unsubscribe from ALL events
        /// OVERRIDE THIS to match your SubscribeToEvents()
        /// CRITICAL: Every += in SubscribeToEvents() needs a -= here!
        /// </summary>
        protected virtual void UnsubscribeFromEvents()
        {
            // Example:
            // SessionManager.Instance.OnStateChanged -= HandleStateChanged;
            // SessionManager.Instance.OnDishCompleted -= HandleDishCompleted;
        }

        /// <summary>
        /// Load scene-specific state from SessionManager
        /// OVERRIDE THIS if your scene needs to restore state
        /// </summary>
        protected virtual void LoadSceneState()
        {
            // Example:
            // int dishCount = SessionManager.Instance.GetCompletedDishCount();
            // UpdateUIWithDishCount(dishCount);
        }

        /// <summary>
        /// Save scene-specific state to SessionManager
        /// OVERRIDE THIS if your scene needs to persist state
        /// </summary>
        protected virtual void SaveSceneState()
        {
            // Example:
            // If user was in middle of cooking tutorial, save progress
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Transition to another scene
        /// Helper method that uses SceneTransitionManager
        /// </summary>
        protected void TransitionToScene(string targetScene)
        {
            Debug.Log($"[{GetType().Name}] Requesting transition to: {targetScene}");

            switch (targetScene)
            {
                case SceneTransitionManager.SCENE_TUTORIAL:
                    SceneTransitionManager.Instance.LoadTutorial();
                    break;
                case SceneTransitionManager.SCENE_MAIN_GALLERY:
                    SceneTransitionManager.Instance.LoadMainGallery();
                    break;
                case SceneTransitionManager.SCENE_COOKING_TUTORIAL:
                    // Note: This should pass a dishID
                    Debug.LogWarning("LoadCookingTutorial requires dishID - use LoadCookingTutorial(dishID) instead");
                    break;
                case SceneTransitionManager.SCENE_REUNION_DINNER:
                    SceneTransitionManager.Instance.LoadReunionDinner();
                    break;
                default:
                    Debug.LogError($"Unknown scene: {targetScene}");
                    break;
            }
        }

        /// <summary>
        /// Get current session data (shortcut)
        /// </summary>
        protected SessionData GetSessionData()
        {
            return SessionManager.Instance.CurrentSession;
        }

        /// <summary>
        /// Get current game state (shortcut)
        /// </summary>
        protected GameState GetCurrentState()
        {
            return SessionManager.Instance.CurrentState;
        }

        #endregion

        #region Validation

        /// <summary>
        /// Validate scene setup in editor
        /// Warns if scene name or expected state are not set
        /// </summary>
        private void ValidateSceneSetup()
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogWarning($"[{GetType().Name}] Scene name not set in inspector!");
            }

            if (GetCurrentState() != expectedState && GetCurrentState() != GameState.Transitioning)
            {
                Debug.LogWarning($"[{GetType().Name}] State mismatch! Expected: {expectedState}, Actual: {GetCurrentState()}");
            }
        }

        #endregion

        #region Debug Helpers

        [ContextMenu("Print Scene Info")]
        protected void PrintSceneInfo()
        {
            Debug.Log($"=== SCENE INFO ===\n" +
                      $"Controller: {GetType().Name}\n" +
                      $"Scene Name: {sceneName}\n" +
                      $"Expected State: {expectedState}\n" +
                      $"Current State: {GetCurrentState()}\n" +
                      $"Active: {_isSceneActive}");
        }

        #endregion
    }
}
