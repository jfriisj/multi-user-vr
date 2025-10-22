# Co-Located Multi-User VR System MVP - Development Prompt

> **📊 Project Status**: Phase 1 In Progress (44% Complete) | **🎯 Next**: Unity Editor Scene Configuration | **✅ Netcode Installed** | **📅 Updated**: Oct 22, 2025

## Project Overview
Develop a **Minimum Viable Product (MVP)** for an affordable co-located multi-user VR system that enables **3 Meta Quest 3 users to share the same physical room** while collaborating in a virtual environment. This addresses the unique challenges of co-located VR experiences, unlike traditional remote multi-user VR platforms.

### 📦 Installed SDK Packages (v78.0.0)

The project includes **Meta XR All-in-One SDK v78.0.0**, which bundles:

| Package | Description | Key Use for MVP |
|---------|-------------|-----------------|
| **Meta XR Core SDK** | Foundation VR features, tracking, passthrough | Essential - Camera rig, tracking, scene API |
| **Meta XR Interaction SDK** | Hand/controller input, grabbing, locomotion | Essential - User interaction system |
| **Meta XR Platform SDK** | Matchmaking, social features, colocation | **CRITICAL** - LocalMatchmaking, Colocation |
| **Meta MR Utility Kit** | Room understanding, spatial anchors helper | **CRITICAL** - Room boundaries, safety |
| **Meta XR Haptics SDK** | Advanced haptic feedback | Optional - Future enhancement |
| **Meta XR Audio SDK** | Spatial audio, acoustics | Optional - Audio enhancement |
| **Meta XR Simulator** | Test in editor without device | Development tool |

**Documentation**: https://developer.oculus.com/documentation/unity/

### 📥 ✅ Additional Packages Installed

| Package | Version | Purpose | Status |
|---------|---------|---------|--------|
| **Unity Netcode for GameObjects** | v2.6.0 | Network synchronization layer | ✅ **INSTALLED** |
| `com.unity.netcode.gameobjects` | 2.6.0 | Sync transforms, objects, RPCs | Ready for Phase 2 |

**Note**: Meta Platform SDK provides matchmaking and colocation, but Unity Netcode handles the actual gameplay networking (position sync, object states, etc.)

## 🎯 MVP Core Requirements

### Primary Goal
Create a functional prototype that demonstrates **safe, stable multi-user VR collaboration** in a shared physical space with basic networking synchronization.

### Key Features to Implement

#### 1. **Basic Multi-Headset Setup** ⚡ CRITICAL
- [ ] Support for exactly **3 Meta Quest 3 headsets** in the same room
- [ ] Individual headset identification and tracking
- [ ] Basic avatar representation for each user
- [ ] Simple virtual environment (basic room/space)

#### 2. **Collision Prevention System** 🛡️ SAFETY FIRST
- [ ] **Real-time position tracking** of all 3 users (via `OVRCameraRig` positions)
- [ ] **Visual warning system** when users get too close (haptic/audio alerts)
- [ ] **Guardian boundary awareness** using `RoomGuardian` + `MRUK`
- [ ] **Shared room mesh** via `MRUKRoom` for collision detection
- [ ] **Emergency passthrough mode** using `OVRPassthroughLayer`
- [ ] **Emergency stop mechanism** if collision risk is detected

#### 3. **Basic Networking & Synchronization** 🌐
- [ ] **Local network discovery** using `LocalMatchmaking` (Meta Platform SDK)
- [ ] **Physical space alignment** via `ColocationController` + `SharedSpatialAnchorCore`
- [ ] **Real-time position/rotation sync** between headsets (Unity Netcode)
- [ ] **Avatar movement synchronization** with `PlayerNameTagSpawner`
- [ ] **Basic hand tracking sync** (controller positions via `FromOVRControllerDataSource`)
- [ ] **Connection status indicators** via `ColocationSessionEventHandler`
- [ ] **Shared object ownership** with `TransferOwnershipOnSelect`

#### 4. **Shared Virtual Environment** 🏛️
- [ ] **Simple collaborative space** (e.g., meeting room, training area)
- [ ] **Shared virtual objects** that all users can see
- [ ] **Basic interaction system** (point, select, grab)
- [ ] **Synchronized object states** across all users

#### 5. **Safety & User Awareness** ⚠️
- [ ] **Physical space mapping** integration
- [ ] **User proximity indicators** in VR
- [ ] **Boundary visualization** for shared space limits
- [ ] **Emergency exit/pause system**

