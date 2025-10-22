# Co-Located Multi-User VR - Phase 1 Implementation Guide

## 🚀 Quick Start

### 1. Initial Setup (Completed ✅)
- ✅ Unity Netcode for GameObjects installed (`com.unity.netcode.gameobjects v2.3.0`)
- ✅ Meta XR All-in-One SDK v78.0.0 installed
- ✅ Project structure created (`Assets/Scripts/VR`, `Networking`, `Safety`)
- ✅ Core scripts created: `UserTrackingSystem.cs`, `VRNetworkManager.cs`

### 2. Configure VR Scene (Next Step)

**Option A: Automated Setup (Recommended)**
1. Open Unity Editor
2. Go to menu: **Multi-User VR > Setup > Phase 1 - Configure VR Scene**
3. This will automatically:
   - Remove default Main Camera
   - Create and configure OVRCameraRig
   - Create and configure OVRManager
   - Set up Quest 3 optimizations

**Option B: Manual Setup**
1. Delete default "Main Camera" from scene
2. Create empty GameObject named "OVRCameraRig"
3. Add `OVRCameraRig` component to it
4. Create empty GameObject named "OVRManager"
5. Add `OVRManager` component to it
6. Configure settings (see `VRSceneSetup.cs` for recommended values)

### 3. Validate Setup
Run menu command: **Multi-User VR > Setup > Validate Phase 1 Setup**

This checks:
- ✅ OVRCameraRig exists and is configured
- ✅ OVRManager exists with correct settings
- ✅ Unity Netcode package is available
- ✅ No conflicting Main Camera

### 4. Test in Meta XR Simulator
1. Go to **Window > Meta XR > Test in Simulator**
2. Click Play in Unity Editor
3. Use keyboard/mouse to simulate VR movement:
   - WASD - Move
   - Mouse - Look around
   - Space - Recenter

## 📁 Project Structure

```
Assets/
├── Scripts/
│   ├── Setup/
│   │   └── VRSceneSetup.cs         # Automated scene configuration
│   ├── VR/
│   │   └── UserTrackingSystem.cs   # Track player position for safety
│   ├── Networking/
│   │   └── VRNetworkManager.cs     # Network setup (Phase 2)
│   └── Safety/
│       └── (Phase 3 collision prevention)
├── Prefabs/
│   └── Player/
│       └── (Player prefab will be created here)
└── Scenes/
    └── SampleScene.unity           # Main VR scene
```

## 🎯 Phase 1 Completion Checklist

### Core VR Setup
- [ ] Task 1.1: ✅ Install Unity Netcode for GameObjects
- [ ] Task 1.2: ⏳ Configure SampleScene with OVRCameraRig
- [ ] Task 1.3: ⏳ Add and configure OVRManager
- [ ] Task 1.4: ⏳ Test single-user VR in Meta XR Simulator

### Input & Tracking  
- [ ] Task 1.5: ⏳ Add FromOVRControllerDataSource (via Building Blocks)
- [ ] Task 1.6: ⏳ Add FromOVRHandDataSource (via Building Blocks)
- [ ] Task 1.7: ✅ Create UserTrackingSystem script

### Player Setup
- [ ] Task 1.8: ⏳ Create PlayerController prefab
- [ ] Task 1.9: ⏳ Create simple avatar representation

## 🛠️ Next Steps After Phase 1

### Phase 2: Networking & Colocation (Weeks 3-4)
1. Implement `VRNetworkManager.cs` with Unity Netcode
2. Add Meta Platform SDK components:
   - `LocalMatchmaking` - Auto-discover nearby Quest devices
   - `ColocationController` - Manage shared physical space
   - `SharedSpatialAnchorCore` - Sync coordinate systems
3. Create networked player prefab
4. Test with 2-3 physical Quest 3 headsets

### Phase 3: Safety Systems (Weeks 5-6)
1. Add `MRUK` component for room understanding
2. Implement `CollisionPreventionSystem.cs`
3. Integrate `RoomGuardian` for boundary enforcement
4. Add proximity warnings (visual/haptic)
5. Implement emergency passthrough activation

## 📚 Key Components Reference

### Phase 1 - VR Foundation
- `OVRCameraRig` - Main VR camera with tracked controllers
- `OVRManager` - Core VR system manager
- `UserTrackingSystem` - Custom script for position tracking

