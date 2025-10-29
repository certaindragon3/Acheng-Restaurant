# Acheng Restaurant VR Experience
## Project Vision & Planning Document

---

## 1. Project Overview

### 1.1 Project Identity
**Project Name:** Acheng Restaurant VR Experience
**Type:** Academic VR Interactive Museum Experience
**Language:** English
**Target Duration:** 12-15 minutes
**Platform:** PCVR (OpenXR)
**Target Devices:**
  - Meta Quest 3 (via PC Link/Air Link streaming)
  - HTC Vive Vision (native PCVR)
**Number of Dishes:** 5 signature Subang dishes
**Development Timeline:** 13 weeks
**Fieldwork Completion:** November 26, 2025

### 1.2 Vision Statement
An immersive virtual museum experience that preserves and educates users about the traditional culinary heritage of Suzhou cuisine (Subang cuisine) through the lens of Acheng Restaurant. This academic project combines cultural preservation, interactive education, and sensory storytelling to celebrate the labor-intensive traditional cooking techniques that define authentic Subang cuisine.

### 1.3 Research Subject
**Acheng Restaurant (阿成饭店)** - A traditional Subang cuisine restaurant known for its dedication to authentic, labor-intensive cooking methods that preserve centuries-old culinary traditions.

### 1.4 Core Value Proposition
- **Cultural Preservation:** Documenting and showcasing traditional Suzhou culinary techniques in an accessible digital format
- **Hands-on Education:** Interactive cooking demonstrations that teach authentic preparation methods
- **Academic Rigor:** Scholarly context provided for each dish, covering historical, cultural, and regional significance
- **Immersive Storytelling:** Recreating the authentic atmosphere of Acheng Restaurant through spatial scanning technology

---

## 2. Target Audience & Goals

### 2.1 Primary Audience
- **Academic Researchers:** Culinary anthropologists, food historians, cultural studies scholars
- **Culinary Students:** Professional chefs and cooking school students interested in authentic Chinese cuisine
- **Cultural Enthusiasts:** Individuals interested in Chinese food culture and heritage preservation
- **General VR Users:** People seeking educational and culturally enriching VR experiences

### 2.2 Learning Objectives
By the end of the experience, users will be able to:
1. Identify 5 signature Subang dishes and their cultural significance
2. Understand the key preparation techniques that define traditional Subang cuisine
3. Recognize the labor-intensive nature and craftsmanship required for authentic dishes
4. Appreciate the historical and cultural context of Suzhou culinary traditions
5. Experience the warm, familial atmosphere of traditional Chinese dining culture

### 2.3 Success Metrics
- **Engagement:** 80%+ completion rate for at least 2-3 dish tutorials
- **Educational Value:** Users can recall at least 3 key cooking techniques after experience
- **Emotional Impact:** Users report feeling connected to the cultural heritage
- **Technical Quality:** Smooth VR performance with minimal motion sickness

---

## 3. Experience Flow

### 3.1 Overall Journey Map

```
[START]
   ↓
[Tutorial Scene - VR Controls Introduction] (2 min)
   ↓
[Main Gallery - Restaurant Environment] (1-2 min exploration)
   ↓
[Select Dish 1] → [Cooking Tutorial 1] (5 min)
   ↓
[Return to Gallery]
   ↓
[Select Dish 2] → [Cooking Tutorial 2] (5 min)
   ↓
[Optional: Select Dish 3] → [Cooking Tutorial 3] (5 min)
   ↓
[Unlock: Reunion Dinner Scene] (3-4 min)
   ↓
[END - Credits & Acknowledgments]
```

### 3.2 Scene Breakdown

#### Scene 1: Tutorial (2 minutes)
**Purpose:** Familiarize users with VR controls and interaction mechanics

**Key Elements:**
- Simple, neutral environment (minimalist design)
- Interactive tutorial prompts
- Practice areas for:
  - Locomotion (walking/teleportation)
  - Hand tracking/controller usage
  - Object selection and interaction
  - Basic cooking gesture practice (stirring, chopping, etc.)