## 🔬 Research & Testing Requirements

### Technical Investigation
1. **Tracking Interference Testing**
   - Test Meta Quest 3's inside-out tracking with multiple headsets
   - Document any signal interference or tracking degradation
   - Measure minimum safe distances between users
   - Test in different lighting conditions

2. **Network Performance Analysis**
   - Measure latency between headsets
   - Test bandwidth requirements for smooth sync
   - Identify optimal network settings
   - Document connection stability over time

3. **Physical Space Requirements**
   - Define minimum room size requirements
   - Test different room layouts and configurations
   - Document optimal user positioning strategies

### Use Case Validation
- **Training Scenarios**: Simple collaborative training exercises
- **Educational Applications**: Basic learning activities in shared VR
- **Team Building**: Simple cooperative tasks
- **Meeting/Presentation**: Virtual meeting space functionality

## 🛠️ Technical Implementation Plan

### Meta XR All-in-One SDK (v78.0.0) - Available Tools & Components

The project now includes **Meta XR All-in-One SDK v78.0.0**, which provides comprehensive tools for Quest 3 development:

#### **Meta XR Core SDK** - Foundation Components
- **OVRCameraRig**: Main camera rig for VR experiences
- **OVRManager**: Core VR manager handling tracking, rendering, and system features
- **OVRPassthroughLayer**: Mixed reality passthrough for real-world visibility
- **OVRSceneManager/OVRSceneAnchor**: Scene understanding and spatial anchors
- **OVRSpatialAnchor**: Persistent spatial anchors for multi-session experiences
- **OVRHandTracking**: Hand tracking support alongside controllers
- **OVRBody/OVRFace/OVREyeGaze**: Advanced body, face, and eye tracking

#### **Meta XR Interaction SDK** - User Input & Interaction
- **Hand Tracking & Controllers**:
  - `FromOVRHandDataSource`, `FromOVRControllerDataSource`: Input data sources
  - `HandGrabInteractor/Interactable`: Natural hand-based grabbing
  - `DistanceGrabInteractor/Interactable`: Grab objects at distance
  - `RayInteractor/Interactable`: Ray-based pointing and selection
  - `PokeInteractor/Interactable`: Direct touch interactions
  
- **Locomotion System**:
  - `TeleportInteractor/Interactable`: Teleportation movement
  - `LocomotionTurnerInteractor/Interactable`: Smooth/snap turning
  - `PlayerLocomotor`: Complete locomotion management
  
- **UI & Canvas Interaction**:
  - `PointableCanvas`: Make Unity UI interactable in VR
  - `OVRRaycaster`: VR-ready raycasting for UI

#### **Meta MR Utility Kit** - Spatial Understanding
- **MRUK (Mixed Reality Utility Kit)**: Main manager for scene understanding
- **MRUKRoom/MRUKAnchor**: Room and anchor management
- **AnchorPrefabSpawner**: Automatically spawn objects at detected surfaces
- **RoomGuardian**: Safety boundary management
- **SceneDebugger**: Visualize detected room geometry
- **EffectMesh**: Apply visual effects to real-world surfaces

#### **Meta XR Platform SDK** - Social & Networking
- **Matchmaking & Social**:
  - `LocalMatchmaking`: Local network matchmaking
  - `CustomMatchmaking`: Custom matchmaking logic
  - `FriendsMatchmaking`: Friend-based matchmaking
  - `PlayerNameTagSpawner`: Display player names in VR
  
- **Colocation Features**:
  - `ColocationController`: Share physical space between users
  - `SharedSpatialAnchorCore`: Synchronized spatial anchors
  - `AlignCameraToAnchor`: Align users to shared coordinate system

#### **Meta XR Haptics SDK** - Advanced Haptics
- `HapticSource`: Play custom haptic clips from Meta Haptics Studio
- Advanced haptic feedback beyond basic controller rumble

#### **Meta XR Audio SDK** - Spatial Audio
- `MetaXRAudioSource`: 3D spatial audio sources
- `MetaXRAcousticGeometry`: Room acoustics simulation
- `MetaXRAcousticMaterial`: Surface material properties for acoustics

#### **Building Blocks** - Pre-built Systems
- `SpatialAnchorCoreBuildingBlock`: Quick spatial anchor setup
- `PassthroughProjectionSurfaceBuildingBlock`: Mixed reality projection
- `RoomMeshController`: Room mesh visualization and interaction

