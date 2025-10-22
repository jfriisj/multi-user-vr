# ✅ Phase 1 Execution Checklist

**Use this checklist while working in Unity Editor**  
**Check off each item as you complete it**

---

## 🎯 Pre-Flight Checks

- [x] Unity Hub is installed
- [x] Unity Editor 6000.2.3f1 is installed
- [x] Project path has no spaces: `c:\github\multi-user-vr\building-block` ✅
- [x] Git is tracking changes (optional but recommended)

---

## 🚀 Step 1: Launch Unity Editor (5 min)

- [x] Open **Unity Hub**
- [x] Click on `building-block` project to launch
- [x] Wait for Unity Editor to fully load (progress bar complete)
- [x] Wait for package import to finish (may take 2-3 minutes)
- [x] Check Unity Console (Window > General > Console) - should be clear or only warnings

**Validation**: Unity Editor is open and responsive ✅

---

## 📂 Step 2: Open Main Scene (1 min)

- [x] In Unity Editor, go to **File > Open Scene**
- [x] Navigate to `Assets/Scenes/`
- [x] Double-click **SampleScene.unity**
- [x] Scene loads in Hierarchy window

**Validation**: You see "SampleScene" in Hierarchy with default objects ✅

---

## ⚡ Step 3: Run Automated VR Setup (5 min)

### 3a. Check for Menu
- [x] In Unity menu bar, look for **"Multi-User VR"** menu
  - If NOT visible: Check Console for script errors, try **Assets > Reimport All**

### 3b. Execute Setup
- [x] Click **Multi-User VR > Setup > Phase 1 - Configure VR Scene**
- [x] Wait for console messages (should take <10 seconds)

### 3c. Check Console Output
Expected messages (check each):
- [x] `✅ Removed existing Main Camera`
- [x] `✅ Created OVRCameraRig at origin`
- [x] `✅ Created and configured OVRManager`
- [x] `✅ Quest 3 VR scene setup complete!`

### 3d. Verify Scene Changes
In Hierarchy window, verify:
- [x] **OVRCameraRig** GameObject exists
- [x] **OVRManager** GameObject exists
- [x] No "Main Camera" GameObject (should be deleted)

**Validation**: Scene now has VR camera setup ✅

---

## ✔️ Step 4: Validate Setup (2 min)

- [x] Click **Multi-User VR > Setup > Validate Phase 1 Setup**
- [x] Check Console output for validation results

Expected validation messages:
- [x] `✅ OVRCameraRig found and properly configured`
- [x] `✅ OVRManager found with Quest 3 settings`
- [x] `✅ Unity Netcode package available`
- [x] `✅ No conflicting Main Camera detected`
- [x] `✅ Phase 1 Setup Valid - Ready for Testing`

**If any ❌ errors**: Note them and troubleshoot (see TROUBLESHOOTING section)

**Validation**: All checks pass ✅

---

## 💾 Step 5: Save Scene (30 sec)

- [x] Press **Ctrl+S** (or **Cmd+S** on Mac)
- [x] Or click **File > Save**
- [x] Confirm save dialog if prompted
- [x] Asterisk (*) next to scene name should disappear

**Validation**: Scene saved with VR setup ✅

---

## 🎮 Step 6: Add Controller Input (5 min)

### 6a. Open Building Blocks
- [ ] Click **Window > Meta XR > Tools > Building Blocks**
- [ ] Building Blocks window opens (may take 10-15 seconds)

**If window is empty**: Wait for Meta XR SDK to fully import, or restart Unity

### 6b. Add Controller Building Block
- [x] In Building Blocks window, find **"Interaction SDK"** category
- [x] Locate **"Controller"** building block
- [x] Click **"Add"** button
- [x] Wait for prefab to be added to scene (5-10 seconds)

### 6c. Verify Controller Added
In Hierarchy, check for new GameObjects:
- [x] Controller-related components added to scene
- [x] No errors in Console