**Exit Condition:** User completes all tutorial objectives OR skips tutorial

---

#### Scene 2: Main Gallery (1-2 minutes + return visits)
**Purpose:** Central hub for dish selection and atmospheric immersion

**Environment Design:**
- Photogrammetry-scanned recreation of actual Acheng Restaurant interior
- Warm, inviting lighting that captures the restaurant's ambiance
- Cultural decorations and authentic details preserved from real location

**Spatial Layout:**
- Player spawns in center of circular/semi-circular room
- 5 display tables arranged around perimeter
- Each table features:
  - 3D model of completed dish on decorative plate
  - Informational placard with dish name and brief description
  - Soft spotlight highlighting the dish

**Interaction Mechanic:**
- Point controller at dish → dish model gently floats upward (hover state)
- Hover state reveals additional information (ingredients, difficulty level)
- Click/trigger to enter cooking tutorial for that dish
- Ambient audio: soft traditional music, subtle restaurant ambiance

**Progression Logic:**
- Initially, all 5 dishes available for selection
- After completing 2-3 dishes, visual indicator appears for Reunion Dinner scene access
- Doorway/portal to final scene becomes visible and accessible

---

#### Scene 3: Cooking Tutorial (5 minutes per dish, repeatable)
**Purpose:** Teach authentic preparation methods through guided VR interaction

**Structure:**
**Phase 1 - Introduction (30 seconds)**
- Chef introduction (voice-over + subtitles)
- Dish name and cultural background
- Brief overview of what user will learn

**Phase 2 - Cooking Steps (3.5-4 minutes)**
- **6-8 simplified key steps** (30-40 seconds each)
- Each step includes:
  - **Visual demonstration:** Chef's hands shown performing action
  - **Voice-over narration:** Chef explains technique and importance
  - **English subtitles:** Synchronized with audio
  - **Interactive prompt:** User mimics the action with VR hands/controllers
  - **Success feedback:** Visual/audio cue (particle effects, encouraging voice line)
  - **Skip option:** Button to skip this step if desired

**Example Step Breakdown (Generic):**
1. Ingredient preparation (washing, cutting)
2. Heat control and wok preparation
3. Primary cooking technique (stir-frying, braising, etc.)
4. Seasoning sequence
5. Technique refinement (specific to dish)
6. Plating and presentation

**Phase 3 - Completion (30 seconds)**
- Finished dish presentation
- Chef's final wisdom/cultural insight
- Encouragement and transition back to gallery

**Interaction Design:**
- **Guided, not punitive:** Users cannot "fail" - steps provide gentle guidance
- **Encouraging feedback:** Positive reinforcement for successful actions
- **Flexible pacing:** Users control when to move to next step
- **Educational focus:** Emphasize "why" alongside "how"

---

#### Scene 4: Reunion Dinner (3-4 minutes)
**Purpose:** Culminating experience that contextualizes individual dishes within broader cultural tradition

**Environment:**
- Traditional round dining table (symbolizing unity and completeness)
- Full spread of Subang dishes arranged beautifully
- Warm, familial lighting
- Ambient sounds of family gathering (subtle, respectful)

**Content Presentation:**
- **Visual feast:** All dishes from tutorials plus additional traditional items
- **Academic narration:** Scholarly voice-over discussing:
  - Historical significance of reunion dinners in Chinese culture
  - Role of food in family bonding and cultural identity
  - Subang cuisine's place in Chinese culinary heritage
  - Craftsmanship and generational knowledge transmission
  - Modern challenges and importance of preservation

**Interaction:**
- User can walk around table, observe dishes closely
- Optional: Select individual dishes to hear specific cultural stories
- Contemplative, museum-like pacing

**Emotional Arc:**
- Celebration of cultural richness
- Reflection on tradition and heritage
- Inspiration to value and preserve culinary craftsmanship

---

## 4. Scene Design Specifications

### 4.1 Main Gallery - Detailed Layout

