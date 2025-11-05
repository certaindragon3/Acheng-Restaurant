using UnityEngine;
using System;
using System.Collections.Generic;

namespace AchengRestaurant.Data
{
    /// <summary>
    /// ScriptableObject that stores all data for a single dish
    /// Create instances via: Assets → Create → Acheng Restaurant → Dish Data
    ///
    /// This allows designers to create and edit dish data without touching code
    /// Data persists across scenes and is loaded from Assets/Data/Dishes/
    /// </summary>
    [CreateAssetMenu(fileName = "NewDish", menuName = "Acheng Restaurant/Dish Data", order = 1)]
    public class DishData : ScriptableObject
    {
        [Header("Dish Identity")]
        [Tooltip("Unique ID for this dish (e.g., 'squirrel_fish')")]
        public string dishID;

        [Header("Dish Names")]
        public string englishName;
        public string chineseName;  // 汉字
        public string pinyinName;   // Romanization

        [Header("Classification")]
        public DishCategory category = DishCategory.Main;
        public DishDifficulty difficulty = DishDifficulty.Intermediate;

        [Header("Descriptions")]
        [TextArea(2, 4)]
        [Tooltip("Brief description for gallery placard (~50 words)")]
        public string briefDescription;

        [TextArea(4, 8)]
        [Tooltip("Detailed description for intro narration (~200 words)")]
        public string detailedDescription;

        [Header("Cultural Context")]
        [TextArea(3, 6)]
        public string historicalOrigin;

        [TextArea(3, 6)]
        public string culturalSymbolism;

        [TextArea(3, 6)]
        public string regionalSignificance;

        [TextArea(3, 6)]
        public string craftsmanshipNotes;

        [Header("Cooking Tutorial")]
        [Tooltip("Estimated duration in seconds")]
        public int estimatedDuration = 300; // 5 minutes default

        [Tooltip("Cooking steps for this dish")]
        public List<CookingStep> cookingSteps = new List<CookingStep>();

        [TextArea(2, 3)]
        [Tooltip("Chef's final words after cooking is complete")]
        public string completionMessage;

        [Header("Assets")]
        [Tooltip("3D model of the finished dish")]
        public GameObject dishModel;

        [Tooltip("Reference image for plating")]
        public Texture2D platingReference;

        #region Validation

        /// <summary>
        /// Validate dish data - called in editor
        /// </summary>
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(dishID))
            {
                Debug.LogWarning($"[DishData] {name}: dishID is not set!");
            }

            if (cookingSteps.Count == 0)
            {
                Debug.LogWarning($"[DishData] {name}: No cooking steps defined!");
            }

            // Validate step numbers are sequential
            for (int i = 0; i < cookingSteps.Count; i++)
            {
                if (cookingSteps[i].stepNumber != i + 1)
                {
                    Debug.LogWarning($"[DishData] {name}: Step {i} has wrong step number (expected {i + 1}, got {cookingSteps[i].stepNumber})");
                }
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Get total number of cooking steps
        /// </summary>
        public int GetStepCount()
        {
            return cookingSteps.Count;
        }

        /// <summary>
        /// Get a specific cooking step by index
        /// </summary>
        public CookingStep GetStep(int index)
        {
            if (index < 0 || index >= cookingSteps.Count)
            {
                Debug.LogError($"[DishData] Invalid step index: {index}");
                return null;
            }
            return cookingSteps[index];
        }

        #endregion
    }

    #region Supporting Classes

    /// <summary>
    /// A single step in the cooking tutorial
    /// </summary>
    [Serializable]
    public class CookingStep
    {
        [Tooltip("Step number (1-indexed)")]
        public int stepNumber;

        [Tooltip("Step title (e.g., 'Prepare Ingredients')")]
        public string title;

        [Header("Narration")]
        [Tooltip("Audio clip of chef narration")]
        public AudioClip narrationAudio;

        [TextArea(2, 4)]
        [Tooltip("English subtitles for narration")]
        public string subtitles;

        [Tooltip("Duration of narration in seconds")]
        public float narrationDuration;

        [Header("Interaction")]
        [Tooltip("Type of gesture/action required")]
        public GestureType requiredGesture = GestureType.None;

        [Tooltip("Success threshold (0-1, where 0.7 = 70% accuracy required)")]
        [Range(0f, 1f)]
        public float successThreshold = 0.7f;

        [Header("Visual Guide")]
        [Tooltip("Animation or video reference for this step")]
        public AnimationClip visualGuideAnimation;
    }

    /// <summary>
    /// Dish category types
    /// </summary>
    public enum DishCategory
    {
        Appetizer,
        Main,
        Soup,
        Dessert,
        Side
    }

    /// <summary>
    /// Difficulty levels
    /// </summary>
    public enum DishDifficulty
    {
        Beginner,
        Intermediate,
        Advanced
    }

    /// <summary>
    /// Types of cooking gestures/interactions
    /// </summary>
    public enum GestureType
    {
        None,           // No interaction required (just watch)
        Chopping,       // Downward motion
        Stirring,       // Circular motion in wok
        Pouring,        // Tilt to pour
        Flipping,       // Quick upward flick
        Seasoning,      // Pinch or shake gesture
        Plating,        // Grab and place
        HeatAdjust,     // Dial turn
        Washing         // Side-to-side motion
    }

    #endregion
}
