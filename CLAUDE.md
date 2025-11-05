# Acheng Restaurant VR Experience - CLAUDE.md
## Project Context & Development Guide

---

## 1. Current Project Status

### 1.1 What EXISTS Right Now

**Project Type:** Unity VR experience using OpenXR for PCVR  
**Unity Version:** 6000.0.61f1 (Unity LTS 6)  
**Platform:** PCVR only (Meta Quest 3 via Link/Air Link, HTC Vive Vision)  
**Current Stage:** Early foundation phase (post-initialization, pre-development)

**Untracked Files (as of latest git):**
- Empty CLAUDE.md file (needs content)

### 1.2 Assets Folder Structure (Current)

```
Assets/
├── Scenes/
│   └── SampleScene.unity           [Default Unity scene - placeholder]
│
├── TutorialInfo/
│   ├── Icons/
│   ├── Scripts/
│   │   ├── Readme.cs               [Basic ScriptableObject for readme display]
│   │   └── Editor/
│   │       └── ReadmeEditor.cs
│   └── Readme.asset
│
├── XR/
│   ├── Settings/
│   │   ├── OpenXR Editor Settings.asset
│   │   └── OpenXR Package Settings.asset
│   ├── Loaders/
│   │   └── OpenXRLoader.asset
│   └── XRGeneralSettingsPerBuildTarget.asset
│
└── Settings/
    └── [Various quality/rendering settings]

InputSystem_Actions.inputactions    [Input configuration for controllers]
```

### 1.3 Scripts Currently Present

**Only 2 C# scripts exist:**
1. `/Assets/TutorialInfo/Scripts/Readme.cs` - Lightweight ScriptableObject
2. `/Assets/TutorialInfo/Scripts/Editor/ReadmeEditor.cs` - Editor utility

**Status:** No game logic implemented yet. Project is infrastructure-only.

### 1.4 Scenes Currently Present

**Only 1 Unity scene:**
- `/Assets/Scenes/SampleScene.unity` - Default Unity sample scene (placeholder)

**Status:** No actual project scenes created yet.

### 1.5 Documentation Files

```
docs/
├── project-vision.md          [Comprehensive 800+ line vision document]
├── 1029refine.md              [Oct 29 refinement notes in Chinese]
├── thought-tracker.md         [Initial project concept notes]
└── test.md                    [Empty/placeholder]

CLAUDE.md                       [This file - needs to be created]
```

### 1.6 Dependencies

**Installed Packages:**
- `com.unity.xr.openxr` (1.15.1)
- `com.unity.xr.management` (4.5.3)
- `com.unity.inputsystem` (1.14.2)
- `com.unity.render-pipelines.universal` (17.0.4) - URP for PCVR
- `com.unity.timeline` (1.8.9)
- Standard Unity modules (audio, animation, physics, etc.)

**MISSING (as per vision document):**
- `com.unity.xr.interaction.toolkit` - NOT YET added
- XR Hands package (for hand tracking) - NOT YET added

### 1.7 Git Commit History

```
78d48bb - tst
0e4cc33 - unity lts version update
8b99193 - 1029会议成果 (Oct 29 meeting results)
e691f3d - file
99c4671 - openxr integration
bc35162 - init
ac8373c - initialization
```

**Interpretation:** Project was initialized, OpenXR was integrated, Unity LTS was updated, and Oct 29 meeting notes were committed. Very recent activity (latest test commit).

---

## 2. Project Vision & Requirements (From Documentation)

### 2.1 Core Experience Design

**Title:** Acheng Restaurant VR Experience  
**Academic Focus:** Culinary heritage preservation - 5 signature Suzhou (Subang) dishes  
**Target Audience:** Culinary students, food historians, cultural enthusiasts, general VR users  
**Total Duration:** 12-15 minutes  

**5 Scenes Planned:**
1. **Tutorial Scene** (2 min) - VR controls introduction
2. **Main Gallery** (1-2 min) - Photogrammetry-scanned restaurant with 5 dish displays
3. **Cooking Tutorial Scenes** (5 min each × 5 = 25 min total, but only 2-3 required) - Interactive cooking demonstrations
4. **Reunion Dinner Scene** (3-4 min) - Culminating experience with all dishes
5. **Transitions/Credits** - Navigation between scenes

### 2.2 Technical Requirements

**VR Controls:**
- **Locomotion:** Smooth continuous movement + teleportation option
- **Rotation:** Snap turning (30° increments)
- **Interaction:** Ray-cast selection + grip button for grasping
- **Cooking Gestures:** Chopping, stirring, pouring, flipping, seasoning, plating, heat adjustment, washing