**Spatial Dimensions:**
- Room diameter: ~8-10 meters (VR space)
- Player spawn point: Center, elevated slightly on decorative rug
- Display tables: 5 stations evenly distributed in 270° arc

**Display Station Components:**
| Element | Specifications |
|---------|---------------|
| Table | Traditional wooden design, ~1m height |
| Dish Model | High-fidelity 3D scan, photorealistic materials |
| Information Placard | Bilingual (Chinese characters + English), elegant calligraphy style |
| Lighting | Warm spotlight (2700K), subtle rim lighting on dish |
| Interaction Zone | 1.5m radius from table center |

**Environmental Details:**
- Wall decorations: Calligraphy scrolls, traditional paintings
- Furniture: Antique cabinets, cultural artifacts from restaurant
- Flooring: Polished wood, traditional Chinese patterns
- Ceiling: Lanterns, wooden beams (if present in real restaurant)
- Audio: 60% ambiance, 40% traditional guzheng/pipa music

### 4.2 Cooking Tutorial Scene - Kitchen Setup

**Layout:**
- Chef demonstration area: Directly in front of player (2m distance)
- Player workspace: Countertop at comfortable VR height (~0.9m)
- Ingredient stations: Left and right sides, within arm's reach
- Tools rack: Above counter, easily accessible

**Visual Design:**
- Clean, well-lit professional kitchen aesthetic
- Authenticity balanced with clarity (not cluttered)
- Focus on essential elements for current step
- Dynamic object highlighting (next item to use glows subtly)

---

## 5. Interaction Design

### 5.1 VR Control Scheme

**Locomotion:**
- **Primary:** Smooth continuous movement (left thumbstick)
- **Alternative:** Teleportation (for comfort-sensitive users)
- **Rotation:** Snap turning 30° increments (right thumbstick)
- **Speed:** Walking pace (1.5 m/s), no sprinting needed

**Hand Interactions:**
- **Selection:** Ray-cast pointer from controller, trigger to select
- **Grasping:** Grip button to pick up objects
- **Cooking Actions:** Context-sensitive button prompts
  - Trigger = Stir/mix
  - Grip = Grab/hold
  - Buttons = Special actions (flip, season, etc.)

### 5.2 Cooking Gesture Library

**Essential Actions (6-8 per tutorial):**
1. **Chopping:** Downward motion with hand tracking
2. **Stirring:** Circular motion in wok
3. **Pouring:** Tilt controller/hand to pour ingredients
4. **Flipping:** Quick upward wrist flick
5. **Seasoning:** Pinch gesture or button hold
6. **Plating:** Grab and place motion
7. **Heat Adjustment:** Dial turn gesture
8. **Washing:** Side-to-side motion under virtual water

**Gesture Tolerance:**
- Generous hit-boxes for success detection
- Visual guides show correct motion path
- Success threshold: 70% accuracy (forgiving)

### 5.3 Feedback Systems

**Visual Feedback:**
- ✓ Green particle burst on successful action
- ➤ Glowing outline on next interactive object
- ⚠ Gentle yellow pulse if user seems stuck (after 15 seconds)
- ✨ Trail effects following correct gesture motion

**Audio Feedback:**
- Cooking sounds (sizzling, chopping, pouring) - realistic
- Success chime - pleasant, not intrusive
- Chef encouragement - "Perfect technique!", "Well done!", "Excellent!"
- Ambient kitchen sounds maintain immersion

**Haptic Feedback:**
- Controller vibration on object contact
- Rhythmic pulse during stirring actions
- Short buzz on successful step completion

---

## 6. Data Structure

### 6.1 Dish Data Model