### Phase 1: Foundation Setup (Week 1-2) 🟡
**Status**: In Progress | **Progress**: 4/9 tasks completed (44%)

#### Core VR Setup
- [x] **Task 1.1**: ✅ Install Unity Netcode for GameObjects package (`com.unity.netcode.gameobjects` v2.6.0)
- [x] **Task 1.2**: ✅ Configure `SampleScene.unity` with `OVRCameraRig` prefab (automated script created)
- [x] **Task 1.3**: ✅ Add and configure `OVRManager` component to scene (automated script created)
- [ ] **Task 1.4**: ⏳ Test single-user VR in Meta XR Simulator (awaiting Unity Editor execution)

#### Input & Tracking
- [ ] **Task 1.5**: ⏳ Implement `FromOVRHandDataSource` for hand tracking (via Building Blocks)
- [ ] **Task 1.6**: ⏳ Implement `FromOVRControllerDataSource` for controller input (via Building Blocks)
- [x] **Task 1.7**: ✅ Create `UserTrackingSystem` script (track OVRCameraRig position/rotation)

#### Player Setup
- [ ] **Task 1.8**: ⏳ Create basic `PlayerController` prefab with OVRCameraRig
- [ ] **Task 1.9**: ⏳ Create simple avatar representation (capsule + head tracker)

**📂 Created Assets**:
- ✅ `Assets/Scripts/Setup/VRSceneSetup.cs` - Automated VR scene configuration
- ✅ `Assets/Scripts/VR/UserTrackingSystem.cs` - Position tracking for safety
- ✅ `Assets/Scripts/Networking/VRNetworkManager.cs` - Network manager framework (Phase 2)
- ✅ `Assets/Scripts/README.md` - Complete implementation guide
- ✅ `PHASE1_PROGRESS.md` - Detailed progress tracking
- ✅ `QUICK_REFERENCE.md` - Command reference and troubleshooting

**🚀 Next Steps**:
1. Run automated setup in Unity Editor: `Multi-User VR > Setup > Phase 1 - Configure VR Scene`
2. Validate setup: `Multi-User VR > Setup > Validate Phase 1 Setup`
3. Add input sources via Building Blocks
4. Test in Meta XR Simulator

**Phase 1 Completion Criteria**: Single user can wear Quest 3, see VR environment, and interact with controllers/hands in Unity Editor simulator.

---

### Phase 2: Networking & Sync (Week 3-4) ❌
**Status**: Not Started | **Progress**: 0/11 tasks completed

#### Network Foundation
- [ ] **Task 2.1**: Set up Unity Netcode `NetworkManager` in scene
- [ ] **Task 2.2**: Configure network transport for local network (UnityTransport)
- [ ] **Task 2.3**: Create `VRNetworkManager` script extending NetworkManager

#### Meta Colocation Integration
- [ ] **Task 2.4**: Add `LocalMatchmaking` component for device discovery
- [ ] **Task 2.5**: Implement `ColocationController` for shared space management
- [ ] **Task 2.6**: Set up `SharedSpatialAnchorCore` workflow (host creates anchor)
- [ ] **Task 2.7**: Implement `AlignCameraToAnchor` for all clients
- [ ] **Task 2.8**: Add `ColocationSessionEventHandler` for connection status

#### Avatar Synchronization
- [ ] **Task 2.9**: Make PlayerController a NetworkObject with NetworkTransform
- [ ] **Task 2.10**: Synchronize head position/rotation across network
- [ ] **Task 2.11**: Add `PlayerNameTagSpawner` to display user names in VR

**Phase 2 Completion Criteria**: 2-3 Quest 3 headsets can discover each other, align to shared physical space, and see each other's avatars moving in real-time.

---

### Phase 3: Safety & Polish (Week 5-6) ❌
**Status**: Not Started | **Progress**: 0/10 tasks completed

#### Room Understanding & Boundaries
- [ ] **Task 3.1**: Add `MRUK` component to scene for room understanding
- [ ] **Task 3.2**: Implement `MRUKRoom` to detect room boundaries
- [ ] **Task 3.3**: Integrate `RoomGuardian` for safety boundary visualization
- [ ] **Task 3.4**: Test room boundary detection on physical Quest 3 devices