**Performance Targets:**
- Frame Rate: 90 FPS (target), 72 FPS minimum
- Draw Calls: <300 per scene
- Polygon Budget: 1-2M triangles per active view
- Texture Memory: <4GB total (PC VRAM)
- Platform: PCVR only (no mobile optimization needed)

### 2.3 Content Requirements (Not Yet Created)

- [ ] 5 finished dish 3D models (photogrammetry scans)
- [ ] Restaurant interior photogrammetry (captured by Nov 26, 2025)
- [ ] 25-35 ingredient 3D models
- [ ] 15 cooking tool models
- [ ] Chef voice-over narration (20-25 min total)
- [ ] Background music (traditional Suzhou style, 3-4 tracks)
- [ ] Cooking SFX library
- [ ] Subtitle files (SRT format)

---

## 3. Development Timeline & Phases

### Phase 1: Foundation (Weeks 1-2)
**Status:** IN PROGRESS (partially done - OpenXR integrated)

**Remaining Tasks:**
- VR movement system (walk/teleport)
- Basic hand controller visualization
- Simple test scene with grabbable objects
- Scene transition system
- Data structure implementation (ScriptableObjects for dishes)

### Phase 2: Tutorial & Interaction (Weeks 3-4)
**Status:** NOT STARTED

**Tasks:**
- Tutorial scene with step-by-step VR training
- Cooking gesture recognition (6-8 core actions)
- Visual/audio feedback systems
- Skip functionality
- Progress tracking

### Phase 3: Main Gallery Scene (Week 5)
**Status:** NOT STARTED

### Phase 4: Cooking Tutorial Template (Weeks 6-7)
**Status:** NOT STARTED

### Phase 5: Content Population (Weeks 8-9)
**Status:** BLOCKED (waiting for fieldwork: Nov 26, 2025)

### Phase 6-8: Reunion Dinner, Polish, Academic Prep
**Status:** NOT STARTED

---

## 4. Recent Design Refinements (Oct 29 Notes)

From `1029refine.md` in Chinese:

**Key Decisions Made:**
1. ✓ Chef companion image needs design - maybe a "contextualized" virtual spirit character
2. ✓ Subtitles appear as speech bubbles above avatar
3. ✓ Motion capture clothing investigation (need <400K option)
4. ✓ Hand gesture capture research needed
5. **SKIP REUNION DINNER FEAST** - Avatar integration is too hard
6. **Alternative:** Handheld translation machine UI that shows subtitles when pointed at elements
7. **CRITICAL:** Be very careful with STATE management - no conflicts between scenes. Keep functions reusable.

**Current Architecture Concern:** State handling between scenes needs careful management to avoid conflicts.

---

## 5. Architecture Patterns & Systems Needed

### 5.1 What Needs to Be Built

Based on the project vision and current state:

**Core Systems:**
1. **XR Input Management** - Map controller inputs using Unity Input System
2. **Movement System** - Locomotion (smooth + teleport), rotation, comfort options
3. **Hand Interaction System** - Ray-cast selection, object grasping, gesture recognition
4. **Scene Management** - Safe transitions between 4+ scenes with state preservation
5. **Gesture Recognition** - Detect cooking motions (chopping, stirring, pouring, etc.)
6. **Feedback Systems** - Visual (particles, outlines), audio (voice, SFX), haptic (controller vibration)
7. **Progress Tracking** - Track dish completion, unlocks, session data
8. **Data Management** - ScriptableObjects for dish data (name, description, cultural context, steps, assets)

### 5.2 Recommended Project Structure