```json
{
  "dishID": "string (unique identifier)",
  "dishName": {
    "english": "string",
    "chinese": "string (汉字)",
    "pinyin": "string (romanization)"
  },
  "category": "string (appetizer/main/soup/etc.)",
  "description": {
    "brief": "string (50 words - for gallery placard)",
    "detailed": "string (200 words - for intro narration)"
  },
  "culturalContext": {
    "historicalOrigin": "string",
    "culturalSymbolism": "string",
    "regionalSignificance": "string",
    "craftmanshipNotes": "string"
  },
  "cookingTutorial": {
    "estimatedDuration": "number (seconds)",
    "difficultyLevel": "string (beginner/intermediate/advanced)",
    "steps": [
      {
        "stepNumber": "number",
        "title": "string",
        "narration": {
          "audioFile": "string (path)",
          "subtitles": "string (English text)",
          "duration": "number (seconds)"
        },
        "interaction": {
          "type": "string (chop/stir/pour/etc.)",
          "requiredGesture": "string (gesture ID)",
          "successCriteria": "object (thresholds)",
          "visualGuide": "string (animation reference)"
        },
        "ingredients": ["array of ingredient IDs used in this step"],
        "tools": ["array of tool IDs used in this step"]
      }
    ],
    "completionMessage": "string (chef's final words)"
  },
  "assets": {
    "dishModel3D": "string (path)",
    "platingReference": "string (texture/image)",
    "ingredientModels": ["array of asset paths"],
    "toolModels": ["array of asset paths"]
  }
}
```

### 6.2 Progress Tracking

```json
{
  "userSession": {
    "sessionID": "string (UUID)",
    "startTime": "timestamp",
    "completedTutorial": "boolean",
    "dishesCompleted": [
      {
        "dishID": "string",
        "completionTime": "timestamp",
        "stepsCompleted": "number",
        "stepsSkipped": "number"
      }
    ],
    "unlockedReunionDinner": "boolean",
    "totalDuration": "number (seconds)"
  }
}
```

---

## 7. UI/UX Guidelines

### 7.1 Visual Language

**Typography:**
- **Headings:** Elegant serif font (reminiscent of traditional Chinese typography)
- **Body Text:** Clean sans-serif for readability in VR
- **Subtitles:** High-contrast, easy to read, positioned below chef demonstration
- **Size:** Minimum 36pt equivalent for VR legibility

**Color Palette:**
- **Primary:** Warm reds and golds (traditional Chinese auspicious colors)
- **Secondary:** Rich browns and dark woods (restaurant aesthetic)
- **Accent:** Soft jade green (cultural reference)
- **UI Elements:** White/cream for contrast, semi-transparent backgrounds

**Spatial UI:**
- Diegetic when possible (in-world menus, signs)
- Floating UI panels anchored to world space
- Minimal HUD - only when necessary (step counter, skip button)

### 7.2 Accessibility Considerations

**Visual:**
- High contrast mode option
- Colorblind-friendly indicators (not relying solely on color)
- Adjustable subtitle size and background opacity

**Audio:**
- Subtitles for all voice-over content
- Visual cues accompany audio feedback
- Adjustable audio levels (voice/music/effects separately)

**Comfort:**
- Teleportation alternative to smooth locomotion
- Snap turning instead of smooth rotation
- Vignette effect during movement (optional, user-controlled)
- No sudden camera movements or forced perspective changes

**Language:**
- Primary: English
- Future consideration: Multilingual support (Chinese, Japanese, etc.)

### 7.3 Onboarding & Guidance

**Progressive Disclosure:**
- Tutorial introduces one mechanic at a time
- Gallery allows free exploration before first dish selection
- Each cooking tutorial builds on previous lessons

**Help System:**
- Context-sensitive hints appear after user inactivity (15-20 seconds)
- "Help" button accessible at any time (pauses and shows control reference)
- Optional re-tutorial function in main menu

---

## 8. Technical Architecture

### 8.1 Unity Setup

**Core Systems:**
- **XR Framework:** OpenXR (cross-platform VR support)
- **Input System:** Unity New Input System (already integrated)
- **Rendering:** URP (Universal Render Pipeline) for optimized VR performance
- **Audio:** Unity Audio with spatial sound

