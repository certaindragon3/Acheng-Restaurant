using UnityEngine;
using AchengRestaurant.Core;
using AchengRestaurant.Data;

namespace AchengRestaurant.Scenes
{
    /// <summary>
    /// Controller for Tutorial scene
    /// Teaches users basic VR controls and interactions
    ///
    /// This is an EXAMPLE of how to use BaseSceneController
    /// Shows proper event subscription, state management, and scene transitions
    /// </summary>
    public class TutorialSceneController : BaseSceneController
    {
        [Header("Tutorial Settings")]
        [SerializeField] private bool allowSkipTutorial = true;
        [SerializeField] private float tutorialTimeoutSeconds = 300f; // 5 minutes

        [Header("UI References")]
        [SerializeField] private GameObject tutorialUI;
        [SerializeField] private GameObject skipButton;

        // Tutorial state
        private int currentStepIndex = 0;
        private bool tutorialCompleted = false;
        private float tutorialStartTime;

        #region Tutorial Steps (Example)

        private readonly string[] tutorialSteps = new string[]
        {
            "Welcome to Acheng Restaurant VR Experience",
            "Use the left thumbstick to move around",
            "Use the right thumbstick to snap turn",
            "Point at objects with your controller",
            "Press trigger to select objects",
            "Try picking up an object with grip button",
            "Great! You're ready to explore"
        };

        #endregion

        #region Scene Lifecycle (BaseSceneController Implementation)

        protected override void OnSceneEnter()
        {
            base.OnSceneEnter();

            // Set scene info (for base class)
            sceneName = SceneTransitionManager.SCENE_TUTORIAL;
            expectedState = GameState.InTutorial;

            // Initialize tutorial
            InitializeTutorial();

            Debug.Log("[TutorialScene] Tutorial started");
        }

        protected override void SubscribeToEvents()
        {
            // Subscribe to state changes if needed
            SessionManager.Instance.OnStateChanged += HandleStateChanged;

            // Note: Tutorial scene doesn't need dish completion events
            // but we subscribe anyway to show the pattern
        }

        protected override void UnsubscribeFromEvents()
        {
            // CRITICAL: Unsubscribe from ALL events
            SessionManager.Instance.OnStateChanged -= HandleStateChanged;
        }

        protected override void LoadSceneState()
        {
            // Check if user has already completed tutorial
            SessionData session = GetSessionData();

            if (session.tutorialCompleted)
            {
                Debug.Log("[TutorialScene] User already completed tutorial - allowing skip");
                if (skipButton != null)
                    skipButton.SetActive(true);
            }
        }

        protected override void SaveSceneState()
        {
            // Tutorial doesn't have persistent state to save
            // (completion is handled by CompleteTutorial())
        }

        protected override void OnSceneExit()
        {
            Debug.Log("[TutorialScene] Tutorial exited");
            base.OnSceneExit();
        }

        #endregion

        #region Tutorial Logic

        /// <summary>
        /// Initialize tutorial UI and state
        /// </summary>
        private void InitializeTutorial()
        {
            currentStepIndex = 0;
            tutorialCompleted = false;
            tutorialStartTime = Time.time;

            // Show tutorial UI
            if (tutorialUI != null)
                tutorialUI.SetActive(true);

            // Show skip button if allowed
            if (skipButton != null)
                skipButton.SetActive(allowSkipTutorial);

            // Display first step
            DisplayCurrentStep();
        }

        /// <summary>
        /// Display current tutorial step
        /// </summary>
        private void DisplayCurrentStep()
        {
            if (currentStepIndex >= tutorialSteps.Length)
            {
                CompleteTutorial();
                return;
            }

            string stepText = tutorialSteps[currentStepIndex];
            Debug.Log($"[TutorialScene] Step {currentStepIndex + 1}/{tutorialSteps.Length}: {stepText}");

            // TODO: Update UI text to show current step
            // tutorialText.text = stepText;
        }

        /// <summary>
        /// Advance to next tutorial step
        /// Call this when user completes current step
        /// </summary>
        public void AdvanceToNextStep()
        {
            currentStepIndex++;
            DisplayCurrentStep();
        }

        /// <summary>
        /// Complete tutorial and transition to main gallery
        /// </summary>
        private void CompleteTutorial()
        {
            if (tutorialCompleted)
                return;

            tutorialCompleted = true;

            // Mark tutorial as completed in SessionManager
            SessionManager.Instance.CompleteTutorial();

            Debug.Log("[TutorialScene] Tutorial completed! Transitioning to Gallery...");

            // Small delay before transition
            Invoke(nameof(TransitionToGallery), 2f);
        }

        /// <summary>
        /// Skip tutorial (if allowed)
        /// </summary>
        public void SkipTutorial()
        {
            if (!allowSkipTutorial)
            {
                Debug.LogWarning("[TutorialScene] Skip not allowed");
                return;
            }

            Debug.Log("[TutorialScene] Tutorial skipped");
            CompleteTutorial();
        }

        /// <summary>
        /// Transition to main gallery scene
        /// </summary>
        private void TransitionToGallery()
        {
            SceneTransitionManager.Instance.LoadMainGallery();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handle state changes
        /// </summary>
        private void HandleStateChanged(GameState oldState, GameState newState)
        {
            Debug.Log($"[TutorialScene] State changed: {oldState} → {newState}");

            // Example: If we're transitioning out, hide UI
            if (newState == GameState.Transitioning)
            {
                if (tutorialUI != null)
                    tutorialUI.SetActive(false);
            }
        }

        #endregion

        #region Unity Lifecycle

        void Update()
        {
            // Check for timeout (optional)
            if (!tutorialCompleted && allowSkipTutorial)
            {
                float elapsed = Time.time - tutorialStartTime;
                if (elapsed > tutorialTimeoutSeconds)
                {
                    Debug.Log("[TutorialScene] Tutorial timeout - auto-advancing");
                    CompleteTutorial();
                }
            }

            // TODO: Check for user input to advance steps
            // Example: if (Input.GetKeyDown(KeyCode.Space)) AdvanceToNextStep();
        }

        #endregion

        #region Debug Helpers

        [ContextMenu("Force Complete Tutorial")]
        private void ForceCompleteTutorial()
        {
            CompleteTutorial();
        }

        [ContextMenu("Reset Tutorial Progress")]
        private void ResetTutorialProgress()
        {
            currentStepIndex = 0;
            tutorialCompleted = false;
            DisplayCurrentStep();
        }

        #endregion
    }
}