```
Assets/
├── Scenes/
│   ├── 00_Startup.unity              [Initialize XR, load main menu]
│   ├── 01_Tutorial.unity
│   ├── 02_MainGallery.unity
│   ├── 03_CookingTutorial_Template.unity
│   └── 04_ReunionDinner.unity
│
├── Scripts/
│   ├── Core/
│   │   ├── XRManager.cs              [Master XR setup & initialization]
│   │   ├── InputHandler.cs           [Centralized input management]
│   │   ├── XRMovement.cs             [Locomotion & rotation]
│   │   ├── XRHandController.cs       [Hand tracking & visualization]
│   │   └── SceneTransitionManager.cs [Safe scene loading with state preservation]
│   │
│   ├── Interaction/
│   │   ├── GestureRecognizer.cs      [Cooking motion detection]
│   │   ├── InteractableObject.cs     [Base class for interactable items]
│   │   └── DishSelector.cs           [Gallery dish selection logic]
│   │
│   ├── Tutorial/
│   │   ├── TutorialStep.cs           [Individual tutorial step]
│   │   ├── CookingStep.cs            [Cooking action within tutorial]
│   │   └── StepSequencer.cs          [Manages step progression & feedback]
│   │
│   ├── Data/
│   │   ├── DishData.cs               [ScriptableObject: dish definition]
│   │   ├── RecipeStep.cs             [Individual cooking step data]
│   │   ├── ProgressManager.cs        [Session progress tracking]
│   │   └── DataManager.cs            [Central data access]
│   │
│   ├── UI/
│   │   ├── SubtitleController.cs     [Display & sync subtitles]
│   │   ├── InfoPlacardDisplay.cs     [Gallery dish info display]
│   │   ├── FeedbackEffects.cs        [Visual/audio/haptic feedback]
│   │   └── InteractionHints.cs       [Context-sensitive help text]
│   │
│   └── Utilities/
│       ├── AudioManager.cs           [Centralized audio control]
│       ├── VignetteController.cs     [Comfort feature for movement]
│       └── PerformanceMonitor.cs     [FPS & memory tracking]
│
├── Data/
│   ├── Dishes/
│   │   ├── Dish_01.asset             [ScriptableObject instances]
│   │   ├── Dish_02.asset
│   │   └── ...
│   └── ProgressData.json             [Runtime session data]
│
├── Models/
│   ├── Environment/
│   │   └── RestaurantScan/           [Photogrammetry model - TBD]
│   ├── Dishes/
│   ├── Ingredients/
│   └── Tools/
│
├── Audio/
│   ├── Narration/
│   ├── Music/
│   ├── SFX/
│   └── Ambient/
│
├── Prefabs/
│   ├── XR_Origin.prefab              [XR player rig]
│   ├── DishDisplayStation.prefab
│   ├── CookingWorkspace.prefab
│   └── UI_Panels.prefab
│
├── Materials/
│   ├── DishMaterials/
│   ├── EnvironmentMaterials/
│   └── UIMaterials/
│
└── Settings/
    ├── XRSettings.asset              [Existing]
    ├── InputActions.inputactions     [Existing - may need expansion]
    └── AudioSettings.asset           [To be created]
```

### 5.3 State Management Strategy

**CRITICAL (per Oct 29 notes):** Need careful state handling to prevent conflicts between scenes.

**Recommendation:**
- Implement a singleton `GameManager` or `SessionManager` to track:
  - Current scene
  - Completed dishes
  - User preferences (locomotion mode, comfort settings, audio levels)
  - Current tutorial progress
- Use `DontDestroyOnLoad` sparingly - only for managers that must persist
- Each scene should be mostly self-contained except for reading from `SessionManager`
- Use events/delegates to communicate between systems instead of direct references

---

## 6. Input System Configuration

**Current State:** `InputSystem_Actions.inputactions` exists but may be incomplete

**Required Input Actions:**
- Locomotion: Left Thumbstick (move), Right Thumbstick (rotate)
- Selection: Trigger button (point & select)
- Interaction: Grip button (grab objects)
- UI: Various buttons for menus/skipping steps
- Comfort: Button to enable/disable vignette, switch locomotion modes

**Note:** May need to expand the existing InputActions file to cover all gestures.

---

## 7. Important Considerations

### 7.1 Performance Budget Constraints

- **PC-optimized assets** are acceptable (higher poly count than mobile)
- **URP is already chosen** for efficient rendering
- Must profile regularly to stay under <300 draw calls per scene
- Use LOD (Level of Detail) for hero assets like dish models
- Consider object pooling for cooking ingredients
- Lightmap baking for static lighting recommended

### 7.2 Accessibility Requirements

- Teleportation option alongside smooth movement
- Snap turning (not smooth) to reduce motion sickness
- Vignette effect option during movement
- High-contrast subtitle mode
- Adjustable audio levels (voice/music/SFX separately)
- Colorblind-friendly visual indicators

### 7.3 Cultural & Academic Integrity

- Work closely with Chef Shen Jiechen from Acheng Restaurant
- Ensure authentic representation of Suzhou culinary techniques
- Academic rigor in cultural context (research-backed content)
- Respect for traditional craftsmanship and labor intensity
- Accurate traditional music selection (Suzhou/Jiangnan style)

### 7.4 Content Pipeline Dependencies

