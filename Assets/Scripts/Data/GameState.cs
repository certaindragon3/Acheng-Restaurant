using System;

namespace AchengRestaurant.Data
{
    /// <summary>
    /// Global game state enum - represents which scene/phase the user is in
    /// CRITICAL: Be very careful when transitioning states to avoid conflicts
    /// </summary>
    public enum GameState
    {
        Initializing,      // Game is loading
        InTutorial,        // User is in VR tutorial scene
        InGallery,         // User is in main restaurant gallery
        InCookingTutorial, // User is learning to cook a dish
        InReunionDinner,   // User is in final reunion dinner scene
        Transitioning      // Scene is being loaded
    }

    /// <summary>
    /// Represents a completed dish tutorial session
    /// </summary>
    [Serializable]
    public class DishCompletionData
    {
        public string dishID;
        public DateTime completionTime;
        public int stepsCompleted;
        public int stepsSkipped;

        public DishCompletionData(string id, int completed, int skipped)
        {
            dishID = id;
            completionTime = DateTime.Now;
            stepsCompleted = completed;
            stepsSkipped = skipped;
        }
    }

    /// <summary>
    /// User progress data for the entire session
    /// Persists across scene changes via SessionManager
    /// </summary>
    [Serializable]
    public class SessionData
    {
        public string sessionID;
        public DateTime startTime;
        public bool tutorialCompleted;
        public System.Collections.Generic.List<DishCompletionData> dishesCompleted;
        public bool reunionDinnerUnlocked;

        public SessionData()
        {
            sessionID = System.Guid.NewGuid().ToString();
            startTime = DateTime.Now;
            tutorialCompleted = false;
            dishesCompleted = new System.Collections.Generic.List<DishCompletionData>();
            reunionDinnerUnlocked = false;
        }

        /// <summary>
        /// Check if reunion dinner should be unlocked (2-3 dishes completed)
        /// </summary>
        public bool ShouldUnlockReunionDinner()
        {
            return dishesCompleted.Count >= 2;
        }

        /// <summary>
        /// Check if a specific dish has been completed
        /// </summary>
        public bool IsDishCompleted(string dishID)
        {
            return dishesCompleted.Exists(d => d.dishID == dishID);
        }
    }
}