### 6d. (Optional) Add Hand Tracking
- [x] In Building Blocks, find **"Hand Tracking"** building block
- [x] Click **"Add"** button
- [x] Verify hand tracking components added

### 6e. Save Scene Again
- [ ] Press **Ctrl+S** to save with input setup

**Validation**: Input sources added to scene ✅

---

## 🧪 Step 7: Test in Meta XR Simulator (10 min)

### 7a. Open Simulator
- [x] Click **Window > Meta XR > Test in Simulator**
- [x] Simulator window opens (may be a separate floating window)

### 7b. Enter Play Mode
- [x] Click the **Play** button ▶️ at top of Unity Editor
- [x] Wait for Play mode to start (5-10 seconds)
- [x] Game view should show VR camera perspective

### 7c. Test Movement
- [ ] Press **W** - should move forward
- [ ] Press **S** - should move backward
- [ ] Press **A** - should strafe left
- [ ] Press **D** - should strafe right
- [x] Move **Mouse** - should rotate camera view
- [ ] Press **Space** - should recenter tracking

### 7d. Check Performance
- [ ] Look at **Game view** stats (top right corner)
- [ ] FPS should be 60+ (editor target, Quest 3 targets 90fps)
- [ ] No stuttering or lag

### 7e. Check Console
- [ ] Switch to Console window
- [ ] Should have no red errors
- [ ] Yellow warnings are usually okay

### 7f. Exit Play Mode
- [ ] Click **Play** button ▶️ again to stop
- [ ] Verify scene returns to edit mode

**Validation**: VR experience works smoothly in simulator ✅

---

## 🎭 Step 8: Create Player Prefab (10 min)

### 8a. Create Player GameObject
- [x] In Hierarchy, right-click > **Create Empty**
- [x] Rename to **"PlayerController"**
- [x] Position at origin: Transform Position (0, 0, 0)

### 8b. Add UserTrackingSystem Component
- [x] Select PlayerController in Hierarchy
- [x] In Inspector, click **Add Component**
- [x] Search: **"UserTrackingSystem"**
- [x] Click to add component
- [x] Component appears in Inspector

### 8c. Configure UserTrackingSystem
In Inspector, set values:
- [x] **User ID**: `0` (for local player)
- [ ] **Danger Zone Distance**: `0.5` (meters)
- [x] **Show Debug Visualization**: Check ✅ (for testing)

### 8d. Add Visual Representation
- [x] Right-click PlayerController in Hierarchy
- [x] Select **3D Object > Capsule**
- [x] Rename capsule to **"HeadRepresentation"**
- [x] In Transform component, set:
  - [x] Position: (0, 1.8, 0) - standing head height
  - [x] Scale: (0.2, 0.15, 0.2) - head-sized

### 8e. Create Prefab
- [x] Ensure `Assets/Prefabs/Player/` folder exists (create if needed)
- [x] Drag **PlayerController** from Hierarchy into `Assets/Prefabs/Player/` folder
- [x] Wait for prefab to be created
- [x] PlayerController in Hierarchy should turn blue (indicating prefab)