**BLOCKING ITEM:** Fieldwork at Acheng Restaurant
- **Deadline:** November 26, 2025
- **Purpose:** Capture photogrammetry of restaurant interior & dishes, interview chef
- **If delayed:** Can proceed with placeholder assets, but final quality depends on this

---

## 8. Key Files & Their Roles

| File Path | Purpose | Status |
|-----------|---------|--------|
| `Assets/Scenes/SampleScene.unity` | Placeholder scene | TO REPLACE |
| `Assets/XR/Settings/OpenXR*.asset` | XR runtime configuration | EXISTING |
| `Assets/InputSystem_Actions.inputactions` | Input mapping | NEEDS REVIEW |
| `Packages/manifest.json` | Project dependencies | CURRENT |
| `ProjectSettings/ProjectVersion.txt` | Unity version (6000.0.61f1) | LOCKED |
| `docs/project-vision.md` | Complete specification | REFERENCE |
| `docs/1029refine.md` | Latest design decisions | REFERENCE |

---

## 9. Next Steps for Development

### Immediate (This Phase)

1. **Set up XR Manager system** - Create `Core/XRManager.cs` to initialize OpenXR and manage XR lifecycle
2. **Implement basic movement** - `Core/XRMovement.cs` with smooth + teleport locomotion
3. **Create hand controller visualization** - `Core/XRHandController.cs` to show VR hands
4. **Build test scene** - Simple grabbable objects scene to verify inputs work
5. **Define data structures** - Create `Data/DishData.cs` (ScriptableObject) with the JSON structure from vision document
6. **Scene manager** - `Core/SceneTransitionManager.cs` for safe loading between scenes

### Short-term (Phase 2-3)

7. Create **Tutorial Scene** with interactive VR controls introduction
8. Build **Main Gallery Scene** (can use placeholder environment initially)
9. Implement **Gesture Recognition** system for cooking actions
10. Add **Feedback Systems** (particles, audio cues, haptic)
11. Create **progress tracking** with SessionManager

### Medium-term (Phase 4-5)

12. Build **Cooking Tutorial Template Scene** - reusable scene for all dishes
13. Integrate **audio/subtitles** with narration system
14. Populate **dish data** - create ScriptableObjects for all 5 dishes
15. Import/create **cooking assets** once fieldwork is complete

### Long-term (Phase 6+)

16. **Reunion Dinner Scene** (new design needed per Oct 29 notes - handheld translator UI)
17. **Performance optimization** - LOD, occlusion culling, profiling
18. **User testing & polish** - iterate based on feedback
19. **Academic documentation** - prepare presentation materials

---

## 10. Development Philosophy

Based on the project instructions and previous refinements:

1. **Careful State Management** - As emphasized in Oct 29 notes, keep state handling simple and avoid conflicts between scenes. Prefer composition/events over direct dependencies.

2. **Reusable Functions** - Build modular, reusable systems. The cooking tutorial template should work for all 5 dishes without scene duplication.

3. **Iterative Testing** - Test gesture recognition and feedback systems early. User testing is built into Phase 7.

4. **Respect for Source Material** - Work closely with Chef Shen. Authenticity is a success metric.

5. **Performance First** - Profile regularly. PCVR allows higher visual fidelity but still needs optimization for 90 FPS.

6. **Accessibility Included** - Comfort options (vignette, teleport, snap turn) should be available from Phase 1, not tacked on later.

---

## 11. Git Workflow Notes

- Latest branch: `main`
- Recent commits indicate active development
- Empty CLAUDE.md was just created - fill with content as needed
- Use meaningful commit messages in English for academic project
- Consider creating feature branches for each phase (e.g., `feature/phase-1-foundation`, `feature/gesture-recognition`)

---

## 12. Contact & References

**Project Lead:** Jiesen Huang  
**Mentor:** Giovanni Satini  
**Cultural Advisor:** Chef Shen Jiechen (Acheng Restaurant)  
**Academic Institution:** Duke Kunshan University  

**Key Documentation:**
- Detailed vision: `/docs/project-vision.md` (800+ lines)
- Latest refinements: `/docs/1029refine.md` (Chinese)
- Original concept: `/docs/thought-tracker.md`

---

## Document Info
- **Created:** 2025-11-05
- **Based on:** Analysis of current codebase, project-vision.md, 1029refine.md
- **Status:** Ready for development team reference
- **Last Assessed Codebase State:** Only 2 simple scripts, 1 default scene, full XR infrastructure initialized

