using UnityEngine;
using System.Collections.Generic;
using AchengRestaurant.Core;
using AchengRestaurant.Data;

namespace AchengRestaurant.Scenes
{
    /// <summary>
    /// Controller for Main Gallery scene
    /// Central hub where users select dishes to learn
    ///
    /// This demonstrates:
    /// - Managing multiple interactable objects (dish displays)
    /// - Responding to session progress (reunion dinner unlock)
    /// - Coordinating with SessionManager for state
    /// </summary>
    public class GallerySceneController : BaseSceneController
    {
        [Header("Dish Data")]
        [SerializeField] private List<DishData> availableDishes = new List<DishData>();

        [Header("Gallery Elements")]
        [SerializeField] private GameObject[] dishDisplayStations;
        [SerializeField] private GameObject reunionDinnerPortal;
        [SerializeField] private GameObject completionIndicatorUI;

        [Header("Audio")]
        [SerializeField] private AudioSource ambientMusic;
        [SerializeField] private AudioClip galleryMusicClip;

        // Gallery state
        private Dictionary<string, bool> dishCompletionStatus = new Dictionary<string, bool>();
        private bool reunionDinnerVisible = false;

        #region Scene Lifecycle (BaseSceneController Implementation)

        protected override void OnSceneEnter()
        {
            base.OnSceneEnter();

            // Set scene info
            sceneName = SceneTransitionManager.SCENE_MAIN_GALLERY;
            expectedState = GameState.InGallery;

            // Initialize gallery
            InitializeGallery();

            // Start ambient music
            PlayAmbientMusic();

            Debug.Log("[GalleryScene] Gallery initialized");
        }

        protected override void SubscribeToEvents()
        {
            // Subscribe to session events
            SessionManager.Instance.OnStateChanged += HandleStateChanged;
            SessionManager.Instance.OnDishCompleted += HandleDishCompleted;
            SessionManager.Instance.OnReunionDinnerUnlocked += HandleReunionDinnerUnlocked;
        }

        protected override void UnsubscribeFromEvents()
        {
            // CRITICAL: Unsubscribe from ALL events
            SessionManager.Instance.OnStateChanged -= HandleStateChanged;
            SessionManager.Instance.OnDishCompleted -= HandleDishCompleted;
            SessionManager.Instance.OnReunionDinnerUnlocked -= HandleReunionDinnerUnlocked;
        }

        protected override void LoadSceneState()
        {
            // Load completion status from SessionManager
            SessionData session = GetSessionData();

            // Update dish completion visuals
            foreach (var completion in session.dishesCompleted)
            {
                dishCompletionStatus[completion.dishID] = true;
                UpdateDishVisual(completion.dishID, true);
            }

            // Check if reunion dinner should be visible
            if (session.reunionDinnerUnlocked)
            {
                ShowReunionDinnerPortal();
            }

            // Update progress UI
            UpdateProgressUI();

            Debug.Log($"[GalleryScene] Loaded state: {session.dishesCompleted.Count}/5 dishes completed");
        }

        protected override void SaveSceneState()
        {
            // Gallery doesn't need to save state
            // (all state is already in SessionManager)
        }

        protected override void OnSceneExit()
        {
            // Stop music
            if (ambientMusic != null && ambientMusic.isPlaying)
                ambientMusic.Stop();

            Debug.Log("[GalleryScene] Gallery exited");
            base.OnSceneExit();
        }

        #endregion

        #region Gallery Initialization

        /// <summary>
        /// Initialize gallery displays and UI
        /// </summary>
        private void InitializeGallery()
        {
            // Initialize dish display stations
            if (dishDisplayStations != null && dishDisplayStations.Length > 0)
            {
                for (int i = 0; i < dishDisplayStations.Length && i < availableDishes.Count; i++)
                {
                    SetupDishDisplay(dishDisplayStations[i], availableDishes[i]);
                }
            }

            // Hide reunion dinner portal initially
            if (reunionDinnerPortal != null)
                reunionDinnerPortal.SetActive(false);

            // Initialize completion status dictionary
            dishCompletionStatus.Clear();
            foreach (var dish in availableDishes)
            {
                dishCompletionStatus[dish.dishID] = false;
            }
        }

        /// <summary>
        /// Setup a single dish display station
        /// </summary>
        private void SetupDishDisplay(GameObject station, DishData dish)
        {
            if (station == null || dish == null)
                return;

            // TODO: Set dish model, placard text, etc.
            // Example:
            // station.GetComponent<DishDisplay>().SetDishData(dish);

            Debug.Log($"[GalleryScene] Setup display for: {dish.englishName}");
        }

        #endregion

        #region Dish Selection