**Key Packages:**
- `com.unity.xr.openxr` - VR runtime (already integrated)
- `com.unity.inputsystem` - Controller mapping (already integrated)
- `com.unity.xr.interaction.toolkit` - VR interaction helpers (to be added)

**Platform Configuration:**
- **Primary Development Target:** PCVR
- **Supported Devices:**
  - Meta Quest 3 (via Oculus Link/Air Link)
  - HTC Vive Vision (OpenXR native)
- **Performance Baseline:** Desktop PC (RTX 3060 or equivalent minimum)

### 8.2 Project Structure

```
Assets/
├── Scenes/
│   ├── Tutorial.unity
│   ├── MainGallery.unity
│   ├── CookingTutorial_Template.unity
│   └── ReunionDinner.unity
│
├── Scripts/
│   ├── Core/
│   │   ├── XRMovement.cs
│   │   ├── XRHandController.cs
│   │   └── SceneTransitionManager.cs
│   │
│   ├── Interaction/
│   │   ├── DishSelector.cs
│   │   ├── CookingGestureRecognizer.cs
│   │   └── InteractableObject.cs
│   │
│   ├── Tutorial/
│   │   ├── TutorialStep.cs
│   │   ├── CookingStep.cs
│   │   └── StepSequencer.cs
│   │
│   ├── Data/
│   │   ├── DishData.cs (ScriptableObject)
│   │   └── ProgressManager.cs
│   │
│   └── UI/
│       ├── SubtitleController.cs
│       ├── InfoPlacardDisplay.cs
│       └── FeedbackEffects.cs
│
├── Data/
│   └── Dishes/
│       ├── Dish_01.asset
│       ├── Dish_02.asset
│       └── ... (ScriptableObject instances)
│
├── Models/
│   ├── Environment/
│   │   └── RestaurantScan/
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
└── Prefabs/
    ├── XR_Origin.prefab
    ├── DishDisplayStation.prefab
    └── CookingWorkspace.prefab
```

### 8.3 Performance Targets

**PCVR Requirements:**
- **Frame Rate:** 90 FPS (target for both devices), 72 FPS minimum
- **Draw Calls:** <300 per scene (PCVR allows higher budget than standalone)
- **Polygon Budget:** 1-2M tris per scene (active view) - higher fidelity for PC
- **Texture Memory:** <4GB total (PC GPU VRAM budget)
- **Resolution:** High-quality textures (2K-4K for hero assets)

**Optimization Strategies:**
- LOD (Level of Detail) for dish models (3 LOD levels)
- Occlusion culling in gallery scene
- Object pooling for cooking ingredients
- Compressed audio formats (Vorbis for music, uncompressed for voice)
- Lightmap baking for static lighting + real-time lights for interactables
- GPU instancing for repeated decorative objects
- Multi-threaded rendering enabled

**Quality Settings:**
- Single quality preset optimized for RTX 3060+ GPUs
- Auto-detection and fallback for lower-spec systems
- No mobile-specific optimizations needed (pure PCVR)

---

## 9. Content Requirements

### 9.1 Asset Needs

**3D Models:**
- [x] XR Origin rig (Unity default)
- [ ] Restaurant interior (photogrammetry scan - to be captured in fieldwork by Nov 26)
- [ ] 5 finished dish models (high-quality 3D scans or photogrammetry)
- [ ] ~25-35 ingredient models (vegetables, proteins, seasonings)
- [ ] ~15 cooking tool models (wok, cleaver, spatula, bowls, etc.)
- [ ] Furniture and decorative items for atmosphere

**Audio:**
- [ ] Chef voice-over narration (5 dishes × ~4 minutes = 20-25 min total)
- [ ] Background music (traditional Suzhou/Jiangnan style, 3-4 tracks)
- [ ] Cooking SFX library (chopping, sizzling, pouring, etc.)
- [ ] Ambient restaurant sounds

**Text Content:**
- [ ] Dish descriptions (brief and detailed)
- [ ] Cultural context essays (historical, regional, symbolic)
- [ ] Tutorial instructions
- [ ] Reunion dinner narration script
- [ ] Subtitle files (SRT format)

