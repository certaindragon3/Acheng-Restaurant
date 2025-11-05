# Acheng Restaurant VR - Implementation Guide

Quick reference for implementing the core architecture in Unity.

---

## 🚀 Getting Started (Unity Editor)

### Step 1: Initial Setup

1. **Open Unity Project**
   - Open the project in Unity 6000.0.28f1 or later
   - Wait for packages to download (XR Interaction Toolkit 3.3.0 should install automatically)

2. **Verify Package Installation**
   - Window → Package Manager
   - Confirm these packages are installed:
     - XR Interaction Toolkit (3.3.0)
     - OpenXR Plugin (1.15.1)
     - Input System (1.14.2)

3. **Configure XR Settings**
   - Edit → Project Settings → XR Plug-in Management
   - Enable OpenXR for Windows/Mac
   - OpenXR Feature Groups: Enable "Interaction Profiles" and "Hand Tracking"

---

## 📋 Scene Setup Checklist

### Creating Your First Scene (Tutorial)

#### 1. Create New Scene
```
File → New Scene → Basic (Built-in)
Save as: Assets/Scenes/Tutorial.unity
```

#### 2. Add XR Origin
```
Right-click in Hierarchy → XR → XR Origin (Action-based)
```

This creates:
- XR Origin (parent object)
  - Camera Offset
    - Main Camera
  - Left Controller
  - Right Controller

#### 3. Add SessionManager
```
Hierarchy → Right-click → Create Empty
Name: [SessionManager]
Add Component → SessionManager
```

**Important:** Mark as DontDestroyOnLoad (this happens automatically in code)

#### 4. Add SceneTransitionManager
```
Hierarchy → Right-click → Create Empty
Name: [SceneTransitionManager]
Add Component → SceneTransitionManager
```

#### 5. Add Scene Controller
```
Hierarchy → Right-click → Create Empty
Name: TutorialSceneController
Add Component → TutorialSceneController
```

In Inspector, set:
- Scene Name: `Tutorial`
- Expected State: `InTutorial`

#### 6. Configure XR Origin Movement
```
Select XR Origin
Add Component → Character Controller
Add Component → XRMovementController
```

In XRMovementController Inspector:
- Move Speed: `1.5`
- Enable Smooth Movement: ✓
- Enable Teleportation: ✓
- Snap Turn Angle: `30`

#### 7. Configure XR Controllers
```
Select: XR Origin → Left Controller
Add Component → XRHandController
```

In Inspector:
- Hand Type: `Left`
- Ray Max Distance: `10`

Repeat for Right Controller with `Hand Type: Right`

---

## 🎯 Creating a Dish Data Asset

### Step 1: Create Dish Data
```
Project Window → Assets/Data/Dishes/
Right-click → Create → Acheng Restaurant → Dish Data
Name: Squirrel_Fish.asset
```

### Step 2: Fill in Data
In Inspector:
```
Dish ID: squirrel_fish
English Name: Squirrel Fish
Chinese Name: 松鼠鳜鱼
Pinyin Name: Sōngshǔ Guìyú

Category: Main
Difficulty: Advanced

Brief Description: (50 words)
"A signature Subang dish featuring crispy fried fish..."

Detailed Description: (200 words)
"Squirrel Fish is one of the most celebrated dishes..."

Historical Origin: (100 words)
"Dating back to the Qing Dynasty..."

```

### Step 3: Add Cooking Steps
```
Cooking Steps → Size: 6

Step 1:
  - Step Number: 1
  - Title: Prepare the Fish
  - Subtitles: "First, we carefully score the fish..."
  - Required Gesture: Chopping
  - Success Threshold: 0.7
  - Narration Duration: 45

(Repeat for steps 2-6)
```

---

## 🔧 Testing Your Setup

### Test 1: Session Manager
```
1. Play Mode in Unity
2. Find [SessionManager] in Hierarchy
3. Right-click component → Print Session Info
4. Console should show: "State: Initializing, Tutorial: ✗, Dishes: 0/5"
```

### Test 2: State Transitions
```
1. Play Mode
2. Open Console
3. Find [SessionManager] → CompleteTutorial (context menu)
4. Console should log: "Tutorial completed"
5. Find [SceneTransitionManager] → LoadMainGallery
6. Should log: "Loading scene: MainGallery"
```