### 8f. Delete from Scene
- [x] Right-click PlayerController in Hierarchy
- [x] Click **Delete**
- [x] (We'll spawn this via network in Phase 2)

### 8g. Save Scene
- [x] Press **Ctrl+S** to save

**Validation**: PlayerController prefab exists in Project window ✅

---

## 📸 Step 9: Document Your Work (5 min)

### 9a. Take Screenshots
- [x] Enter Play mode
- [x] Take screenshot of Game view showing VR perspective
- [x] Exit Play mode
- [x] Take screenshot of Hierarchy showing OVRCameraRig and OVRManager
- [x] Save screenshots to `Documentation/Screenshots/` (create folder if needed)

### 9b. Note Any Issues
Create a file `ISSUES_ENCOUNTERED.md` if you had any problems:
- [x] Document what went wrong
- [x] Document how you solved it
- [x] Note any warnings that persist

### 9c. Update Progress
In `PHASE1_PROGRESS.md`, update the checklist:
- [x] Mark all completed tasks with ✅
- [x] Update "Current Phase Status" percentage
- [x] Add notes in "Recent Changes" section

---

## 🎉 Phase 1 Completion Verification

**Go through this final checklist to confirm Phase 1 is complete:**

### Scene Setup ✅
- [x] OVRCameraRig exists and configured
- [x] OVRManager exists with Quest 3 settings
- [x] No Main Camera in scene
- [x] Scene saved successfully

### Input Setup ✅
- [x] Controller building block added
- [x] FromOVRControllerDataSource components present
- [x] (Optional) Hand tracking added

### Testing ✅
- [x] Simulator test completed successfully
- [ ] WASD movement works
- [x] Mouse look works
- [x] No errors in Console
- [ ] 60+ fps in editor

### Assets ✅
- [x] PlayerController prefab created
- [x] UserTrackingSystem component configured
- [x] Visual head representation added

### Documentation ✅
- [ ] Screenshots taken
- [ ] Progress updated
- [ ] Issues documented (if any)

---

## ✅ PHASE 1 COMPLETE!

**If all items above are checked**, Phase 1 is officially complete! 🎉

**Next Steps**:
1. Read `NEXT_STEPS.md` for Phase 2 preparation
2. Plan hardware acquisition (2-3 Quest 3 headsets)
3. Set up local network for multi-device testing
4. Begin Phase 2 implementation planning

---

## 🐛 TROUBLESHOOTING

### "Multi-User VR" menu not visible
**Fix**:
1. Check Console for script compilation errors
2. Right-click `Assets/Scripts/` > **Reimport**
3. Restart Unity Editor
4. Verify scripts are in correct folders

### Building Blocks window empty
**Fix**:
1. Wait for Meta XR SDK import to complete (check progress bar)
2. Go to **Window > Package Manager**
3. Find "Meta XR All-in-One SDK" and click **Reimport**
4. Restart Unity Editor

### "OVRCameraRig not found" error
**Fix**:
1. Run setup script again: **Multi-User VR > Setup > Phase 1 - Configure VR Scene**
2. Check if Meta XR SDK is properly installed (Package Manager)

### Black screen in Play mode
**Fix**:
1. Check if OVRCameraRig is enabled in Hierarchy
2. Ensure no other cameras are active
3. Check Lighting settings (Window > Rendering > Lighting)
4. Add a light to scene: **GameObject > Light > Directional Light**

### Validation fails with "Unity Netcode not found"
**Fix**:
1. Go to **Window > Package Manager**
2. Search "Netcode for GameObjects"
3. If missing, click **+ > Add package from git URL...**
4. Enter: `com.unity.netcode.gameobjects`
5. Wait for import to complete

### Low FPS in simulator (<30 fps)
**Fix**:
1. Check if other applications are running
2. Lower resolution in Game view
3. Disable high-quality graphics in Project Settings
4. This is normal for editor - Quest 3 device will be 90fps

---

## 📊 Time Tracking

**Estimated Total Time**: 35-45 minutes

| Step | Estimated | Actual | Notes |
|------|-----------|--------|-------|
| Launch Unity | 5 min | ___ min | |
| Open Scene | 1 min | ___ min | |
| Run Setup | 5 min | ___ min | |
| Validate | 2 min | ___ min | |
| Save Scene | 1 min | ___ min | |
| Add Input | 5 min | ___ min | |
| Test Simulator | 10 min | ___ min | |
| Create Prefab | 10 min | ___ min | |
| Document | 5 min | ___ min | |
| **TOTAL** | **35 min** | **___ min** | |

**Actual completion time**: _________ (fill in when done)

---

**Good luck! You've got this!** 🚀

Remember: If you get stuck, check the troubleshooting section or create an issue in the project repo.