### 9.2 Content Creation Pipeline

**Phase 1: Research & Scripting**
1. Conduct fieldwork at Acheng Restaurant (deadline: Nov 26, 2025)
2. Select 5 signature dishes
3. Interview chef (Shen Jiechen) for cooking insights
4. Research cultural/historical context
5. Write narration scripts and academic content
6. Capture photogrammetry data of restaurant interior

**Phase 2: Asset Production**
1. Restaurant photogrammetry scanning
2. Dish photography and 3D scanning
3. Voice-over recording with chef or voice actor
4. Music licensing or original composition
5. SFX library acquisition

**Phase 3: Integration**
1. Import and optimize 3D assets
2. Sync audio with subtitles
3. Populate ScriptableObject dish data
4. Build cooking interaction sequences
5. Playtest and iterate

---

## 10. Development Phases

### Phase 1: Foundation (Weeks 1-2)
**Goal:** Establish core VR functionality and project infrastructure

**Deliverables:**
- ✓ VR movement system (walk/teleport)
- ✓ Basic hand controller visualization
- ✓ Simple test scene with grabbable objects
- ✓ Scene transition system
- ✓ Data structure implementation (ScriptableObjects)

**Success Criteria:**
- User can move smoothly in VR without discomfort
- User can select and grab objects reliably
- Scene loading works without errors

---

### Phase 2: Tutorial & Interaction (Weeks 3-4)
**Goal:** Build tutorial scene and cooking gesture system

**Deliverables:**
- Tutorial scene with step-by-step VR training
- Cooking gesture recognition (6-8 core actions)
- Visual/audio feedback systems
- Skip functionality
- Progress tracking

**Success Criteria:**
- 90%+ users can complete tutorial within 2 minutes
- Gesture recognition accuracy >85%
- Feedback feels responsive and encouraging

---

### Phase 3: Main Gallery Scene (Week 5)
**Goal:** Create central hub with dish selection

**Deliverables:**
- Gallery environment layout (placeholder or scanned)
- 4-5 dish display stations
- Hover/selection interactions
- Info placard UI
- Ambient audio/music integration

**Success Criteria:**
- Scene loads within 3 seconds
- Dish selection feels intuitive
- Atmosphere is warm and inviting

---

### Phase 4: Cooking Tutorial Template (Weeks 6-7)
**Goal:** Build reusable cooking tutorial scene

**Deliverables:**
- Cooking scene environment
- Step sequencer system
- Chef narration + subtitle sync
- Interactive cooking actions (1 complete dish as prototype)
- Return-to-gallery transition

**Success Criteria:**
- One fully functional dish tutorial (5 min experience)
- All gestures work smoothly
- Audio and visuals are synced

---

### Phase 5: Content Population (Weeks 8-9)
**Goal:** Complete all dish tutorials and cultural content

**Dependencies:**
- Fieldwork completed by Nov 26, 2025
- 5 dishes selected and documented
- Voice-over recorded or scheduled

**Deliverables:**
- 5 complete dish tutorials
- All narration recorded and integrated
- Cultural context text written and displayed
- Academic rigor verification

**Success Criteria:**
- All tutorials playable end-to-end
- Content is accurate and respectful
- Total experience time: 12-15 minutes

---

### Phase 6: Reunion Dinner Scene (Week 10)
**Goal:** Create culminating experience

**Deliverables:**
- Reunion dinner environment
- Full table spread of dishes
- Academic narration and visuals
- Unlock condition (2-3 dishes completed)

**Success Criteria:**
- Scene evokes emotional connection
- Academic content is clear and meaningful
- Feels like a satisfying conclusion

---

### Phase 7: Polish & Testing (Weeks 11-12)
**Goal:** Refine experience and ensure quality

**Deliverables:**
- Performance optimization (90 FPS target)
- Bug fixes
- User testing sessions (5-10 participants)
- Accessibility improvements
- Final audio/visual polish

