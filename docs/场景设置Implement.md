好的！我来详细说明如何在Unity编辑器中手动配置这些组件属性。让我先检查一下各个脚本的可序列化字段，然后给你详细的配置指南： 

读取 [](file:///d%3A/signature_work/Acheng-Restaurant/Assets/Scripts/Scenes/TutorialSceneController.cs#1-1)，行 1 到 80

读取 [](file:///d%3A/signature_work/Acheng-Restaurant/Assets/Scripts/Scenes/GallerySceneController.cs#1-1)，行 1 到 80

读取 [](file:///d%3A/signature_work/Acheng-Restaurant/Assets/Scripts/Core/XRMovementController.cs#1-1)，行 1 到 80

读取 [](file:///d%3A/signature_work/Acheng-Restaurant/Assets/Scripts/Core/XRHandController.cs#1-1)，行 1 到 80

现在让我查看BaseSceneController以了解继承的字段： 

读取 [](file:///d%3A/signature_work/Acheng-Restaurant/Assets/Scripts/Core/BaseSceneController.cs#1-1)，行 1 到 60

完美！现在我有足够的信息了。让我创建一个详细的配置指南：

## 📖 Unity编辑器手动配置详细指南

---

## 🎯 **第一步：配置Tutorial场景**

### 1. 打开Tutorial场景
```
Unity菜单栏 → File → Open Scene
选择：Assets/Scenes/Tutorial.unity
点击 Open
```

---

### 2. 配置 `TutorialSceneController`

**在Hierarchy中选择:** `TutorialSceneController`

**在Inspector面板中配置：**

#### **Scene Info 部分**
- **Scene Name**: `Tutorial`
  - 输入框中输入：`Tutorial`
  
- **Expected State**: `InTutorial`
  - 下拉菜单选择：`InTutorial`

#### **Tutorial Settings 部分**
- **Allow Skip Tutorial**: ✓（勾选）
  - 允许用户跳过教程
  
- **Tutorial Timeout Seconds**: `300`
  - 5分钟后自动结束教程

#### **UI References 部分**（可选，稍后添加）
- **Tutorial UI**: `None (GameObject)` - 稍后拖拽UI对象
- **Skip Button**: `None (GameObject)` - 稍后添加按钮

---

### 3. 配置 `XR Origin` 的 `XRMovementController`

**在Hierarchy中选择:** `XR Origin`

**在Inspector面板中找到 `XRMovementController` 组件：**

#### **Movement Settings 部分**
- **Move Speed**: `1.5`
  - 玩家移动速度（米/秒）
  
- **Sprint Multiplier**: `1.5`
  - 冲刺时的速度倍数
  
- **Enable Smooth Movement**: ✓（勾选）
  - 启用平滑移动（类似传统FPS）
  
- **Enable Teleportation**: ✓（勾选）
  - 启用传送功能（减少晕动症）

#### **Rotation Settings 部分**
- **Snap Turn Angle**: `30`
  - 快速转向的角度（度）
  
- **Enable Snap Turn**: ✓（勾选）
  - 启用快速转向功能

#### **Comfort Settings 部分**
- **Enable Vignette**: ☐（不勾选）
  - 移动时的视野遮蔽效果（可选，用于减少晕动症）
  
- **Vignette Intensity**: `0.5`
  - 如果启用，这是遮蔽强度

#### **Input Actions 部分**（重要！需要配置输入）
- **Move Action**: 
  1. 点击右侧的小圆圈按钮
  2. 在弹出窗口中找到：`XRI Default Left Controller/Move`
  3. 双击选择
  
- **Turn Action**: 
  1. 点击右侧的小圆圈按钮
  2. 选择：`XRI Default Right Controller/Turn`
  
- **Sprint Action**: 
  1. 点击右侧的小圆圈按钮
  2. 选择：`XRI Default Left Controller/Primary Button` 或类似的按钮

> **💡 提示**：如果找不到这些Input Actions，需要先配置XR Interaction Toolkit的Input Actions。
> 方法：`Window → Package Manager → XR Interaction Toolkit → Samples → Import "Starter Assets"`

---

### 4. 配置 `Left Controller` 的 `XRHandController`

**在Hierarchy中选择:** `XR Origin → Left Controller`

**在Inspector面板中找到 `XRHandController` 组件：**

#### **Hand Type 部分**
- **Hand Type**: `Left`（下拉菜单选择）

#### **Visual Elements 部分**（可选，稍后添加模型）
- **Hand Model**: `None` - 稍后拖拽手部3D模型
- **Controller Model**: `None` - 稍后拖拽控制器3D模型
- **Ray Line**: `None` - 稍后添加LineRenderer

#### **Ray Settings 部分**
- **Ray Max Distance**: `10`
  - 射线最大距离（米）
  
- **Ray Default Color**: 白色 `(R:255, G:255, B:255, A:255)`
  - 点击颜色方块可以调整
  
- **Ray Hover Color**: 绿色 `(R:0, G:255, B:0, A:255)`
  - 悬停在物体上时的颜色
  
- **Ray Width**: `0.02`
  - 射线线条宽度

#### **Haptic Feedback 部分**
- **Haptic Intensity**: `0.5`
  - 触觉反馈强度（0-1）
  
- **Haptic Duration**: `0.1`
  - 触觉反馈持续时间（秒）

#### **Input Actions 部分**
- **Select Action**: 
  - 选择：`XRI Default Left Controller/Select`
  
- **Activate Action**: 
  - 选择：`XRI Default Left Controller/Activate`
  
- **Grip Action**: 
  - 选择：`XRI Default Left Controller/Grip`

---

### 5. 配置 `Right Controller` 的 `XRHandController`

**在Hierarchy中选择:** `XR Origin → Right Controller`

**配置方法与Left Controller相同，但是：**

#### **Hand Type 部分**
- **Hand Type**: `Right`（注意这里选择Right！）

#### **其他配置**
- 其他所有设置与Left Controller相同
- Input Actions选择对应的Right Controller输入

---

### 6. 配置 `TutorialCanvas` 为世界空间

**在Hierarchy中选择:** `TutorialCanvas`

**在Inspector面板中找到 `Canvas` 组件：**

#### **Canvas 部分**
- **Render Mode**: `World Space`（下拉菜单选择）
  - 这会让Canvas在3D空间中渲染
  
- **Event Camera**: 
  - 拖拽 `Main Camera` 到这个字段
  - 或点击右侧圆圈，选择 `Main Camera`

#### **Rect Transform 部分**（已自动设置）
- **Pos X**: `0`
- **Pos Y**: `2`
- **Pos Z**: `3`
- **Scale**: `0.01, 0.01, 0.01`

---

## 🎯 **第二步：配置MainGallery场景**

### 1. 打开MainGallery场景
```
Unity菜单栏 → File → Open Scene
选择：Assets/Scenes/MainGallery.unity（如果路径是MainGallery.unity/MainGallery.unity，选择那个）
点击 Open
```

---

### 2. 配置 `GallerySceneController`

**在Hierarchy中选择:** `GallerySceneController`

**在Inspector面板中配置：**

#### **Scene Info 部分**
- **Scene Name**: `MainGallery`
  
- **Expected State**: `InGallery`

#### **Dish Data 部分**
- **Available Dishes**: `Size: 0`（暂时为空）
  - 稍后创建菜品数据后，修改Size为5
  - 然后将5个DishData资源拖拽到列表中

#### **Gallery Elements 部分**
- **Dish Display Stations**: `Size: 5`
  1. 点击 `Size` 输入框，输入 `5`
  2. 展开列表，会看到 Element 0 到 Element 4
  3. 从Hierarchy中拖拽对应的GameObject：
     - **Element 0**: 拖拽 `DishDisplayStation_1`
     - **Element 1**: 拖拽 `DishDisplayStation_2`
     - **Element 2**: 拖拽 `DishDisplayStation_3`
     - **Element 3**: 拖拽 `DishDisplayStation_4`
     - **Element 4**: 拖拽 `DishDisplayStation_5`
  
- **Reunion Dinner Portal**: `None` - 稍后创建传送门对象
  
- **Completion Indicator UI**: `None` - 稍后创建UI

#### **Audio 部分**（可选）
- **Ambient Music**: `None` - 稍后添加AudioSource组件
- **Gallery Music Clip**: `None` - 稍后导入音频文件

---

### 3. 配置 `XR Origin`

**重复Tutorial场景中的步骤3（XRMovementController配置）**

参数完全相同：
- Move Speed: `1.5`
- Enable Smooth Movement: ✓
- Enable Teleportation: ✓
- Snap Turn Angle: `30`
- Enable Snap Turn: ✓

---

### 4. 配置左右控制器

**重复Tutorial场景中的步骤4和5**

- Left Controller → Hand Type: `Left`, Ray Max Distance: `10`
- Right Controller → Hand Type: `Right`, Ray Max Distance: `10`

---

## 🎨 **第三步：创建菜品数据（重要！）**

### 1. 创建第一个菜品：松鼠鳜鱼

**在Project窗口中：**
```
1. 导航到：Assets/Data/Dishes/
2. 右键点击空白处
3. 选择：Create → Acheng Restaurant → Dish Data
4. 命名为：Squirrel_Fish.asset
```

**选择新创建的Squirrel_Fish.asset，在Inspector中填写：**

#### **Dish Identity**
- **Dish ID**: `squirrel_fish`（必须唯一，用下划线命名）

#### **Dish Names**
- **English Name**: `Squirrel Fish`
- **Chinese Name**: `松鼠鳜鱼`
- **Pinyin Name**: `Sōngshǔ Guìyú`

#### **Classification**
- **Category**: `Main`（下拉菜单）
- **Difficulty**: `Advanced`（下拉菜单）

#### **Descriptions**
- **Brief Description**: 
```
A signature Suzhou dish featuring crispy fried fish with sweet and sour sauce, named for its squirrel-like appearance when served.
```

- **Detailed Description**: 
```
Squirrel Fish is one of the most celebrated dishes in Jiangsu cuisine. The fish is carefully deboned, scored in a crosshatch pattern, and deep-fried until golden and crispy. When plated, it curls up to resemble a squirrel's bushy tail. The dish is then topped with a vibrant sweet and sour sauce made from tomatoes, sugar, and vinegar, creating a perfect balance of flavors and textures.
```

#### **Cultural Context**
- **Historical Origin**: 
```
Dating back to the Qing Dynasty, this dish was allegedly created for Emperor Qianlong during his visit to Suzhou. The chef transformed a carp from the imperial pond into this masterpiece to avoid punishment.
```

- **Cultural Symbolism**: 
```
Represents prosperity and abundance in Chinese culture. The fish symbolizes surplus (余), while its playful presentation brings joy to family gatherings.
```

- **Regional Significance**: 
```
A cornerstone of Suzhou cuisine, showcasing the region's emphasis on precise knife work, delicate flavors, and artistic presentation.
```

- **Craftsmanship Notes**: 
```
Requires expert knife skills to score the fish without cutting through. The frying temperature must be precisely controlled to achieve the signature crispy exterior while keeping the interior tender.
```

#### **Cooking Steps**
- **Size**: `6`（输入6，会出现6个步骤）

**Step 0（第一步）：**
- **Step Number**: `1`
- **Title**: `Prepare the Fish`
- **Subtitles**: `First, we carefully debone and score the fish in a diamond pattern.`
- **Required Gesture**: `Chopping`
- **Success Threshold**: `0.7`
- **Narration Duration**: `45`

**Step 1-5**: 继续填写其他步骤...

---

### 2. 重复创建其他4道菜

使用相同的方法创建：
- `Dongpo_Pork.asset` （东坡肉）
- `Longjing_Shrimp.asset` （龙井虾仁）
- `Sweet_Osmanthus_Cake.asset` （桂花糕）
- `West_Lake_Fish.asset` （西湖醋鱼）

---

## ✅ **第四步：测试配置**

### 1. 测试Tutorial场景

```
1. 确保Tutorial场景已打开
2. 点击Unity顶部的 Play 按钮（▶️）
3. 在Console窗口（Window → General → Console）查看日志：
   ✓ 应该看到：[TutorialScene] Tutorial started
   ✓ 不应该有红色错误信息
4. 点击 Play 按钮停止
```

### 2. 测试MainGallery场景

```
1. 打开MainGallery场景
2. 点击 Play 按钮
3. Console应该显示：[GalleryScene] Gallery initialized
4. 停止播放
```

### 3. 测试场景切换

```
1. 在Tutorial场景的Play模式下
2. 在Hierarchy中找到 [SceneTransitionManager]
3. 在Inspector中，右键点击 SceneTransitionManager 组件
4. 选择调试菜单中的场景切换功能
5. 应该能够切换到MainGallery场景
```

---

## 🔧 **常见问题解决**

### ❌ "找不到Input Actions"
**解决方案：**
```
1. Window → Package Manager
2. 找到 XR Interaction Toolkit
3. 在右侧面板点击 "Samples"
4. 点击 "Starter Assets" 的 Import 按钮
5. 导入后，Input Actions就会出现在选择列表中
```

### ❌ "Canvas不显示在VR中"
**解决方案：**
```
1. 确保Canvas的Render Mode是 World Space
2. Event Camera必须指向Main Camera
3. Canvas Scale要设置为 0.01（否则太大看不到）
```

### ❌ "控制器没有射线"
**解决方案：**
```
1. 需要手动添加 XRRayInteractor 组件到控制器
2. 或者在代码中已经自动添加，但需要配置Input Actions
```

---

## 📝 **配置清单（检查用）**

打印这个清单，逐项检查：

**Tutorial场景：**
- [ ] TutorialSceneController: Scene Name = "Tutorial"
- [ ] TutorialSceneController: Expected State = "InTutorial"
- [ ] XRMovementController: Move Speed = 1.5
- [ ] XRMovementController: Enable Smooth Movement = ✓
- [ ] Left Controller: Hand Type = Left
- [ ] Right Controller: Hand Type = Right
- [ ] TutorialCanvas: Render Mode = World Space

**MainGallery场景：**
- [ ] GallerySceneController: Scene Name = "MainGallery"
- [ ] GallerySceneController: Expected State = "InGallery"
- [ ] GallerySceneController: 5个DishDisplayStation已分配
- [ ] XRMovementController配置完成
- [ ] 左右控制器配置完成

**菜品数据：**
- [ ] 至少创建了1个DishData资源用于测试
- [ ] Dish ID已正确填写

---

按照这个详细指南操作，你应该能够完成所有配置！如果遇到具体问题，随时告诉我！🚀