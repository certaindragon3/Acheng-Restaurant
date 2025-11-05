using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using AchengRestaurant.Data;

namespace AchengRestaurant.Core
{
    /// <summary>
    /// SINGLETON: Handles all scene transitions safely
    /// Prevents state conflicts by coordinating with SessionManager
    ///
    /// CRITICAL RULES:
    /// 1. NEVER use SceneManager.LoadScene() directly - always use this manager
    /// 2. Transitions are asynchronous to prevent freezing
    /// 3. Game state is set to Transitioning during load
    /// 4. Scenes MUST have a BaseSceneController to receive OnSceneLoaded callback
    /// </summary>
    public class SceneTransitionManager : MonoBehaviour
    {
        #region Singleton Pattern
        private static SceneTransitionManager _instance;
        public static SceneTransitionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SceneTransitionManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("[SceneTransitionManager]");
                        _instance = go.AddComponent<SceneTransitionManager>();
                        DontDestroyOnLoad(go);
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region Scene Names (Constants)
        public const string SCENE_TUTORIAL = "Tutorial";
        public const string SCENE_MAIN_GALLERY = "MainGallery";
        public const string SCENE_COOKING_TUTORIAL = "CookingTutorial";
        public const string SCENE_REUNION_DINNER = "ReunionDinner";
        #endregion

        #region State
        private bool _isTransitioning = false;
        public bool IsTransitioning => _isTransitioning;

        private string _currentSceneName;
        public string CurrentSceneName => _currentSceneName;
        #endregion

        #region Public Scene Transition Methods

        /// <summary>
        /// Load Tutorial scene
        /// </summary>
        public void LoadTutorial()
        {
            LoadSceneAsync(SCENE_TUTORIAL, GameState.InTutorial);
        }

        /// <summary>
        /// Load Main Gallery scene
        /// </summary>
        public void LoadMainGallery()
        {
            LoadSceneAsync(SCENE_MAIN_GALLERY, GameState.InGallery);
        }

        /// <summary>
        /// Load Cooking Tutorial scene for a specific dish
        /// </summary>
        /// <param name="dishID">ID of the dish to cook</param>
        public void LoadCookingTutorial(string dishID)
        {
            // TODO: Pass dishID to cooking scene (via SessionManager or PlayerPrefs)
            Debug.Log($"[SceneTransition] Loading cooking tutorial for dish: {dishID}");
            LoadSceneAsync(SCENE_COOKING_TUTORIAL, GameState.InCookingTutorial);
        }

        /// <summary>
        /// Load Reunion Dinner scene
        /// Only allowed if unlocked in SessionManager
        /// </summary>
        public void LoadReunionDinner()
        {
            if (!SessionManager.Instance.IsReunionDinnerUnlocked())
            {
                Debug.LogWarning("[SceneTransition] Reunion Dinner not yet unlocked!");
                return;
            }

            LoadSceneAsync(SCENE_REUNION_DINNER, GameState.InReunionDinner);
        }

        #endregion

        #region Core Scene Loading Logic

        /// <summary>
        /// Asynchronously load a scene with state management
        /// </summary>
        private void LoadSceneAsync(string sceneName, GameState targetState)
        {
            if (_isTransitioning)
            {
                Debug.LogWarning($"[SceneTransition] Already transitioning, ignoring request for {sceneName}");
                return;
            }

            StartCoroutine(LoadSceneCoroutine(sceneName, targetState));
        }

        /// <summary>
        /// Coroutine that handles the actual scene loading
        /// </summary>
        private IEnumerator LoadSceneCoroutine(string sceneName, GameState targetState)
        {
            _isTransitioning = true;

            // Set state to Transitioning
            SessionManager.Instance.TransitionToState(GameState.Transitioning);

            Debug.Log($"[SceneTransition] Loading scene: {sceneName}");

            // TODO: Show loading screen here (optional)
            // yield return ShowLoadingScreen();

            // Begin async scene load
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            // Wait for scene to fully load
            while (!asyncLoad.isDone)
            {
                // TODO: Update loading progress bar (optional)
                // float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
                yield return null;
            }

            // Scene loaded successfully
            _currentSceneName = sceneName;
            Debug.Log($"[SceneTransition] Scene loaded: {sceneName}");

            // Small delay to ensure scene is fully initialized
            yield return new WaitForSeconds(0.1f);

            // Set target state
            SessionManager.Instance.TransitionToState(targetState);

            // TODO: Hide loading screen here (optional)
            // yield return HideLoadingScreen();

            _isTransitioning = false;
            Debug.Log($"[SceneTransition] Transition complete: {sceneName} ({targetState})");
        }

        #endregion

        #region Unity Lifecycle

        void Awake()
        {
            // Ensure singleton
            if (_instance != null && _instance != this)
            {
                Debug.LogWarning("[SceneTransitionManager] Duplicate instance destroyed");
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);

            // Get current scene name
            _currentSceneName = SceneManager.GetActiveScene().name;
            Debug.Log($"[SceneTransitionManager] Initialized in scene: {_currentSceneName}");
        }

        #endregion

        #region Debug Helpers

        [ContextMenu("Print Current Scene")]
        public void PrintCurrentScene()
        {
            Debug.Log($"Current Scene: {_currentSceneName}, Transitioning: {_isTransitioning}");
        }

        #endregion
    }
}