### Phase 2 - Networking (Not Yet Implemented)
- `Unity.Netcode.NetworkManager` - Network game objects
- `Meta.XR.MultiplayerBlocks.Shared.LocalMatchmaking` - Device discovery
- `Meta.XR.MultiplayerBlocks.Shared.ColocationController` - Physical space sync
- `SharedSpatialAnchorCoreBuildingBlock` - Shared coordinate anchor

### Phase 3 - Safety (Not Yet Implemented)
- `Meta.XR.MRUtilityKit.MRUK` - Room understanding
- `Meta.XR.MRUtilityKit.MRUKRoom` - Room boundaries
- `Meta.XR.MRUtilityKit.RoomGuardian` - Safety enforcement
- `OVRPassthroughLayer` - Emergency see-through mode

## 🔍 Troubleshooting

### "OVRCameraRig not rendering in Scene view"
- This is normal - OVR components only fully activate in Play mode
- Use Meta XR Simulator to test

### "Unity Netcode package not detected"
- Wait for Unity to finish importing packages
- Check Package Manager (Window > Package Manager)
- Verify `com.unity.netcode.gameobjects` is in Packages/manifest.json

### "Meta XR Building Blocks window is empty"
- Ensure Meta XR SDK is fully imported
- Check Window > Meta XR > Tools > Building Blocks
- May need to reimport Meta XR SDK

### "Cannot test on Quest 3 device"
- Phase 1 testing uses Meta XR Simulator in Editor
- Physical device testing starts in Phase 2
- Ensure Android Build Support is installed for later phases

## 📖 Documentation Links

- [Meta XR SDK Documentation](https://developer.oculus.com/documentation/unity/)
- [Unity Netcode for GameObjects](https://docs-multiplayer.unity3d.com/netcode/current/about/)
- [Meta Colocation Guide](https://developer.oculus.com/documentation/unity/unity-colocation/)
- [MVP Requirements](../MVP_DEVELOPMENT_PROMPT.md)

## 🎮 Using Meta XR Building Blocks

Building Blocks are pre-configured prefabs for common VR features:

**Recommended for Phase 1:**
1. **Window > Meta XR > Tools > Building Blocks**
2. Add "Interaction SDK > Controller" for controller input
3. Add "Interaction SDK > Hand Tracking" for hand tracking (optional)

**For Phase 2:**
- "Spatial Anchor Core" - Shared coordinate system
- "Local Matchmaking" - Connect headsets
- "Colocation" - Physical space alignment

**For Phase 3:**
- "Room Mesh Controller" - Visualize room boundaries
- "Passthrough Projection Surface" - MR safety features

## ⚡ Quick Commands

### Unity Menu Commands
- `Multi-User VR > Setup > Phase 1 - Configure VR Scene` - Auto-configure VR
- `Multi-User VR > Setup > Phase 1 - Add Floor Plane` - Add test floor
- `Multi-User VR > Setup > Validate Phase 1 Setup` - Check configuration

### Unity Windows
- `Window > Meta XR > Tools > Building Blocks` - Add pre-built components
- `Window > Meta XR > Test in Simulator` - VR testing in editor

## 🔬 Testing Strategy

### Phase 1 Testing (Current)
- **Environment**: Unity Editor with Meta XR Simulator
- **Goal**: Validate single-user VR experience
- **Test Cases**:
  - Camera rig tracks head movement
  - Controllers respond to input
  - No compilation errors
  - 90fps in editor

### Phase 2 Testing (Future)
- **Environment**: 2-3 physical Quest 3 headsets
- **Goal**: Validate networking and colocation
- **Test Cases**:
  - Devices discover each other
  - Shared spatial anchor alignment
  - Avatar synchronization
  - <50ms network latency

### Phase 3 Testing (Future)
- **Environment**: 3 users in same physical room
- **Goal**: Validate safety systems
- **Test Cases**:
  - Proximity warnings activate
  - Passthrough triggers correctly
  - No physical collisions
  - Room boundaries detected

---

**Status**: Phase 1 implementation in progress
**Next Milestone**: Complete VR scene setup and test in simulator
**Target Date**: End of Week 2
