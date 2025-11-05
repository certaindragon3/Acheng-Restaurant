using UnityEngine;
using System;
using AchengRestaurant.Data;

namespace AchengRestaurant.Core
{
    /// <summary>
    /// SINGLETON: Global session and state manager
    /// Persists across all scene changes using DontDestroyOnLoad
    ///
    /// CRITICAL STATE MANAGEMENT RULES:
    /// 1. This is the ONLY source of truth for global game state
    /// 2. All state transitions MUST go through TransitionToState()
    /// 3. Subscribe to OnStateChanged event to react to state changes
    /// 4. Never modify _currentState directly - always use TransitionToState()
    /// </summary>
    public class SessionManager : MonoBehaviour
    {
        #region Singleton Pattern
        private static SessionManager _instance;
        public static SessionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SessionManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("[SessionManager]");
                        _instance = go.AddComponent<SessionManager>();
                        DontDestroyOnLoad(go);
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region State Management
        private GameState _currentState = GameState.Initializing;
        public GameState CurrentState => _currentState;

        /// <summary>
        /// Event fired when game state changes
        /// Subscribe: SessionManager.Instance.OnStateChanged += YourHandler;
        /// Unsubscribe: SessionManager.Instance.OnStateChanged -= YourHandler;
        /// </summary>
        public event Action<GameState, GameState> OnStateChanged;

        /// <summary>
        /// Transition to a new game state
        /// Fires OnStateChanged event to notify all subscribers
        /// </summary>
        public void TransitionToState(GameState newState)
        {
            if (_currentState == newState)
            {
                Debug.LogWarning($"[SessionManager] Already in state: {newState}");
                return;
            }

            GameState oldState = _currentState;
            _currentState = newState;

            Debug.Log($"[SessionManager] State: {oldState} → {newState}");
            OnStateChanged?.Invoke(oldState, newState);
        }
        #endregion

        #region Session Data
        private SessionData _sessionData;
        public SessionData CurrentSession => _sessionData;

        /// <summary>
        /// Event fired when a dish is completed
        /// </summary>
        public event Action<string> OnDishCompleted;

        /// <summary>
        /// Event fired when reunion dinner is unlocked
        /// </summary>
        public event Action OnReunionDinnerUnlocked;

        /// <summary>
        /// Mark a dish as completed
        /// Returns true if this is a NEW completion, false if already completed
        /// </summary>
        public bool CompleteDish(string dishID, int stepsCompleted, int stepsSkipped)
        {
            if (_sessionData.IsDishCompleted(dishID))
            {
                Debug.LogWarning($"[SessionManager] Dish {dishID} already completed");
                return false;
            }

            DishCompletionData completion = new DishCompletionData(dishID, stepsCompleted, stepsSkipped);
            _sessionData.dishesCompleted.Add(completion);

            Debug.Log($"[SessionManager] Dish completed: {dishID} ({stepsCompleted} steps, {stepsSkipped} skipped)");
            OnDishCompleted?.Invoke(dishID);

            // Check if reunion dinner should be unlocked
            if (!_sessionData.reunionDinnerUnlocked && _sessionData.ShouldUnlockReunionDinner())
            {
                _sessionData.reunionDinnerUnlocked = true;
                Debug.Log("[SessionManager] Reunion Dinner UNLOCKED!");
                OnReunionDinnerUnlocked?.Invoke();
            }

            return true;
        }

        /// <summary>
        /// Mark tutorial as completed
        /// </summary>
        public void CompleteTutorial()
        {
            if (_sessionData.tutorialCompleted)
            {
                Debug.LogWarning("[SessionManager] Tutorial already completed");
                return;
            }

            _sessionData.tutorialCompleted = true;
            Debug.Log("[SessionManager] Tutorial completed");
        }

        /// <summary>
        /// Check if reunion dinner is unlocked
        /// </summary>
        public bool IsReunionDinnerUnlocked()
        {
            return _sessionData.reunionDinnerUnlocked;
        }

        /// <summary>
        /// Get number of completed dishes
        /// </summary>
        public int GetCompletedDishCount()
        {
            return _sessionData.dishesCompleted.Count;
        }
        #endregion

        #region Unity Lifecycle
        void Awake()
        {
            // Ensure singleton
            if (_instance != null && _instance != this)
            {
                Debug.LogWarning("[SessionManager] Duplicate instance destroyed");
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize new session
            _sessionData = new SessionData();
            Debug.Log($"[SessionManager] New session created: {_sessionData.sessionID}");
        }

        void OnDestroy()
        {
            if (_instance == this)
            {
                // Log session stats before destruction
                TimeSpan duration = DateTime.Now - _sessionData.startTime;
                Debug.Log($"[SessionManager] Session ended. Duration: {duration.TotalMinutes:F1} min, Dishes: {_sessionData.dishesCompleted.Count}");
            }
        }
        #endregion

        #region Debug Helpers
        /// <summary>
        /// Reset session data (useful for testing)
        /// WARNING: Use only for debugging!
        /// </summary>
        [ContextMenu("Reset Session Data")]
        public void ResetSessionData()
        {
            _sessionData = new SessionData();
            Debug.LogWarning("[SessionManager] Session data RESET");
        }

        /// <summary>
        /// Print current session info to console
        /// </summary>
        [ContextMenu("Print Session Info")]
        public void PrintSessionInfo()
        {
            Debug.Log($"=== SESSION INFO ===\n" +
                      $"State: {_currentState}\n" +
                      $"Tutorial: {(_sessionData.tutorialCompleted ? "✓" : "✗")}\n" +
                      $"Dishes: {_sessionData.dishesCompleted.Count}/5\n" +
                      $"Reunion Unlocked: {(_sessionData.reunionDinnerUnlocked ? "✓" : "✗")}");
        }
        #endregion
    }
}
