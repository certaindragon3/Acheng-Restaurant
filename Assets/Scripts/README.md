# Acheng Restaurant VR - Code Architecture

This document provides a quick reference for the core architecture.

## 📁 Folder Structure

```
Scripts/
├── Core/                   # Core systems (singletons, managers)
│   ├── SessionManager.cs          # Global state & session data
│   ├── SceneTransitionManager.cs  # Safe scene loading
│   └── BaseSceneController.cs     # Base class for all scene controllers
│
├── Data/                   # Data structures & ScriptableObjects
│   ├── GameState.cs               # Enums and session data classes
│   └── DishData.cs                # ScriptableObject for dish data
│
├── Scenes/                 # Scene-specific controllers
│   └── (TutorialSceneController.cs, GallerySceneController.cs, etc.)
│
├── Interaction/            # VR interaction systems
│   └── (GestureRecognizer.cs, Interactable.cs, etc.)
│
├── Tutorial/               # Tutorial step systems
│   └── (TutorialStep.cs, StepSequencer.cs, etc.)
│
└── UI/                     # UI controllers
    └── (SubtitleController.cs, FeedbackEffects.cs, etc.)
```

## 🏗️ Core Architecture

### 1. SessionManager (Singleton)
**Location:** `Scripts/Core/SessionManager.cs`

**Purpose:** Global state management and session data persistence

**Key Features:**
- Singleton pattern with DontDestroyOnLoad
- Manages GameState transitions
- Tracks user progress (completed dishes, tutorial status)
- Fires events for state changes

**Usage:**
```csharp
// Get current state
GameState state = SessionManager.Instance.CurrentState;

// Transition to new state
SessionManager.Instance.TransitionToState(GameState.InGallery);

// Subscribe to state changes
SessionManager.Instance.OnStateChanged += HandleStateChanged;

// Mark dish complete
SessionManager.Instance.CompleteDish("dish_id", stepsCompleted, stepsSkipped);
```

### 2. SceneTransitionManager (Singleton)
**Location:** `Scripts/Core/SceneTransitionManager.cs`

**Purpose:** Safe asynchronous scene loading with state coordination

**Key Features:**
- Prevents state conflicts during scene transitions
- Asynchronous loading (no freezing)
- Coordinates with SessionManager for state updates

**Usage:**
```csharp
// Load specific scenes
SceneTransitionManager.Instance.LoadTutorial();
SceneTransitionManager.Instance.LoadMainGallery();
SceneTransitionManager.Instance.LoadCookingTutorial("dish_id");
SceneTransitionManager.Instance.LoadReunionDinner();

// Check if transitioning
bool isLoading = SceneTransitionManager.Instance.IsTransitioning;
```

### 3. BaseSceneController (Abstract Base Class)
**Location:** `Scripts/Core/BaseSceneController.cs`

**Purpose:** Template for all scene controllers - ensures proper lifecycle management

**Key Features:**
- OnSceneEnter() - Initialize scene, subscribe to events
- OnSceneExit() - Clean up, unsubscribe from events
- Prevents state pollution between scenes

**Usage:**
```csharp
using AchengRestaurant.Core;
using AchengRestaurant.Data;

public class TutorialSceneController : BaseSceneController
{
    protected override void OnSceneEnter()
    {
        base.OnSceneEnter();
        // Initialize tutorial UI
    }

    protected override void SubscribeToEvents()
    {
        SessionManager.Instance.OnStateChanged += HandleStateChanged;
    }

    protected override void UnsubscribeFromEvents()
    {
        SessionManager.Instance.OnStateChanged -= HandleStateChanged;
    }

    protected override void OnSceneExit()
    {
        // Clean up tutorial state
        base.OnSceneExit();
    }
}
```

### 4. DishData (ScriptableObject)
**Location:** `Scripts/Data/DishData.cs`

**Purpose:** Data container for dish information and cooking steps

**How to Create:**
1. Right-click in Project → Create → Acheng Restaurant → Dish Data
2. Fill in dish details in Inspector
3. Save to `Assets/Data/Dishes/`

**Fields:**
- Dish identity (ID, names in English/Chinese/Pinyin)
- Descriptions (brief, detailed, cultural context)
- Cooking steps (list of CookingStep objects)
- Asset references (3D model, textures, audio)

## 🔑 Critical State Management Rules

**From your mentor's guidance:**

1. **Be VERY careful of STATE handling** - No conflicts between scenes
2. **Maintain function reusability** across all scenes

### Rules to Follow:

✅ **DO:**
- Always use SessionManager for global state
- Always use SceneTransitionManager for scene loading
- Inherit from BaseSceneController for every scene
- Subscribe to events in OnSceneEnter()
- Unsubscribe from ALL events in OnSceneExit()
- Use ScriptableObjects (DishData) for content data

❌ **DON'T:**
- Never use SceneManager.LoadScene() directly
- Never use static variables for scene-specific data
- Never forget to unsubscribe from events
- Never modify SessionManager state directly (use public methods)
- Never assume initial state values in scenes

## 📊 Game State Flow

```
Initializing
    ↓
InTutorial → (complete) → InGallery
                              ↓
                    (select dish) → InCookingTutorial
                              ↑              ↓
                              └── (return) ──┘
                              ↓
                    (2-3 dishes done) → InReunionDinner
```

## 🎯 Next Steps

1. Create scene-specific controllers that inherit from BaseSceneController
2. Create VR interaction systems (XRMovement, XRHandController)
3. Build gesture recognition system for cooking tutorials
4. Create UI systems (subtitles, feedback effects)

## 📝 Development Checklist

When creating a new scene:
- [ ] Create a SceneController that inherits from BaseSceneController
- [ ] Set sceneName and expectedState in Inspector
- [ ] Implement OnSceneEnter() and OnSceneExit()
- [ ] Subscribe/Unsubscribe to SessionManager events properly
- [ ] Test scene transitions in both directions
- [ ] Verify no state pollution (enter scene multiple times)

---

**Last Updated:** 2025-11-05
**Author:** Jiesen Huang
**Reference:** See CLAUDE.md for full project documentation