        /// <summary>
        /// Called when user selects a dish to learn
        /// This would be called by the DishDisplay component
        /// </summary>
        public void OnDishSelected(string dishID)
        {
            // Find the dish data
            DishData selectedDish = availableDishes.Find(d => d.dishID == dishID);

            if (selectedDish == null)
            {
                Debug.LogError($"[GalleryScene] Dish not found: {dishID}");
                return;
            }

            Debug.Log($"[GalleryScene] User selected dish: {selectedDish.englishName}");

            // Transition to cooking tutorial
            SceneTransitionManager.Instance.LoadCookingTutorial(dishID);
        }

        /// <summary>
        /// Called when user enters reunion dinner portal
        /// </summary>
        public void OnReunionDinnerSelected()
        {
            if (!SessionManager.Instance.IsReunionDinnerUnlocked())
            {
                Debug.LogWarning("[GalleryScene] Reunion Dinner not yet unlocked!");
                // TODO: Show "Complete more dishes" message
                return;
            }

            Debug.Log("[GalleryScene] Entering Reunion Dinner scene");
            SceneTransitionManager.Instance.LoadReunionDinner();
        }

        #endregion

        #region Visual Updates

        /// <summary>
        /// Update visual state of a dish (completed/not completed)
        /// </summary>
        private void UpdateDishVisual(string dishID, bool isCompleted)
        {
            // TODO: Update visual state
            // Example: Add a checkmark, change lighting, etc.

            Debug.Log($"[GalleryScene] Updated visual for {dishID}: {(isCompleted ? "✓" : "○")}");
        }

        /// <summary>
        /// Show reunion dinner portal with animation
        /// </summary>
        private void ShowReunionDinnerPortal()
        {
            if (reunionDinnerVisible)
                return;

            reunionDinnerVisible = true;

            if (reunionDinnerPortal != null)
            {
                reunionDinnerPortal.SetActive(true);
                // TODO: Play unlock animation/effects
                Debug.Log("[GalleryScene] Reunion Dinner portal revealed!");
            }
        }

        /// <summary>
        /// Update progress UI (X/5 dishes completed)
        /// </summary>
        private void UpdateProgressUI()
        {
            SessionData session = GetSessionData();
            int completed = session.dishesCompleted.Count;
            int total = 5;

            // TODO: Update UI text
            // completionText.text = $"{completed}/{total} Dishes Mastered";

            Debug.Log($"[GalleryScene] Progress: {completed}/{total}");
        }

        #endregion

        #region Audio

        /// <summary>
        /// Play ambient gallery music
        /// </summary>
        private void PlayAmbientMusic()
        {
            if (ambientMusic != null && galleryMusicClip != null)
            {
                ambientMusic.clip = galleryMusicClip;
                ambientMusic.loop = true;
                ambientMusic.Play();
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handle state changes
        /// </summary>
        private void HandleStateChanged(GameState oldState, GameState newState)
        {
            Debug.Log($"[GalleryScene] State changed: {oldState} → {newState}");

            // If returning from cooking tutorial, refresh UI
            if (oldState == GameState.InCookingTutorial && newState == GameState.InGallery)
            {
                Debug.Log("[GalleryScene] Returned from cooking tutorial - refreshing");
                LoadSceneState(); // Reload to show new completion status
            }
        }

        /// <summary>
        /// Handle dish completion (fired by SessionManager)
        /// </summary>
        private void HandleDishCompleted(string dishID)
        {
            Debug.Log($"[GalleryScene] Dish completed event received: {dishID}");

            // Update local state
            dishCompletionStatus[dishID] = true;

            // Update visual
            UpdateDishVisual(dishID, true);

            // Update progress UI
            UpdateProgressUI();

            // TODO: Play celebration effects (particles, sound)
        }

        /// <summary>
        /// Handle reunion dinner unlock (fired by SessionManager)
        /// </summary>
        private void HandleReunionDinnerUnlocked()
        {
            Debug.Log("[GalleryScene] Reunion Dinner unlocked event received!");

            // Show the portal
            ShowReunionDinnerPortal();

            // TODO: Play special unlock effects, announcement, etc.
        }

        #endregion

        #region Debug Helpers

        [ContextMenu("Simulate Dish Completion")]
        private void SimulateDishCompletion()
        {
            if (availableDishes.Count > 0)
            {
                string testDishID = availableDishes[0].dishID;
                SessionManager.Instance.CompleteDish(testDishID, 6, 0);
            }
        }

        [ContextMenu("Force Unlock Reunion Dinner")]
        private void ForceUnlockReunionDinner()
        {
            // Complete 2 dishes to unlock
            for (int i = 0; i < 2 && i < availableDishes.Count; i++)
            {
                SessionManager.Instance.CompleteDish(availableDishes[i].dishID, 6, 0);
            }
        }

        [ContextMenu("Print Gallery State")]
        private void PrintGalleryState()
        {
            Debug.Log("=== GALLERY STATE ===");
            foreach (var kvp in dishCompletionStatus)
            {
                Debug.Log($"{kvp.Key}: {(kvp.Value ? "✓" : "○")}");
            }
            Debug.Log($"Reunion Dinner Visible: {reunionDinnerVisible}");
        }

        #endregion
    }
}