#### Collision Prevention System
- [ ] **Task 3.5**: Create `CollisionPreventionSystem` script
- [ ] **Task 3.6**: Implement distance calculation between all user positions
- [ ] **Task 3.7**: Add visual warnings (proximity indicators) using `EffectMesh`
- [ ] **Task 3.8**: Implement haptic feedback when users get too close
- [ ] **Task 3.9**: Add `OVRPassthroughLayer` emergency mode trigger

#### Polish & Optimization
- [ ] **Task 3.10**: Performance optimization (target 90fps on Quest 3)

**Phase 3 Completion Criteria**: System actively prevents collisions with visual/haptic warnings, can trigger passthrough mode, and maintains 90fps with 3 users.

---

### Phase 4: Validation & Documentation (Week 7-8) ❌
**Status**: Not Started | **Progress**: 0/8 tasks completed

#### Testing & Validation
- [ ] **Task 4.1**: Conduct 30-minute session test with 3 users
- [ ] **Task 4.2**: Measure and document tracking accuracy (<2cm target)
- [ ] **Task 4.3**: Measure network latency between headsets (<50ms target)
- [ ] **Task 4.4**: Validate `SharedSpatialAnchorCore` alignment accuracy
- [ ] **Task 4.5**: Test safety features (collision prevention, passthrough)

#### Use Case Demonstrations
- [ ] **Task 4.6**: Create simple collaborative task demo (move shared objects)
- [ ] **Task 4.7**: Record 5-minute demo video for stakeholders

#### Documentation
- [ ] **Task 4.8**: Write setup guide and technical documentation

**Phase 4 Completion Criteria**: MVP meets all success metrics, documented, and demo-ready for stakeholders.

## 🎮 MVP User Experience Flow

### Setup Process
1. **Individual Calibration**: Each user sets up their play space
2. **Network Connection**: All headsets join the same session
3. **Safety Briefing**: Virtual safety tutorial for shared space
4. **Spawn Positioning**: Users positioned safely in virtual space

### Core Interaction Loop
1. **Awareness**: Users see each other's avatars and boundaries
2. **Collaboration**: Simple shared tasks (move objects, point, discuss)
3. **Safety**: Continuous monitoring and warning system
4. **Exit**: Clean disconnection and session end

## 📊 Success Metrics

### Technical Performance
- ✅ **Tracking Accuracy**: <2cm position error per headset
- ✅ **Network Latency**: <50ms between headsets
- ✅ **Frame Rate**: Maintain 90fps on all headsets
- ✅ **Safety Response**: <100ms collision warning system

### User Experience
- ✅ **Setup Time**: <5 minutes from start to collaborative experience
- ✅ **Safety Incidents**: Zero physical collisions during testing
- ✅ **Synchronization**: Real-time movement with no noticeable lag
- ✅ **Stability**: 30-minute sessions without connection drops

### Business Validation
- ✅ **Cost Comparison**: Document cost vs. existing commercial solutions
- ✅ **Use Case Fit**: Validate 3+ practical applications
- ✅ **Scalability**: Assess potential for expanding to 4+ users
- ✅ **Market Readiness**: Basic demo-ready for stakeholders

## 🚧 Development Constraints & Considerations

### Hardware Limitations
- **Meta Quest 3 only** - leverage specific features and limitations
- **Battery life** - optimize for 1-2 hour sessions
- **Processing power** - maintain performance with 3 concurrent streams
- **Wi-Fi dependency** - ensure robust local network setup

### Safety-First Development
- **Physical safety is paramount** - never compromise on collision prevention
- **Graceful degradation** - system should fail safely
- **Clear user communication** - always inform users of system status
- **Testing protocols** - establish comprehensive safety testing procedures

