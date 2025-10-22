# 🎮 Multi-User VR - Quick Command Reference

## Unity Editor Menu Commands

### Phase 1 Setup Commands
```
Multi-User VR > Setup > Phase 1 - Configure VR Scene
  └─ Auto-configures OVRCameraRig and OVRManager for Quest 3

Multi-User VR > Setup > Phase 1 - Add Floor Plane  
  └─ Adds simple test floor to scene

Multi-User VR > Setup > Validate Phase 1 Setup
  └─ Checks if VR scene is properly configured
```

### Meta XR Tools
```
Window > Meta XR > Tools > Building Blocks
  └─ Add pre-built VR components (controllers, hand tracking, etc.)

Window > Meta XR > Test in Simulator
  └─ Test VR experience in Unity Editor without device
```

## 🚀 Phase 1 Quick Start (5 Minutes)

### Step-by-Step Setup:
```bash
# 1. Open Unity Editor and load SampleScene.unity

# 2. Run automated VR setup
Menu: Multi-User VR > Setup > Phase 1 - Configure VR Scene

# 3. Validate setup
Menu: Multi-User VR > Setup > Validate Phase 1 Setup
Expected output: "✅ Phase 1 Setup Valid"

# 4. Add floor for testing
Menu: Multi-User VR > Setup > Phase 1 - Add Floor Plane

# 5. Add controller input (Building Blocks)
Menu: Window > Meta XR > Tools > Building Blocks
  └─ Add: Interaction SDK > Controller

# 6. Test in simulator
Menu: Window > Meta XR > Test in Simulator
Press Play ▶️

# 7. Control VR camera in simulator:
WASD - Move around
Mouse - Look/rotate view
Space - Recenter tracking
```

## 📁 Important File Locations

### Scripts
```
Assets/Scripts/Setup/VRSceneSetup.cs          # Automated configuration
Assets/Scripts/VR/UserTrackingSystem.cs       # Position tracking
Assets/Scripts/Networking/VRNetworkManager.cs # Networking (Phase 2)
Assets/Scripts/README.md                      # Full documentation
```

### Documentation
```
PHASE1_PROGRESS.md                  # Implementation status report
MVP_DEVELOPMENT_PROMPT.md           # Full MVP requirements
.github/copilot-instructions.md     # AI assistant context
```

### Configuration
```
Packages/manifest.json              # Package dependencies
ProjectSettings/                    # Unity project settings
```

## 🔍 Validation Checklist

### Before Testing in Simulator:
```
☐ OVRCameraRig exists in scene
☐ OVRManager exists in scene  
☐ No default "Main Camera" present
☐ Unity Netcode package imported
☐ No compilation errors in Console
```

### During Simulator Test:
```
☐ Scene renders in VR view
☐ WASD movement works
☐ Mouse look controls head rotation
☐ Console shows no errors
☐ Framerate is 60+ fps (check Stats window)
```

## 🛠️ Troubleshooting Quick Fixes

### Issue: "OVRCameraRig not found"
```bash
Solution: Run setup script again
Menu: Multi-User VR > Setup > Phase 1 - Configure VR Scene
```

### Issue: "Unity Netcode not detected"
```bash
Solution: Check Package Manager
Menu: Window > Package Manager
Search: "Netcode for GameObjects"
If missing: Add package from Unity Registry
```

### Issue: "Meta XR Simulator window is empty"
```bash
Solution 1: Reimport Meta XR SDK
Right-click Packages/Meta XR All-in-One SDK > Reimport

Solution 2: Check XR Plugin Management
Menu: Edit > Project Settings > XR Plugin Management
Ensure: OpenXR + Meta XR Plugin are enabled
```

### Issue: "Black screen in Play mode"
```bash
Solution: Check Main Camera conflict
Run: Multi-User VR > Setup > Validate Phase 1 Setup
If error about Main Camera: Delete it manually or re-run setup
```

### Issue: "Compilation errors"
```bash
Solution: Wait for package import to complete
Check: Console window for specific errors
If persists: Reimport all scripts (Assets > Reimport All)
```

## 🎯 Component Quick Reference

### Phase 1 Essential Components
```csharp
// VR Core (Add to scene)
OVRCameraRig        // Main VR camera rig
OVRManager          // VR system manager

// Input Sources (Add via Building Blocks)
FromOVRControllerDataSource  // Controller input
FromOVRHandDataSource        // Hand tracking input

// Custom Scripts (Attach to GameObjects)
UserTrackingSystem  // Track player position (attach to OVRCameraRig)
```

### Phase 2 Components (Not Yet Used)
```csharp
// Networking (Phase 2)
Unity.Netcode.NetworkManager     // Network management
LocalMatchmaking                 // Device discovery
ColocationController             // Physical space sync
SharedSpatialAnchorCore         // Shared coordinate system

// Custom Scripts (Phase 2)
VRNetworkManager    // Custom network manager (framework ready)
```

### Phase 3 Components (Not Yet Used)
```csharp
// Safety & Room Understanding (Phase 3)
MRUK                // Mixed Reality Utility Kit
MRUKRoom            // Room boundaries
RoomGuardian        // Safety enforcement
OVRPassthroughLayer // Emergency see-through
```

## 📊 Performance Monitoring

### In Unity Editor:
```
Window > Analysis > Profiler
  └─ Check CPU, GPU, Rendering stats

Game View > Stats
  └─ Monitor FPS, drawcalls, triangles

Console > Show timestamp
  └─ Check for performance warnings
```

### Target Performance (Quest 3):
```
✅ FPS: 90+ (72 minimum acceptable)
✅ Drawcalls: <500
✅ Triangles: <500k
✅ Memory: <3GB
```

## 🔗 External Resources

### Documentation Links:
```
Meta XR SDK Docs:
https://developer.oculus.com/documentation/unity/

Unity Netcode Docs:
https://docs-multiplayer.unity3d.com/netcode/current/about/

Meta Colocation Guide:
https://developer.oculus.com/documentation/unity/unity-colocation/

Meta Building Blocks:
https://developer.oculus.com/documentation/unity/unity-buildingblocks/
```

### Support:
```
Meta Developer Forums:
https://communityforums.atmeta.com/t5/VR-Development/bd-p/vr-development

Unity Forums:
https://forum.unity.com/forums/xr.118/
```

## 🎨 Keyboard Shortcuts (Meta XR Simulator)

### Movement:
```
W/A/S/D     - Move forward/left/back/right
Q/E         - Move down/up
Shift       - Move faster
Ctrl        - Move slower
```

### Camera:
```
Mouse       - Look around (hold right-click)
Scroll      - Adjust movement speed
Space       - Recenter tracking
```

### Controllers:
```
Left Ctrl   - Grip button (left)
Right Ctrl  - Grip button (right)
Left Alt    - Trigger (left)
Right Alt   - Trigger (right)
```

---

**Last Updated**: October 22, 2025  
**Project**: Co-Located Multi-User VR MVP  
**Phase**: Phase 1 - Foundation Setup