**Success Criteria:**
- No critical bugs
- 80%+ user completion rate
- Positive feedback on cultural authenticity
- Smooth performance on target hardware

---

### Phase 8: Academic Presentation Prep (Week 13)
**Goal:** Prepare for academic showcase

**Deliverables:**
- Documentation of development process
- Academic paper/presentation materials
- Demo video (trailer)
- Acknowledgments and credits
- Installation guide for exhibition

---

## 11. Risk Assessment & Mitigation

| Risk | Likelihood | Impact | Mitigation Strategy |
|------|------------|--------|---------------------|
| Fieldwork delays (restaurant access) | Low | High | Nov 26 deadline confirmed; begin with placeholder if needed |
| 3D scanning quality issues | Medium | Medium | Test equipment before fieldwork; backup manual modeling |
| Voice-over recording challenges | Low | Medium | Chef Shen Jiechen confirmed; backup voice actor if needed |
| VR motion sickness in users | Low | Medium | PCVR allows better performance; implement comfort options |
| Technical performance issues | Low | Medium | PCVR baseline higher; regular profiling; PC-optimized assets |
| Cultural misrepresentation | Low | Critical | Direct collaboration with Chef Shen; mentor review |
| Scope creep | High | Medium | Strict adherence to 5 dishes; resist feature additions |

---

## 12. Success Indicators

**Academic Impact:**
- Publication in culinary heritage or VR education journals
- Acceptance to academic conferences (CHI, SIGGRAPH, food studies)
- Adoption by culinary schools or museums

**User Engagement:**
- Average completion rate >75%
- Positive qualitative feedback on cultural authenticity
- Users report learning new culinary knowledge

**Technical Quality:**
- Maintains 90 FPS throughout experience
- <5% of users report discomfort
- Zero critical bugs in final release

**Cultural Preservation:**
- Accurate representation verified by Acheng Restaurant
- Contribution to digital heritage archive
- Potential for expanded series covering more restaurants/cuisines

---

## 13. Future Expansion Possibilities

**Post-Launch Enhancements:**
- Additional dishes from Acheng Restaurant
- Other Suzhou restaurants and regional cuisines
- Multiplayer mode (cook together with friends)
- Recipe export feature (printable recipe cards)
- Behind-the-scenes documentary content
- VR museum exhibition integration

**Platform Expansion:**
- Standalone Quest version (native, post-PCVR completion)
- Non-VR desktop version for broader accessibility
- Educational institution licensing
- Translation into Chinese, Japanese, other languages
- WebXR version for browser-based access

---

## 14. Acknowledgments & Credits (Preliminary)

**Research:**
- Acheng Restaurant and Chef Shen Jiechen
- Giovanni Satini - Mentor
- Culinary advisors and cultural consultants: Shen Jiechen

**Special Thanks:**
- OpenXR and Unity communities
- VR development resources and tutorials
- Duke Kunshan University

---

## Document Version Control
- **Version:** 1.1
- **Last Updated:** 2025-10-29
- **Author:** Jiesen Huang
- **Status:** Approved - Ready for Development

---

## 15. Project Parameters (Confirmed)

**Technical Specifications:**
- Platform: PCVR (OpenXR)
- Target Devices: Meta Quest 3 (Link/Air Link), HTC Vive Vision
- Performance Target: RTX 3060+ equivalent
- No standalone mobile optimization required

**Content Specifications:**
- Number of Dishes: 5 signature Subang dishes
- Fieldwork Deadline: November 26, 2025
- Chef Collaboration: Shen Jiechen (confirmed)
- Total Experience Duration: 12-15 minutes

**Development Parameters:**
- Timeline: 13 weeks
- Required Plugin: XR Interaction Toolkit (to be added)
- Existing Integrations: OpenXR, Unity Input System

---

**Next Steps:**
1. ✓ Project vision document approved
2. Begin Phase 1 development (VR foundation and basic systems)
3. Prepare for fieldwork (equipment checklist, interview questions)
4. Schedule regular progress reviews