### Scope Limitations (Out of MVP)
- ❌ Advanced haptic feedback systems (Meta Haptics SDK available but not MVP priority)
- ❌ Complex AI-driven features  
- ❌ Cloud networking capabilities (use `LocalMatchmaking` only)
- ❌ Advanced avatar customization (basic representation only)
- ❌ Voice chat integration (use Meta Platform SDK's built-in VoIP features)
- ❌ Eye tracking (`OVREyeGaze` available but not MVP priority)
- ❌ Advanced body tracking (`OVRBody` available but not MVP priority)
- ❌ Face tracking (`OVRFace` available but not MVP priority)

## 🎯 Target Customer Validation

### Primary Testing Scenarios
1. **Corporate Training**: Emergency response simulation
2. **Educational Lab**: Collaborative science experiment
3. **Team Building**: Simple puzzle-solving activities
4. **Presentation Space**: Virtual meeting and review sessions

### Key Questions to Answer
- Does co-location provide clear advantages over remote VR?
- What specific use cases benefit most from shared physical space?
- How does cost compare to existing enterprise solutions?
- What are the practical deployment challenges?

## 📋 Deliverables

### Technical Deliverables
- [ ] **Working Unity Project** with full source code
- [ ] **Build for Meta Quest 3** (APK files)
- [ ] **Setup and Installation Guide**
- [ ] **Network Configuration Documentation**
- [ ] **Safety Protocols Document**

### Research Deliverables  
- [ ] **Tracking Interference Report** with test results
- [ ] **Performance Benchmarks** across different scenarios
- [ ] **Use Case Analysis** with recommendations
- [ ] **Cost-Benefit Analysis** vs. existing solutions
- [ ] **Technical Limitations Documentation**

### Demo Materials
- [ ] **5-minute Demo Scenario** for stakeholders
- [ ] **User Training Materials** for safe operation
- [ ] **Troubleshooting Guide** for common issues
- [ ] **Future Development Roadmap**

## 🔄 Iterative Development Approach

### Weekly Milestones
- **Week 1**: Single headset VR environment setup
- **Week 2**: Basic networking between 2 headsets
- **Week 3**: 3-headset synchronization working
- **Week 4**: Collision prevention system implemented
- **Week 5**: Safety systems and polish features
- **Week 6**: Use case testing and validation
- **Week 7**: Performance optimization and debugging
- **Week 8**: Documentation and final demonstrations

### Risk Mitigation
- **Tracking Issues**: Have backup positioning systems ready
- **Network Problems**: Test with different network configurations
- **Safety Concerns**: Implement multiple backup safety systems
- **Hardware Failures**: Plan for equipment backup and redundancy

---

## 🎁 Meta XR SDK Integration Guide

### Key SDK Components for MVP Development

#### **Essential for MVP (Must Use)**
1. **OVRCameraRig + OVRManager**: Core VR tracking and rendering
2. **FromOVRHandDataSource / FromOVRControllerDataSource**: Input handling
3. **LocalMatchmaking**: Discover and connect 3 Quest 3s on same network
4. **ColocationController + SharedSpatialAnchorCore**: Align physical spaces
5. **MRUK + RoomGuardian**: Room understanding and safety boundaries
6. **OVRPassthroughLayer**: Emergency passthrough for safety

#### **Recommended for Enhanced Features**
1. **HandGrabInteractor/RayInteractor**: Natural object interaction
2. **PlayerLocomotor + TeleportInteractor**: Movement system
3. **PointableCanvas**: VR UI interaction
4. **AnchorPrefabSpawner**: Spawn objects on real-world surfaces
5. **PlayerNameTagSpawner**: Display user names in space

#### **Available but Not MVP Priority**
1. **Meta Haptics SDK**: Advanced haptic feedback
2. **OVRBody/OVRFace/OVREyeGaze**: Advanced biometric tracking
3. **Meta Audio SDK**: Spatial audio with room acoustics
4. **CustomMatchmaking/FriendsMatchmaking**: Advanced matchmaking

### Quick Start with Meta XR Building Blocks

Meta XR SDK includes **Building Blocks** - pre-configured prefabs for common VR features:

```
Recommended Building Blocks for MVP:
✅ Spatial Anchor Core - For shared coordinate system
✅ Passthrough Projection Surface - For MR safety features  
✅ Room Mesh Controller - For environment visualization
✅ Local Matchmaking - For connecting headsets
✅ Colocation - For physical space alignment
```

These can be added via: **Window > Meta XR > Tools > Building Blocks**

### Meta Platform SDK - Colocation Features

The **colocation system** is critical for multi-user co-located experiences:

```csharp
// Key Components for Physical Space Sharing:
ColocationController - Manages shared space session
SharedSpatialAnchorCore - Creates anchor all users align to
AlignCameraToAnchor - Positions each user's camera relative to anchor
LocalMatchmaking - Discovers nearby Quest devices automatically
```

**Workflow**: Host creates shared anchor → Other users discover via LocalMatchmaking → All align to same anchor → Physical coordinates synchronized

## 🚀 Getting Started

### ✅ Completed Setup (Phase 1 - 44% Complete)
1. ✅ **Meta Quest 3 SDK installed** (Meta XR All-in-One SDK v78.0.0)
2. ✅ **Unity Netcode for GameObjects installed** (v2.6.0)
3. ✅ **Core scripts created**:
   - `VRSceneSetup.cs` - Automated VR configuration
   - `UserTrackingSystem.cs` - Position tracking
   - `VRNetworkManager.cs` - Network framework (Phase 2)
4. ✅ **Documentation created**:
   - `Assets/Scripts/README.md` - Implementation guide
   - `PHASE1_PROGRESS.md` - Detailed status
   - `QUICK_REFERENCE.md` - Command reference

### ⏳ Immediate Next Steps (Unity Editor)
1. **Open Unity Editor** and load `SampleScene.unity`
2. **Run automated VR setup**: 
   - Menu: `Multi-User VR > Setup > Phase 1 - Configure VR Scene`
   - This auto-configures OVRCameraRig and OVRManager
3. **Validate setup**:
   - Menu: `Multi-User VR > Setup > Validate Phase 1 Setup`
   - Check console for ✅ success messages
4. **Add input sources** (Optional):
   - Menu: `Window > Meta XR > Tools > Building Blocks`
   - Add: `Interaction SDK > Controller`
5. **Test in simulator**:
   - Menu: `Window > Meta XR > Test in Simulator`
   - Press Play ▶️ and test with WASD + Mouse

### 📊 Phase 1 Progress Summary
- **Completed**: 4/9 tasks (44%)
- **Remaining**: 5 tasks (scene configuration, input setup, prefab creation)
- **Estimated Time**: 2-3 hours to complete Phase 1
- **Blockers**: None - awaiting Unity Editor execution

### 🎯 Phase 2 Preparation
Once Phase 1 is complete:
- Install Meta XR Building Blocks (Colocation, Spatial Anchor Core)
- Implement Unity Netcode integration in `VRNetworkManager.cs`
- Test with 2-3 physical Quest 3 headsets
- Align physical spaces using `SharedSpatialAnchorCore`

### Success Definition
**MVP is successful when 3 users can safely collaborate in the same physical room for 30+ minutes on a simple task without safety incidents, with smooth synchronization, and clear advantages over remote alternatives.**

This MVP will serve as the foundation for a potentially revolutionary approach to enterprise VR training and collaboration, making high-end VR experiences accessible and affordable for organizations that cannot invest in expensive custom installations.

---

## 🔑 Meta XR SDK Quick Reference

### Most Important Components for Co-Located Multi-User VR

#### **Networking & Colocation (CRITICAL)**
```csharp
// Meta Platform SDK - Colocation Package
LocalMatchmaking                  // Auto-discover nearby Quest devices
ColocationController              // Manage shared physical space session  
SharedSpatialAnchorCore          // Create/share spatial anchor all users align to
AlignCameraToAnchor              // Position user relative to shared anchor
ColocationSessionEventHandler     // Handle connection events
```

#### **Safety & Room Awareness (CRITICAL)**
```csharp
// Meta MR Utility Kit
MRUK                             // Main MR manager
MRUKRoom                         // Room boundary and layout data
RoomGuardian                     // Safety boundary enforcement
OVRPassthroughLayer              // Emergency see-through mode
OVRSceneManager                  // Scene understanding
```

#### **VR Core (ESSENTIAL)**
```csharp
// Meta XR Core SDK
OVRCameraRig                     // Main VR camera/tracking
OVRManager                       // Core VR system manager
OVRSpatialAnchor                 // Persistent spatial anchors
```

#### **Interaction (ESSENTIAL)**
```csharp
// Meta XR Interaction SDK  
FromOVRHandDataSource            // Hand tracking input
FromOVRControllerDataSource      // Controller input
HandGrabInteractor/Interactable  // Grab objects with hands
RayInteractor/Interactable       // Point and select
```

### Component Discovery in Unity
Over **300+ components** available from Meta XR SDK. Key namespaces:
- `Meta.XR.MultiplayerBlocks.*` - Colocation and multiplayer
- `Meta.XR.MRUtilityKit.*` - Room understanding and safety
- `Oculus.Interaction.*` - Input and interaction systems
- `OVR*` - Core VR functionality

Use MCP tools to explore: `mcp_unity-mcp_Component_GetAll` with search terms like "Meta", "OVR", "Colocation", "MRUK"