### Test 3: Dish Completion
```
1. Play Mode
2. Create a test dish in Data/Dishes/ (if not exists)
3. Find [SessionManager] → Right-click → Context Menu
4. Simulate dish completion
5. Console: "Dish completed: [dish_id]"
6. After 2 dishes: "Reunion Dinner UNLOCKED!"
```

---

## 🎨 Connecting UI to Controllers

### Example: Tutorial UI Setup

1. **Create Canvas**
```
Hierarchy → UI → Canvas
Canvas → Render Mode: World Space
Position: (0, 2, 3) // In front of player
Scale: (0.01, 0.01, 0.01)
```

2. **Add UI Text**
```
Canvas → Right-click → UI → TextMeshPro - Text
Name: TutorialStepText
```

3. **Connect to Controller**
```cs
// In TutorialSceneController.cs
[SerializeField] private TextMeshProUGUI tutorialStepText;

private void DisplayCurrentStep()
{
    if (tutorialStepText != null)
    {
        tutorialStepText.text = tutorialSteps[currentStepIndex];
    }
}
```

4. **Assign in Inspector**
```
Select TutorialSceneController
Drag TutorialStepText → Tutorial Step Text field
```

---

## 🏗️ Building Additional Scenes

### Gallery Scene Template

#### 1. Duplicate Tutorial Scene
```
Assets/Scenes/Tutorial.unity → Duplicate
Rename: MainGallery.unity
```

#### 2. Replace Scene Controller
```
Delete: TutorialSceneController object
Create: Empty → GallerySceneController
Add Component → GallerySceneController
```

#### 3. Set Scene Parameters
```
Scene Name: MainGallery
Expected State: InGallery
```

#### 4. Add Dish Display Stations
```
Create: Empty → DishDisplayStation_1
Position: (2, 0, 0) // Arrange in circle

Repeat for 5 stations total
```

#### 5. Assign to Controller
```
GallerySceneController Inspector:
Dish Display Stations → Size: 5
Drag all stations into array
```

---

## 📊 Common Issues & Solutions

### Issue: "SessionManager not found"
**Solution:**
```
Make sure [SessionManager] GameObject exists in FIRST scene
It will persist across all scenes automatically
```

### Issue: "XR Origin not moving"
**Solution:**
```
1. Check XR Origin has CharacterController component
2. Check XRMovementController is attached
3. Verify Input Actions are assigned in Inspector
4. Test in Play Mode with VR headset connected
```

### Issue: "Dishes not showing completion status"
**Solution:**
```
GallerySceneController needs to subscribe to events:
1. Check SubscribeToEvents() is called
2. Verify OnDishCompleted handler exists
3. Test: SessionManager → Simulate Dish Completion
```

### Issue: "Scene transition freezes"
**Solution:**
```
Never use SceneManager.LoadScene() directly!
Always use: SceneTransitionManager.Instance.LoadXXX()
```

---

## 🔍 Debugging Tools

### SessionManager Debug Menu
```
Right-click SessionManager component:
- Print Session Info
- Reset Session Data (clears progress)
```

### SceneTransitionManager Debug
```
Right-click component:
- Print Current Scene
```

### Scene Controller Debug
```
Right-click any scene controller:
- Print Scene Info
```

---

## 📝 Development Workflow

### Daily Development Loop

1. **Open Unity**
2. **Select Scene** (Tutorial/Gallery/etc.)
3. **Run in Play Mode** (or VR if connected)
4. **Test functionality**
5. **Check Console** for logs/errors
6. **Iterate**

### Before Committing Code

- [ ] All scenes load without errors
- [ ] SessionManager persists across scenes
- [ ] State transitions work correctly
- [ ] No memory leaks (check event subscriptions)
- [ ] Console shows no warnings

---

## 🎓 Next Steps

After completing this setup:

1. **Create Cooking Tutorial Scene**
   - Follow Gallery scene template
   - Add CookingTutorialController
   - Implement gesture recognition

2. **Build VR Interaction System**
   - Create grabbable objects
   - Implement cooking gestures
   - Add feedback effects

3. **Populate Content**
   - Create all 5 dish data assets
   - Record voice-over narration
   - Add 3D models

4. **Polish**
   - Add UI transitions
   - Implement loading screens
   - Add ambient audio

---

## 📚 Reference

- **Architecture:** See `/Assets/Scripts/README.md`
- **Full Vision:** See `/docs/project-vision.md`
- **Code Documentation:** See `CLAUDE.md`

---

**Last Updated:** 2025-11-05
**Author:** Jiesen Huang

